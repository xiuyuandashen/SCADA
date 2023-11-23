using SCADA.Communication;
using SCADA.Communication.Modbus;
using SCADA.Industrial.Models;
using SCADA.Industrial.Models.Dto;
using SCADA.Industrial.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Base
{
    public class GlobalMonitor
    {
        public static List<StorageAreaDto> StorageAreaDtos { get; set; }

        public static List<DevicesDto> DevicesDtos { get; set; }

        public static SerialInfo SerialInfo { get; set; }

        static bool isRuning = true;

        static Task MainTask { get; set; } = null;

        static RTU rtuInstance { get; set; } = null;

        public static void Start(Action SuccessAction, Action<string> ErrorAction)
        {
            MainTask = Task.Run(async () =>
            {
                // 获取串口配置信息
                IndustrialService industrialService = new IndustrialService();
                DataResult<SerialInfo> SerialInfodataResult = industrialService.InitSerialInfo();
                if (SerialInfodataResult.Status)
                {
                    SerialInfo = SerialInfodataResult.Result;
                }
                else
                {
                    // 出现异常
                    ErrorAction(SerialInfodataResult.Message);
                    return;
                }

                // 获取存储区信息
                StorageAreaService storageAreaService = new StorageAreaService();
                DataResult<List<StorageAreaDto>> StorageAreaDataResult = storageAreaService.GetAll();
                if (StorageAreaDataResult.Status)
                {
                    StorageAreaDtos = StorageAreaDataResult.Result;
                }
                else
                {
                    // 出现异常
                    ErrorAction(StorageAreaDataResult.Message);
                    return;
                }

                // 初始化设备变量集合以及警戒值
                DeviceService deviceService = new DeviceService();
                DataResult<List<DevicesDto>> DevicedataResult = deviceService.GetAll();
                if (DevicedataResult.Status)
                {
                    DevicesDtos = DevicedataResult.Result;
                }
                else
                {
                    // 出现异常
                    ErrorAction(DevicedataResult.Message);
                    return;
                }

                // 初始化串口通信
                rtuInstance = RTU.getInstance(SerialInfo);
                rtuInstance.ResponseData = new Action<int, List<byte>>(RarsingData);
                if (!rtuInstance.Connection())
                {
                    ErrorAction("程序无法启动，串口连接初始化失败！请检查设备是否连接正常。");
                    return;
                }

                // 执行成功委托启动页面
                SuccessAction();
                int startAddar = 0;
                while (isRuning)
                {
                    foreach (var item in StorageAreaDtos)
                    {
                        // 报文长度过长分段传输 Modbus请求报文最大长度256字节 这边的LENGTH是寄存器个数 一个寄存器占用2字节 相当于 LENGTH * 2 <= 256 这边取整设置100
                        if (item.LENGTH > 100)
                        {
                            startAddar = item.START_REQ.Value;
                            int readCount = item.LENGTH.Value / 100;
                            for (int i = 0; i < readCount; i++)
                            {
                                int readLen = i == readCount ? item.LENGTH.Value - 100 * i : 100;
                                await rtuInstance.Send(item.SLAVE_ADDR.Value, byte.Parse(item.FUNC_CODE), startAddar + i * 100, readLen);
                            }
                        }
                        // 不能整除时，需要传递余下的报文
                        if (item.LENGTH % 100 > 0)
                        {
                            await rtuInstance.Send(item.SLAVE_ADDR.Value, byte.Parse(item.FUNC_CODE), startAddar + (item.LENGTH.Value / 100) * 100, item.LENGTH.Value % 100);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// 响应回调函数
        /// </summary>
        /// <param name="start_addr"></param>
        /// <param name="byteList"></param>
        private static void RarsingData(int start_addr, List<byte> byteList)
        {
            if (byteList != null && byteList.Count > 0)
            {
                // 从站地址 + 功能码 + 起始地址
                string areaId = byteList[0] + byteList[1].ToString("00") + start_addr;
                var monitorList = (
                        from d in DevicesDtos
                        from m in d.MonitorValuesList
                        where m.S_AREA_ID == areaId //&& d.IsRunnig
                        select m
                    ).ToList();
                int startByte;
                byte[] resBytes;
                foreach (var item in monitorList)
                {

                    switch (item.DATA_TYPE)
                    {
                        case "Float":
                            // Float 寄存器一个地址占用16字节 Float占用2格
                            // Modbus RTU 从站返回报文 从站地址、功能码、字节计数、寄存器数据、CRC校验（已去除）
                            // 所以startByte要加3
                            startByte = 3 + item.ADDRESS * 2;
                            // Float 是32位大小 占用 4个字节
                            resBytes = new byte[4] { byteList[startByte], byteList[startByte + 1], byteList[startByte + 2], byteList[startByte + 3] };
                            item.CurrentValue = resBytes.BigEndianByteArrayToFloat();
                            break;
                    }
                }
            }
        }


        public static void Stop()
        {
            isRuning = false;
            if (rtuInstance != null)
            {
                rtuInstance.Dispose();
            }
            if (MainTask != null)
            {
                MainTask.Wait();
            }
        }
    }
}

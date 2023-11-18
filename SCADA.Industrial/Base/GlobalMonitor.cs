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
            MainTask = Task.Run(() =>
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
                if (!rtuInstance.Connection())
                {
                    ErrorAction("程序无法启动，串口连接初始化失败！请检查设备是否连接正常。");
                    return;
                }

                // 执行成功委托启动页面
                SuccessAction();

                while (isRuning)
                {

                }
            });
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

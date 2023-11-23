using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Communication.Modbus
{
    public class RTU
    {
        private static SerialInfo _serialInfo;
        private static RTU _instance;
        private static object oLock = new object();
        private static object DoLock = new object();
        //volatile
        SerialPort _port;
        int _currentSlave;
        // 报文字节大小
        int _wordLen;
        byte _funcCode;
        int _startAddr;
        /// <summary>
        /// 响应数据回调
        /// <para>
        /// 参数：起始地址，除去CRC的响应字节数据
        /// </para>
        /// </summary>
        public Action<int, List<byte>> ResponseData;

        private RTU(SerialInfo serialInfo)
        {
            _port = new SerialPort();
            _serialInfo = serialInfo;
        }

        public static RTU getInstance(SerialInfo serialInfo)
        {
            if (_instance == null)
            {
                lock (oLock)
                {
                    if (_instance == null)
                    {
                        _instance = new RTU(serialInfo);
                    }
                }
            }
            return _instance;
        }


        public bool Connection()
        {
            try
            {
                if (_port != null && _port.IsOpen)
                {
                    _port.Close();
                }
                _port.PortName = _serialInfo.PortName;
                _port.BaudRate = _serialInfo.BaudRate;
                _port.DataBits = _serialInfo.DataBit;
                _port.Parity = _serialInfo.Parity;
                _port.StopBits = _serialInfo.StopBits;
                // 设置一个字节触发一次回调
                _port.ReceivedBytesThreshold = 1;
                // 接收数据回调
                _port.DataReceived += _port_DataReceived;
                _port.Open();
                return true;
            }
            catch
            {

                return false;

            }


        }

        public void Dispose()
        {
            if (_port != null && _port.IsOpen)
            {
                _port.Close();
                _port.Dispose();
                _port = null;
            }
        }

        int _receiveByteCount = 0;
        byte[] _receiveBuffer = new byte[512];

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte _receiveByte;
            while (_port.BytesToRead > 0)
            {
                _receiveByte = (byte)_port.ReadByte();
                _receiveBuffer[_receiveByteCount++] = _receiveByte;
                if (_receiveByteCount >= 512)
                {
                    _receiveByteCount = 0;
                    // 清空缓冲区数据
                    _port.DiscardInBuffer();
                    return;
                }
            }

            // 校验报文
            if (_receiveBuffer[0] == (byte)_currentSlave && _receiveBuffer[1] == _funcCode && _receiveByteCount >= _wordLen + 5)
            {
                // 检查CRC

                // 执行数据回调
                byte[] bytes = new byte[_wordLen + 3];//去除CRC的byte数据
                Array.Copy(_receiveBuffer, bytes, bytes.Length);
                ResponseData?.Invoke(_startAddr, new List<byte>(bytes));
                _port.DiscardInBuffer();
            }
            _receiveBuffer = new byte[512];

        }

        /// <summary>
        /// Modbus RTU 主站发送
        /// </summary>
        /// <param name="slaveAddr">从站地址</param>
        /// <param name="funcCode">功能码</param>
        /// <param name="startAddr">寄存器地址</param>
        /// <param name="len">寄存器数量</param>
        /// <returns></returns>
        public async Task<bool> Send(int slaveAddr, byte funcCode, int startAddr, int len)
        {
            try
            {
                _currentSlave = slaveAddr;
                _funcCode = funcCode;
                _startAddr = startAddr;
                // 线圈类型 Coil Status
                if (funcCode == 0x01)
                    _wordLen = len / 8 + ((len % 8 > 0) ? 1 : 0);
                // 保持寄存器 Holding Register
                if (funcCode == 0x03)
                    // 一个寄存器是16位 
                    _wordLen = len * 2;

                List<byte> sendBuffer = new List<byte>();
                // 从站地址
                sendBuffer.Add((byte)slaveAddr);
                // 功能码
                sendBuffer.Add(funcCode);
                // 寄存器地址高位 32位数除256得到高16位 摩256得到低16位 然后转字节
                sendBuffer.Add((byte)(startAddr / 256));
                // 寄存器地址低位
                sendBuffer.Add((byte)(startAddr % 256));
                // 寄存器数量高位
                sendBuffer.Add((byte)(len / 256));
                // 寄存器数量低位
                sendBuffer.Add((byte)(len % 256));
                // 得到两位CRC校验 字节0是高8位，字节1是低8位
                byte[] crc = CRC16.CRCCalc(sendBuffer.ToArray());
                sendBuffer.AddRange(crc);
                // 同一时期只允许一个线程写入
                lock (DoLock)
                {
                    _receiveByteCount = 0;
                    _port.Write(sendBuffer.ToArray(), 0, 8);
                }

                await Task.Delay(1000);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}

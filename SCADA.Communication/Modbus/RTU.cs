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
        SerialPort _port;

        private RTU(SerialInfo serialInfo)
        {
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

                _port = new SerialPort();
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


        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        /// <summary>
        /// 
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

                _port.Write(sendBuffer.ToArray(), 0, 8);
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

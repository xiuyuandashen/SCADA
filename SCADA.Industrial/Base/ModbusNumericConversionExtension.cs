using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Base
{
    public static class ModbusNumericConversionExtension
    {
        /// <summary>
        /// 大端序字节数组转单精度浮点数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static float BigEndianByteArrayToFloat(this byte[] data)
        {
            byte[] bytes = new byte[data.Length];
            bytes[0] = data[3];
            bytes[1] = data[2];
            bytes[2] = data[1];
            bytes[3] = data[0];
            return BitConverter.ToSingle(bytes, 0);
        }

        /// <summary>
        /// 小端序字节数组转单精度浮点数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static float LittleEndianByteArrayToFloat(this byte[] data)
        {
            byte[] bytes = new byte[data.Length];
            bytes[0] = data[0];
            bytes[1] = data[1];
            bytes[2] = data[2];
            bytes[3] = data[3];
            return BitConverter.ToSingle(bytes, 0);
        }
    }
}

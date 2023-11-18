using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Base
{
    /// <summary>
    /// 系统全局变量
    /// </summary>
    public static class GlobalConfiguration
    {

        /// <summary>
        /// 串口号
        /// </summary>
        public static string PortName { get; set; } = ConfigurationManager.AppSettings["PortName"].ToString();

        /// <summary>
        /// 波特率
        /// </summary>
        public static int BaudRate { get; set; } = int.Parse(ConfigurationManager.AppSettings["BaudRate"].ToString());

        /// <summary>
        /// 数据位
        /// </summary>
        public static int DataBit { get; set; } = int.Parse(ConfigurationManager.AppSettings["DataBit"].ToString());

        /// <summary>
        /// 校验位
        /// </summary>
        public static Parity Parity { get; set; } = (Parity)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["Parity"].ToString());

        /// <summary>
        /// 停止位
        /// </summary>
        public static StopBits StopBits { get; set; } = (StopBits)Enum.Parse(typeof(StopBits), ConfigurationManager.AppSettings["StopBits"].ToString());
    }
}

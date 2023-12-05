using SCADA.Industrial.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Models.Dto
{
    /// <summary>
    /// 设备日志
    /// </summary>
    public class LogModel : BaseDto
    {

        public int RowNumber { get; set; }

        public string DeviceName { get; set; }

        public string LogInfo { get; set; }

        public string Message { get; set; }

        public LogType LogType { get; set; }
    }
}

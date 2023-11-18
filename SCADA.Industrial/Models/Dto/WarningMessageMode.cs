using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Models.Dto
{
    public class WarningMessageMode
    {
        /// <summary>
        /// 监控值Id
        /// </summary>
        public string ValueId { get; set; }

        /// <summary>
        /// 监控值对应的告警信息
        /// </summary>
        public string Message { get; set; }
    }
}

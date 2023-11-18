using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Base
{
    /// <summary>
    /// 监控值状态枚举
    /// </summary>
    public enum MonitorValuesState
    {
        OK=0,//正常
        LoLo,//极低
        Low,//过低
        High,//过高
        HiHi//极高
    }
}

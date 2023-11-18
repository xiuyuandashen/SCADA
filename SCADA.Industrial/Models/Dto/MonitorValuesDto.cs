using SCADA.Industrial.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Models.Dto
{
    public class MonitorValuesDto : BaseDto
    {
        /// <summary>
        /// 告警数据传递委托
        /// 参数：值告警状态、监控阈值告警信息
        /// </summary>
        public Action<MonitorValuesState, string> ValueStateChanged { get; set; }    


        private int d_id;

        public int D_ID
        {
            get { return d_id; }
            set { d_id = value; OnPropertyChanged(); }
        }

        private string value_id;

        public string VALUE_ID
        {
            get { return value_id; }
            set { value_id = value; OnPropertyChanged(); }
        }


        private string value_name;

        public string VALUE_NAME
        {
            get { return value_name; }
            set { value_name = value; OnPropertyChanged(); }
        }

        private string s_area_id;

        public string S_AREA_ID
        {
            get { return s_area_id; }
            set { s_area_id = value; OnPropertyChanged(); }
        }

        private int address;

        public int ADDRESS
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }

        private string data_type;

        public string DATA_TYPE
        {
            get { return data_type; }
            set { data_type = value; OnPropertyChanged(); }
        }

        private string is_alarm;

        public string IS_ALARM
        {
            get { return is_alarm; }
            set { is_alarm = value; OnPropertyChanged(); }
        }

        private string unit;

        public string UNIT
        {
            get { return unit; }
            set { unit = value; OnPropertyChanged(); }
        }

        private string alarm_lolo;

        public string ALARM_LOLO
        {
            get { return alarm_lolo; }
            set { alarm_lolo = value; OnPropertyChanged(); }
        }

        private string alarm_low;

        public string ALARM_LOW
        {
            get { return alarm_low; }
            set { alarm_low = value; OnPropertyChanged(); }
        }

        private string alarm_high;

        public string ALARM_HIGH
        {
            get { return alarm_high; }
            set { alarm_high = value; OnPropertyChanged(); }
        }

        private string alarm_hihi;

        public string ALARM_HIHI
        {
            get { return alarm_hihi; }
            set { alarm_hihi = value; OnPropertyChanged(); }
        }

        private string description;

        public string DESCCRIPTION
        {
            get { return description; }
            set { description = value; }
        }



        private string currentValue;

        /// <summary>
        /// 当前值
        /// </summary>
        public string CurrentValue
        {
            get { return currentValue; }
            set 
            {
                currentValue = value;
                // 如果监控阈值
                if ("是".Equals(is_alarm))
                {
                    // 监控异常信息
                    string msg = description;
                    double val = Convert.ToDouble(CurrentValue);
                    // 监控值状态
                    MonitorValuesState state = MonitorValuesState.OK;
                    if (val < Convert.ToDouble(alarm_lolo))
                    {
                        msg += "极低";
                        state = MonitorValuesState.LoLo;
                    }
                    else if(val < Convert.ToDouble(alarm_low))
                    {
                        msg += "过低";
                        state = MonitorValuesState.Low;
                    }
                    else if(val > Convert.ToDouble(alarm_hihi)) {
                        msg += "极高";
                        state = MonitorValuesState.HiHi;
                    }
                    else if(val > Convert.ToDouble(alarm_high))
                    {
                        msg += "过高";
                        state = MonitorValuesState.High;
                    }
                    // 传递告警信息回调
                    ValueStateChanged(state, msg + $",当前值:[{value}]");
                }
                OnPropertyChanged(); 
            }
        }


    }
}

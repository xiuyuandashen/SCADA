using SCADA.Industrial.Base;
using SCADA.Industrial.Models.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.ViewModel
{
    public class SystemMonitorViewModel : NotifyPropertyBase
    {
        public SystemMonitorViewModel()
        {
            InitLogInfo();
            InitTestDevice();
        }


        public DevicesDto TestDevice { get; set; }

        private void InitTestDevice()
        {
            TestDevice = new DevicesDto();
            TestDevice.D_NAME = "冷却塔 1#";
            TestDevice.IsRunnig = true;
            TestDevice.IsWarning = true;
            TestDevice.MonitorValuesList.Add(new MonitorValuesDto()
            {
                VALUE_NAME = "液压",
                UNIT = "m",
                CurrentValue = 45,
                Values = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservableValue> { new LiveCharts.Defaults.ObservableValue(0), new LiveCharts.Defaults.ObservableValue(1), new LiveCharts.Defaults.ObservableValue(30) }
            });
            TestDevice.MonitorValuesList.Add(new MonitorValuesDto()
            {
                VALUE_NAME = "入口压力",
                UNIT = "Mpa",
                CurrentValue = 34,
                Values = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservableValue> { new LiveCharts.Defaults.ObservableValue(0), new LiveCharts.Defaults.ObservableValue(1), new LiveCharts.Defaults.ObservableValue(2) }
            });
            TestDevice.MonitorValuesList.Add(new MonitorValuesDto()
            {
                VALUE_NAME = "入口温度",
                UNIT = "℃",
                CurrentValue = 45,
            });
            TestDevice.MonitorValuesList.Add(new MonitorValuesDto()
            {
                VALUE_NAME = "出口压力",
                UNIT = "Mpa",
                CurrentValue = 34,
            });
            TestDevice.MonitorValuesList.Add(new MonitorValuesDto()
            {
                VALUE_NAME = "出口温度",
                UNIT = "℃",
                CurrentValue = 35,
            });
            TestDevice.MonitorValuesList.Add(new MonitorValuesDto()
            {
                VALUE_NAME = "补水压力",
                UNIT = "Mpa",
                CurrentValue = 45,
            });

            TestDevice.ErrorMessagesList.Add(new WarningMessageMode { Message = "冷却塔1#液位过低，当前值：0" });
            TestDevice.ErrorMessagesList.Add(new WarningMessageMode { Message = "冷却塔1#液位过低，当前值：15" });

        }

        private void InitLogInfo()
        {
            LogList = new ObservableCollection<LogModel>();
            LogList.Add(new LogModel()
            {
                RowNumber = 1,
                DeviceName = $"冷却塔 1#",
                LogInfo = "已启动",
                Message = "一切正常",
                LogType = LogType.Info
            }
            );
            LogList.Add(new LogModel()
            {
                RowNumber = 2,
                DeviceName = $"冷却塔 2#",
                LogInfo = "已启动",
                Message = "液位极低",
                LogType = LogType.Fault
            }
            );
            LogList.Add(new LogModel()
            {
                RowNumber = 3,
                DeviceName = $"冷却塔 3#",
                LogInfo = "已启动",
                Message = "液位偏高",
                LogType = LogType.Warn
            }
           );
            LogList.Add(new LogModel()
            {
                RowNumber = 4,
                DeviceName = $"冷却塔 4#",
                LogInfo = "已启动",
                Message = "液位过高",
                LogType = LogType.Fault
            }
           );
        }

        private ObservableCollection<LogModel> _logList;

        public ObservableCollection<LogModel> LogList
        {
            get { return _logList; }
            set { _logList = value; NotifyPropertyChanged(); }
        }

    }
}

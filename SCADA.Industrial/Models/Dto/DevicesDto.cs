using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Models.Dto
{
    public class DevicesDto : BaseDto
    {

        private int d_id;

        public int D_ID
        {
            get { return d_id; }
            set { d_id = value; OnPropertyChanged(); }
        }

        private string d_name;

        public string D_NAME
        {
            get { return d_name; }
            set { d_name = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 是否有告警
        /// </summary>
        private bool _isWarning = false;

        public bool IsWarning
        {
            get { return _isWarning; }
            set { _isWarning = value; OnPropertyChanged(); }
        }


        private bool _IsRunnig = false;

        public bool IsRunnig
        {
            get { return _IsRunnig; }
            set { _IsRunnig = value; OnPropertyChanged(); }
        }


        public ObservableCollection<MonitorValuesDto> MonitorValuesList { get; set; } = new ObservableCollection<MonitorValuesDto>();


        public ObservableCollection<WarningMessageMode> ErrorMessagesList { get; set; } = new ObservableCollection<WarningMessageMode>();
    }
}

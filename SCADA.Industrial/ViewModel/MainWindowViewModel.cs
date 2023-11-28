using GalaSoft.MvvmLight.CommandWpf;
using SCADA.Industrial.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SCADA.Industrial.ViewModel
{
    public class MainWindowViewModel : NotifyPropertyBase
    {

        private UIElement _mainContent;

        public UIElement MainContent
        {
            get { return _mainContent; }
            set { _mainContent = value; NotifyPropertyChanged(); }
        }

        public RelayCommand<string> TabChanngedCommand { get; set; }

        public MainWindowViewModel()
        {
            TabChanngedCommand = new RelayCommand<string>(TabChannged);
            TabChannged("SystemMonitor");
        }

        private void TabChannged(string obj)
        {
            if (string.IsNullOrWhiteSpace(obj)) return;
            Type type = typeof(MainWindowViewModel).Assembly.GetTypes().Where(f => f.Name.Contains(obj)).FirstOrDefault();
            
            if (type != null)
            {
                object page = Activator.CreateInstance(type);
                MainContent = (UIElement)page;
            }
        }
    }
}

using SV_final.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV_final.ViewModel
{
    class MainViewModel : BaseViewModel
    {

        private static ObservableCollection<ObjectDetect> _listData;

        public ObservableCollection<ObjectDetect> ListData
        {
            get { return _listData; }
            set
            {
                _listData = value;
                OnPropertyChanged("ListData");
            }
        }

        public OpenViewModel openVM;
        public LogViewModel LogVM { get; set; }
        public SplitViewModel SplitVM { get; set; }
        public MergeViewModel MergeVM { get; set; }

        private object _openContent;

        public object OpenContent
        {
            get { return _openContent; }
            set
            {
                _openContent = value;
                OnPropertyChanged("OpenContent");
            }
        }

        private object _convertContent;

        public object ConvertContent
        {
            get { return _convertContent; }
            set
            {
                _convertContent = value;
                OnPropertyChanged("ConvertContent");
            }
        }

        public MainViewModel()
        {
            LogVM = new LogViewModel();
            this.OpenContent = new OpenViewModel(this, LogVM);
            this.openVM = new OpenViewModel(this, LogVM);
            this.ConvertContent = new ConvertViewModel(this, new OpenViewModel(this, LogVM));
            SplitVM = new SplitViewModel(LogVM);
            ListData = new ObservableCollection<ObjectDetect>();
        }
    }
}

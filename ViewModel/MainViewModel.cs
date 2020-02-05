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
        public OpenViewModel OpenVM { get; set; }

        private ObservableCollection<ObjectDetect> _listData;

        public ObservableCollection<ObjectDetect> ListData
        {
            get { return _listData; }
            set
            {
                ListData = value;
                OnPropertyChanged("ListData");
            }
        }

        private object _openContent;

        public object OpenContent
        {
            get { return _openContent; }
            set
            {
                _openContent = value;
                OnPropertyChanged("Content1");
            }
        }

        public MainViewModel()
        {
            OpenVM = new OpenViewModel(this);
            this.OpenContent = new OpenViewModel(this);
            ListData = new ObservableCollection<ObjectDetect>();
        }
    }
}

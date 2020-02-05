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
        }
    }
}

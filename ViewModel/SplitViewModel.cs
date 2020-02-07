using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV_final.ViewModel
{
    class SplitViewModel : BaseViewModel
    {
        private LogViewModel logViewModel;
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand DelCommand { get; private set; }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<string> WorkerList { get => _wokerList; set => _wokerList = value; }
        private ObservableCollection<string> _wokerList;

        public string SelectedItem { get; set; }

        public SplitViewModel(LogViewModel logViewModel)
        {
            this.logViewModel = logViewModel;
            AddCommand = new RelayCommand(Add);
            DelCommand = new RelayCommand(Del);
            WorkerList = new ObservableCollection<string>();
        }

        private void Del()
        {
            if (SelectedItem != null)
                WorkerList.Remove(SelectedItem);
        }

        private void Add()
        {
            WorkerList.Add(Name);
            Name = "";
        }
    }
}

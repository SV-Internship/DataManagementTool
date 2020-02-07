using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SV_final.ViewModel
{
    class SplitViewModel : BaseViewModel
    {
        private LogViewModel logViewModel;
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand DelCommand { get; private set; }
        public RelayCommand CheckCommand { get; private set; }
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
        private int intNum { get; set; }
        private string _number;
        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }
        private string _numInfo;
        public string NumInfo
        {
            get
            {
                return _numInfo;
            }
            set
            {
                _numInfo = value;
                OnPropertyChanged("NumInfo");
            }
        }
        private string _nameVisibility;
        public string NameVisibility
        {
            get
            {
                return _nameVisibility;
            }
            set
            {
                _nameVisibility = value;
                OnPropertyChanged("NameVisibility");
            }

        }
        private string _numVisibility;
        public string NumVisibility
        {
            get
            {
                return _numVisibility;
            }
            set
            {
                _numVisibility = value;
                OnPropertyChanged("NumVisibility");
            }
        }
        private string Option { get; set; }
        public ObservableCollection<string> WorkerList { get => _wokerList; set => _wokerList = value; }
        private ObservableCollection<string> _wokerList;

        public string SelectedItem { get; set; }

        public SplitViewModel(LogViewModel logViewModel)
        {
            this.logViewModel = logViewModel;
            AddCommand = new RelayCommand(Add);
            DelCommand = new RelayCommand(Del);
            CheckCommand = new RelayCommand(Check);
            WorkerList = new ObservableCollection<string>();
            NameVisibility = "Hidden";
            NumVisibility = "Hidden";
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

        private void Check()
        {
            int i;
            if (!Int32.TryParse(Number, out i))
            {
                intNum = -1;
                NumInfo = "잘못된 입력입니다";
            }
            else
            {
                intNum = i;
                Console.WriteLine(intNum);
                NumInfo = Number + "개 파일로 분할합니다.";
            }
        }

        public void OptionRB(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string opt = rb.Content as string;
            Console.WriteLine(opt);
            if (opt == "Name")
            {
                Option = opt;
                NameVisibility = "Visible";
                NumVisibility = "Hidden";
            }
            else if (opt == "Number")
            {
                Option = opt;
                NameVisibility = "Hidden";
                NumVisibility = "Visible";
            }
            else
            {
                Console.WriteLine("Radio botton ERROR");
            }
        }
    }
}

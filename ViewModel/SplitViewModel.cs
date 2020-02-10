﻿using SV_final.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace SV_final.ViewModel
{
    class SplitViewModel : BaseViewModel
    {
        private LogViewModel logViewModel;
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand DelCommand { get; private set; }
        public RelayCommand CheckCommand { get; private set; }
        public RelayCommand DoSplitCommand { get; private set; }

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
        private string option { get; set; }
        public ObservableCollection<string> WorkerList { get => _wokerList; set => _wokerList = value; }
        private ObservableCollection<string> _wokerList;

        public string SelectedItem { get; set; }

        public SplitViewModel(LogViewModel logViewModel)
        {
            this.logViewModel = logViewModel;
            AddCommand = new RelayCommand(Add);
            DelCommand = new RelayCommand(Del);
            CheckCommand = new RelayCommand(Check);
            DoSplitCommand = new RelayCommand(DoSplit);
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
            if(Name != "")
            {
                WorkerList.Add(Name);
                Name = "";
            }
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
                NumInfo = Number + "개 파일로 분할합니다.";
            }
        }

        private void DoSplit()
        {
            string OriginFileName = "test.xml";
            string NewFile = "New";
            string Format = ".xml";

            XmlSerializer deserializer = new XmlSerializer(typeof(ObjectDetect));
            TextReader reader = new StreamReader(@"..\..\" + OriginFileName);

            ObjectDetect Ori = (ObjectDetect)deserializer.Deserialize(reader);
            reader.Close();

            if (intNum == -1)
            {
                logViewModel.FailLog(GetType(), "분할할 수 없는 숫자입니다");
                return;
            }
            List<ObjectDetect> NewODs = new List<ObjectDetect>();

            if (option == "Name")
            {
                for (int i = 0; i < WorkerList.Count; i++)
                {
                    ObjectDetect NewOD = new ObjectDetect();
                    NewODs.Add(NewOD);
                }
            }
            else if (option == "Number")
            {
                for (int i = 0; i < intNum; i++)
                {
                    ObjectDetect NewOD = new ObjectDetect();
                    NewODs.Add(NewOD);
                }
            }
            else
            {
                //에러처리?
            }

            //foreach(ObjectDetect newOD in NewODs)
            //{
            //    newOD.Files.FileCount = (int)Math.Truncate((double)Ori.Files.FileCount / intNum);
            //    if (newOD == NewODs.LastOrDefault())
            //    {
            //        newOD.Files.FileCount = (int)Math.Ceiling((double)Ori.Files.FileCount / intNum);
            //    }
            //}

            // 1. file 안에 몇 개 들었는지 넣어줘야하고
            // 2. 오브젝트는 상관없이 그냥 FILE 넣어주면 될듯?
            // 3. 엥 왜그래야하지? 마지막에 그냥 file[]count 이거 넣어주면 안되나? 될꺼 같은데? -> 생성자에서 하면 제일 좋을 듯

            for (int i = 0; i < Ori.Files.FileCount; i++)
            {
                int count = intNum;
                if (option == "Name") count = WorkerList.Count();
                Console.WriteLine(Ori.Files.File[i].FileName);
                NewODs[i % count].Files.File.Add(Ori.Files.File[i]);
            }

            for(int i=0; i<NewODs.Count; i++)
            {
                ObjectDetect newOD = NewODs[i];
                newOD.Files.FileCount = newOD.Files.File.Count();
                var writer = new XmlSerializer(typeof(ObjectDetect));

                if (option == "Name")
                {
                    var wfile = new System.IO.StreamWriter(@"..\..\" + NewFile + "_" + WorkerList[i] + Format);
                    writer.Serialize(wfile, newOD);
                    wfile.Close();

                }
                else if (option == "Number")
                {
                    var wfile = new System.IO.StreamWriter(@"..\..\" + NewFile + "_" + i + Format);
                    writer.Serialize(wfile, newOD);
                    wfile.Close();
                }
                else
                {
                    //에러처리?
                }
            }

            //var obj = new ObjectDetect();
            //var writer = new XmlSerializer(typeof(ObjectDetect));
            //var wfile = new System.IO.StreamWriter(@"C:/Users/Stradvision/source/repos/DataManagementTool/test.xml");
            //writer.Serialize(wfile, obj);
            //wfile.Close();
        }
        public void OptionRB(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string opt = rb.Content as string;
            Console.WriteLine(opt);
            if (opt == "Name")
            {
                option = opt;
                NameVisibility = "Visible";
                NumVisibility = "Hidden";
            }
            else if (opt == "Number")
            {
                option = opt;
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

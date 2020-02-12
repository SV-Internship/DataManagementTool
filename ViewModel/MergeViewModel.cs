using Microsoft.WindowsAPICodePack.Dialogs;
using SV_final.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace SV_final.ViewModel
{
    class MergeViewModel : BaseViewModel
    {
        private LogViewModel logViewModel;
        private MainViewModel mainViewModel;
        private OpenViewModel openViewModel;
        public ICommand OpenPathCommand { get; private set; }
        public ICommand SavePathCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ObservableCollection<ObjectDetect> MergeList { get => _mergeList; set => _mergeList = value; }
        private ObservableCollection<ObjectDetect> _mergeList;

        public ObservableCollection<string> FileNameList { get => _fileNameList; set => _fileNameList = value; }
        private ObservableCollection<string> _fileNameList;

        public string SelectedItem { get; set; }

        private string _outPath;
        public string OutPath
        {
            get
            {
                return _outPath;
            }
            set
            {
                _outPath = value;
                OnPropertyChanged("OutPath");
            }
        }

        public MergeViewModel(MainViewModel mainViewModel, LogViewModel logViewModel, OpenViewModel openViewModel)
        {
            this.mainViewModel = mainViewModel;
            this.logViewModel = logViewModel;
            this.openViewModel = openViewModel;
            OpenPathCommand = new RelayCommand(OpenPath);
            SavePathCommand = new RelayCommand(SavePath);
            LoadCommand = new RelayCommand(Load);
            DeleteCommand = new RelayCommand(Delete);
            MergeList = new ObservableCollection<ObjectDetect>();
            FileNameList = new ObservableCollection<string>();
        }

        private void DoMerge()
        {
            ObjectDetect NewOD = new ObjectDetect();
            foreach (ObjectDetect OriOD in MergeList)
            {
                NewOD.Files.File.AddRange(OriOD.Files.File);
            }

            NewOD.Files.FileCount = NewOD.Files.File.Count();

            ODToXml(NewOD, OutPath);

            logViewModel.AddLog(GetType(), OutPath);
        }

        private ObjectDetect XmlToOD(string fileName)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ObjectDetect));
            TextReader reader = new StreamReader(@fileName);

            ObjectDetect result = (ObjectDetect)deserializer.Deserialize(reader);
            reader.Close();
            return result;
        }

        private void ODToXml(ObjectDetect OD, string fileName)
        {
            var writer = new XmlSerializer(typeof(ObjectDetect));
            var wfile = new System.IO.StreamWriter(fileName);
            writer.Serialize(wfile, OD);
            wfile.Close();
        }

        private void OpenPath()
        {
            try
            {
                var dlg = new CommonOpenFileDialog();
                dlg.Filters.Add(new CommonFileDialogFilter("xml", "xml"));
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    OutPath = dlg.FileName;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());

            }
        }
        private void SavePath()
        {
            try
            {
                DoMerge();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
                logViewModel.FailLog(GetType(), OutPath + "저장에 실패하였습니다.");
            }
        }
        private void Load()
        {
            try
            {
                MergeList.Clear();
                foreach (ObjectDetect MainOD in mainViewModel.ListData)
                {
                    MergeList.Add(MainOD);
                }
                FileNameList.Clear();
                foreach (string OpenF in openViewModel.FileList)
                {
                    FileNameList.Add(OpenF);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
                logViewModel.FailLog(GetType(), OutPath + "로드에 실패하였습니다.");
            }
        }
        private void Delete()
        {
            try
            {
                if (SelectedItem != null)
                {
                    MergeList.RemoveAt(FileNameList.IndexOf(SelectedItem));
                    FileNameList.Remove(SelectedItem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

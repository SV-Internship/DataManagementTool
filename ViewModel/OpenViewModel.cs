using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SV_final.ViewModel
{
    class OpenViewModel : BaseViewModel
    {
        private MainViewModel _mainViewmodel;

        private ObservableCollection<string> _fileList;
        public ObservableCollection<string> FileList
        {
            get { return _fileList; }
            set
            {
                _fileList = value;
                OnPropertyChanged("FileList");
            }
        }

        private string _xmlPath;

        public string XmlPath
        {
            get { return _xmlPath; }

            set
            {
                _xmlPath = value;
                OnPropertyChanged("XmlPath");
            }
        }

        private string _txtPath;

        public string TxtPath
        {
            get { return _txtPath; }

            set
            {
                _txtPath = value;
                OnPropertyChanged("TxtPath");
            }
        }

        private string _jsonPath;

        public string JsonPath
        {
            get { return _jsonPath; }

            set
            {
                _jsonPath = value;
                OnPropertyChanged("_jsonPath");
            }
        }

        public RelayCommand OpenCommand { get; private set; }

        public OpenViewModel(MainViewModel mainviewmodel)
        {
            this._mainViewmodel = mainviewmodel;
            FileList = new ObservableCollection<string>();
            OpenCommand = new RelayCommand(Open);
        }

        private void Open()
        {
            OpenFiles();
        }

        private void OpenFiles()
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.ValidateNames = false;
                dialog.CheckFileExists = false;
                dialog.CheckPathExists = true;
                dialog.FileName = "default";
                dialog.Filter = "Xml files(*.xml)|*.xml|Text files(*.txt)|*.txt|Json files(*.json)|*.json|All files|*.*";
                dialog.ShowDialog();

                string Path = System.IO.Path.GetFullPath(dialog.FileName);
                Path = Path.Replace("default", "").Trim();

                string[] parser = Path.Split('.');
                Console.WriteLine(Path);

                if (parser.Length == 2)
                {
                    if (parser[1] == "xml")
                    {
                        XmlPath = Path;
                        FileList.Add(XmlPath);
                        Console.WriteLine(XmlPath);
                    }
                    else if (parser[1] == "txt")
                    {
                        TxtPath = Path;
                        FileList.Add(TxtPath);
                        Console.WriteLine(TxtPath);
                    }
                    else if (parser[1] == "json")
                    {
                        JsonPath = Path;
                        FileList.Add(JsonPath);
                        Console.WriteLine(JsonPath);
                    }
                }
                else
                {
                    string[] parse = Path.Split('.');
                    Console.WriteLine(parse[0]);
                    string[] files = Directory.GetFiles(parse[0]);

                    foreach (string x in files)
                        FileList.Add(x);
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

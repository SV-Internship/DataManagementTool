using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using SV_final.Model;

namespace SV_final.ViewModel
{
    class OpenViewModel : BaseViewModel
    {
        private MainViewModel _mainViewmodel;

        private LogViewModel logViewModel;

        private static string _selectFile;

        public string SelectFile
        {
            get { return _selectFile; }
            set
            {
                _selectFile = value;
                OnPropertyChanged("SelectFile");
            }
        }

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

        private ObservableCollection<string> _visualFilelist;

        public ObservableCollection<string> VisualFileList
        {
            get { return _visualFilelist; }
            set
            {
                _visualFilelist = value;
                OnPropertyChanged("VisualFileList");
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

        public RelayCommand DeleteCommand { get; private set; }

        public OpenViewModel(MainViewModel mainviewmodel, LogViewModel logViewModel)
        {
            _mainViewmodel = mainviewmodel;
            this.logViewModel = logViewModel;
            FileList = new ObservableCollection<string>();
            VisualFileList = new ObservableCollection<string>();
            OpenCommand = new RelayCommand(Open);
            DeleteCommand = new RelayCommand(Delete);
        }

        private void Open()
        {
            OpenFiles();
        }

        private void OpenFiles()
        {
            string FilePath="";

            try
            {
                var dialog = new OpenFileDialog();
                dialog.ValidateNames = false;
                dialog.CheckFileExists = false;
                dialog.CheckPathExists = true;
                dialog.FileName = "default";
                dialog.Filter = "All files|*.*|Xml files(*.xml)|*.xml|Text files(*.txt)|*.txt|Json files(*.json)|*.json";
                dialog.ShowDialog();

                string Path = System.IO.Path.GetFullPath(dialog.FileName);
                Path = Path.Replace("default", "").Trim();
                FilePath = Path;

                string[] parser = Path.Split('.');
                Console.WriteLine(Path);

                if (parser.Length == 2)
                {
                    if (parser[1] == "xml")
                    {
                        XmlPath = Path;
                        FileList.Add(XmlPath);
                        string temp = XmlPath;
                        string[] fileparse = temp.Split('\\');
                        VisualFileList.Add(fileparse[fileparse.Length - 1]);
                        fileParsing(XmlPath);
                    }
                    else if (parser[1] == "txt")
                    {
                        TxtPath = Path;
                        FileList.Add(TxtPath);
                        string temp = TxtPath;
                        string[] fileparse = temp.Split('\\');
                        VisualFileList.Add(fileparse[fileparse.Length - 1]);
                        fileParsing(TxtPath);
                    }
                    else if (parser[1] == "json")
                    {
                        JsonPath = Path;
                        FileList.Add(JsonPath);
                        string temp = JsonPath;
                        string[] fileparse = temp.Split('\\');
                        VisualFileList.Add(fileparse[fileparse.Length - 1]);
                        fileParsing(JsonPath);
                    }
                }
                else
                {
                    string[] parse = Path.Split('.');
                    Console.WriteLine(parse[0]);
                    string[] files = Directory.GetFiles(parse[0]);

                    foreach (string x in files)
                    {
                        FileList.Add(x);
                        string temp = x;
                        string[] fileparse = temp.Split('\\');
                        VisualFileList.Add(fileparse[fileparse.Length - 1]);
                        fileParsing(x);
                    }
                }
                logViewModel.AddLog(GetType(), Path);
            }

            catch (Exception ex)
            {
                logViewModel.FailLog(GetType(), FilePath);
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
            }
        }

        private void Delete()
        {
            var x = VisualFileList.IndexOf(SelectFile);

            VisualFileList.RemoveAt(x);
            FileList.RemoveAt(x);
            _mainViewmodel.ListData.RemoveAt(x);
        }

        private int change(string name)
        {
            switch (name)
            {
                case "PEDESTRIAN":
                    return 1;
                case "BICYCLEMAN":
                    return 2;
                case "MOTORCYCLEMAN":
                    return 3;
                case "SEDAN":
                    return 4;
                case "BAN":
                    return 5;
                case "TRUCK":
                    return 6;
                case "TOPCAR":
                    return 7;
                case "BUS":
                    return 8;
                case "SITTING_MAN":
                    return 9;
                case "ETC":
                    return 10;
                case "BICYCLE":
                    return 11;
                case "MOTORCYCLE":
                    return 12;
                case "TANDEM":
                    return 13;
                case "PICKUPTRUCK":
                    return 14;
                case "AGITATOR":
                    return 15;
                case "EXCAVATOR":
                    return 16;
                case "FORKLIFT":
                    return 17;
                case "LADDERCAR":
                    return 18;
                case "ETCTRUCK":
                    return 19;
                case "ETCCAR":
                    return 20;
                case "FALSEDETECTION":
                    return 21;
                case "ANIMAL":
                    return 22;
                case "BIRD":
                    return 23;
                case "ETCANIMAL":
                    return 24;
                default:
                    return 25;
            }
        }

        private void fileParsing(string filename)
        {
            string[] parsing = filename.Split('.');

            if (parsing[1] == "xml")
            {
                readXml(filename);
            }
            else if (parsing[1] == "txt")
            {
                readTxt(filename);
            }
            else
            {
                readJson(filename);
            }
        }

        private void readTxt(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            bool check1 = true, check2 = false;
            int wholeFile = 0;
            int filecount = 0;

            foreach (string x in lines)
            {
                if (check1)
                {
                    _mainViewmodel.ListData.Add(new ObjectDetect());
                    _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File.Add(new _File());
                    _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File[
                        _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File.Count() - 1].FileName = x;

                    wholeFile++;
                    check1 = false;
                    check2 = true;
                }

                else if (check2)
                {
                    _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File[
                       _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File.Count() - 1].Objects.Count = int.Parse(x);

                    filecount = int.Parse(x);
                    check2 = false;
                }

                else
                {
                    filecount--;
                    string[] parsing = x.Split(' ');
                    string[] label_parse = parsing[5].Split('"');

                    _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File[
                       _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File.Count() - 1].Objects.Object.Add(
                        new _Object(0, int.Parse(parsing[0]), (int.Parse(parsing[4]) - int.Parse(parsing[2])), (int.Parse(parsing[3]) - int.Parse(parsing[1])),
                        0, 0, 0, -1, int.Parse(parsing[4]), int.Parse(parsing[3]), int.Parse(parsing[2]), int.Parse(parsing[1]),
                        int.Parse(parsing[2]), int.Parse(parsing[1]), int.Parse(parsing[4]), int.Parse(parsing[3]), label_parse[0]));

                    if (filecount == 0)
                        check1 = true;
                }
            }

            _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.FileCount = wholeFile;
            _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.FileName = TxtPath;
        }

        private void readXml(string filename)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ObjectDetect));
            TextReader reader = new StreamReader(filename);

            _mainViewmodel.ListData.Add(new ObjectDetect());
            _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1] = (ObjectDetect)deserializer.Deserialize(reader);
            reader.Close();

            _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.FileName = XmlPath;
        }

        private void readJson(string filename)
        {
            string txt;
            using (StreamReader sw = new StreamReader(filename))
            {
                txt = sw.ReadToEnd();
                var readJson = JObject.Parse(txt);

                _mainViewmodel.ListData.Add(new ObjectDetect());
                _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File.Add(new _File());

                _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File[
                          _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File.Count() - 1].Objects.Count = int.Parse(readJson["num_obj"].ToString());

                _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.FileName = JsonPath;

                var jsonObject = JArray.Parse(readJson["objects"].ToString());

                foreach (JObject jObj in jsonObject)
                {
                    var bBox = JArray.Parse(jObj["bbox"].ToString());
                    _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File[
                           _mainViewmodel.ListData[_mainViewmodel.ListData.Count() - 1].Files.File.Count() - 1].Objects.Object.Add(
                            new _Object(0, change(jObj["class"].ToString()), (int)(float.Parse(bBox[3].ToString()) - float.Parse(bBox[1].ToString())),
                            (int)(float.Parse(bBox[2].ToString()) - float.Parse(bBox[0].ToString())),
                            0, 0, 0, -1, (int)float.Parse(bBox[3].ToString()), (int)float.Parse(bBox[2].ToString()), (int)float.Parse(bBox[1].ToString()), (int)float.Parse(bBox[0].ToString()),
                            (int)float.Parse(bBox[1].ToString()), (int)float.Parse(bBox[0].ToString()), (int)float.Parse(bBox[3].ToString()), (int)float.Parse(bBox[2].ToString()), ""));
                }
            }
        }
    }
}
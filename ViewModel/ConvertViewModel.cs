using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using SV_final.Model;
using Newtonsoft.Json.Linq;

namespace SV_final.ViewModel
{
    class ConvertViewModel : BaseViewModel
    { 
        private MainViewModel _mainViewmodel;

        private OpenViewModel _openViewmodel;

        private string _changefile;

        public string changeFile
        {
            get { return _changefile; }
            set
            {
                _changefile = value;
                OnPropertyChanged("changeFile");
            }
        }

        public RelayCommand DoCommand { get; private set; }

        public ConvertViewModel(MainViewModel mainviewmodel, OpenViewModel openviewmodel)
        {
            _mainViewmodel = mainviewmodel;
            _openViewmodel = openviewmodel;
            DoCommand = new RelayCommand(Do);
        }

        private void Do()
        {
            fileParsing(changeFile);
        }

        private void fileParsing(string filename)
        {
            string[] parsing = filename.Split('.');

            if (parsing[1] == "xml")
            {
                WriteXml(filename);
            }
            else if (parsing[1] == "txt")
            {
                WriteTxt(filename);
            }
            else
            {
                WriteJson(filename);
            }
        }

        private void WriteXml(string filename)
        {

            int i = 0;
            Console.WriteLine(_openViewmodel.SelectFile);

            while (true)
            {
                if (_mainViewmodel.ListData[i].Files.FileName == _openViewmodel.SelectFile)
                {
                    Console.WriteLine(i);
                    break;
                }
                else
                    i++;
            }

            using (StreamWriter wr = new StreamWriter(filename))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ObjectDetect));
                xs.Serialize(wr, _mainViewmodel.ListData[i]);
            }
        }

        private void WriteTxt(string filename)
        {
            int i = 0;
            Console.WriteLine(_openViewmodel.SelectFile);
            while (true)
            {
                if (_mainViewmodel.ListData[i].Files.FileName == _openViewmodel.SelectFile)
                {
                    Console.WriteLine(i);
                    break;
                }
                else
                    i++;
            }

            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                foreach (_File file in _mainViewmodel.ListData[i].Files.File)
                {
                    outputFile.WriteLine(file.FileName);
                    if (file.Objects == null)
                    {
                        continue;
                    }
                    outputFile.WriteLine(file.Objects.Count);
                    foreach (_Object obj in file.Objects.Object)
                    {
                        outputFile.WriteLine(obj.Type + " " + obj.Position.Left + " " + obj.Position.Top + " " + obj.Position.Right + " " + obj.Position.Bottom + " " + obj.Label.Text);
                    }
                }
            }
        }

        private void WriteJson(string filename)
        {
            int i = 0;
            Console.WriteLine(_openViewmodel.SelectFile);
            while (true)
            {
                if (_mainViewmodel.ListData[i].Files.FileName == _openViewmodel.SelectFile)
                {
                    Console.WriteLine(i);
                    break;
                }
                else
                    i++;
            }

            using(StreamWriter outputFile = new StreamWriter(filename, false, Encoding.UTF8))
            {
                var whole = new JObject();
                var objects = new JArray();
                JObject size = new JObject(
                    new JProperty("size", new JObject(new JProperty("width", 2048), new JProperty("Height", 1024))),
                    new JProperty("num_obj", _mainViewmodel.ListData[i].Files.File[i].Objects.Count)
                    );

                whole.Add(size);

                foreach(_File file in _mainViewmodel.ListData[i].Files.File)
                {
                    if (file.Objects == null)
                    {
                        continue;
                    }

                    foreach (_Object obj in file.Objects.Object)
                    {
                        JObject fileobject = new JObject(
                            new JProperty("class", obj.Type),
                            new JProperty("bbox", new JArray(obj.Position.Left, obj.Position.Top, obj.Position.Right, obj.Position.Bottom))
                            );

                        objects.Add(fileobject);
                    }
                }

                whole.Add(objects);
                File.WriteAllText(filename, whole.ToString());


            }
        }
    }
}

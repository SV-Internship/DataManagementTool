using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SV_final.Model;

namespace SV_final.ViewModel
{
    class ConvertViewModel : BaseViewModel
    {
        private LogViewModel logViewModel;
        public RelayCommand DoCommand { get; private set; }

        public ConvertViewModel(LogViewModel logViewModel)
        {
            this.logViewModel = logViewModel;
            DoCommand = new RelayCommand(Do);
        }

        private void Do()
        {
            string OriginFileName ="test.xml";
            string NewFileName = "New_TEXT_File.txt";

            XmlSerializer deserializer = new XmlSerializer(typeof(ObjectDetect));
            TextReader reader = new StreamReader(@"..\..\" + OriginFileName);

            ObjectDetect overview = (ObjectDetect)deserializer.Deserialize(reader);
            reader.Close();

            using (StreamWriter outputFile = new StreamWriter(@"..\..\" + NewFileName))
            {
                foreach (_File file in overview.Files.File)
                {
                    outputFile.WriteLine(file.FileName);
                    if(file.Objects == null)
                    {
                        continue;
                    }
                    outputFile.WriteLine(file.Objects.Count);
                    foreach (_Object obj in file.Objects.Object)
                    {
                        outputFile.WriteLine(obj.Type + " "+ obj.Position.Left + " " + obj.Position.Top + " " + obj.Position.Right + " " + obj.Position.Bottom + " " + obj.Label.Text);
                    }
                }
            }
            logViewModel.AddLog(GetType(), OriginFileName, NewFileName);
        }
    }


    
}

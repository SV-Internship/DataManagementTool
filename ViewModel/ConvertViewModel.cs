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
        public RelayCommand DoCommand { get; private set; }

        public ConvertViewModel()
        {
            DoCommand = new RelayCommand(Do);
        }

        private void Do()
        {
            //여기서 변환을 일단은 해야함
            XmlSerializer deserializer = new XmlSerializer(typeof(ObjectDetect));
            TextReader reader = new StreamReader(@"..\..\test.xml");

            ObjectDetect overview = (ObjectDetect)deserializer.Deserialize(reader);
            reader.Close();

            Console.WriteLine(overview.Files.File[98].Objects.Count);

            using (StreamWriter outputFile = new StreamWriter(@"..\..\New_TEXT_File.txt"))
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
        }
    }


    
}

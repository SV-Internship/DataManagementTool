using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SV_final.Model;
using System.Xml.Serialization;
using System.IO;

namespace SV_final.View
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        ObservableCollection<ObjectDetect> listData;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listData = new ObservableCollection<ObjectDetect>();
            Data.ItemsSource = listData;

            //var obj = new ObjectDetect();
            //var writer = new XmlSerializer(typeof(ObjectDetect));
            //var wfile = new System.IO.StreamWriter(@"C:/Users/Stradvision/source/repos/DataManagementTool/test.xml");
            //writer.Serialize(wfile, obj);
            //wfile.Close();

            //// Now we can read the serialized book ...  
            ////System.Xml.Serialization.XmlSerializer reader =
            ////    new System.Xml.Serialization.XmlSerializer(typeof(Object));
            ////System.IO.StreamReader file = new System.IO.StreamReader(
            ////    @"C:/Users/Stradvision/source/repos/DataManage_v1/DataManage_v1/test.xml");
            ////Object overview = (Object)reader.Deserialize(file);
            ////file.Close();

            ////Console.WriteLine(overview.Type);


            //XmlSerializer deserializer = new XmlSerializer(typeof(ObjectDetect));
            //TextReader reader = new StreamReader(@"c:/users/stradvision/source/repos/DataManagementTool/test.xml");

            //object obj = deserializer.Deserialize(reader);
            //ObjectDetect overview = (ObjectDetect)deserializer.Deserialize(reader);
            //listData.Add((ObjectDetect)overview);
            //reader.Close();

            //FileCount.Text = (listData[0]).ToString();

            XmlSerializer deserializer = new XmlSerializer(typeof(ObjectDetect));
            TextReader reader = new StreamReader(@"c:/users/stradvision/source/repos/DataManagementTool/test.xml");

            ObjectDetect overview = (ObjectDetect)deserializer.Deserialize(reader);
            listData.Add((ObjectDetect)overview);
            reader.Close();

            FileCount.Text = (listData[0].Files.FileCount).ToString();

        }
    }
}

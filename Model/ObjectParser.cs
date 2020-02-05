using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SV_final.ViewModel;

namespace SV_final.Model
{
    public class ObjectParser
    {
        private MainViewModel _mainViewModel = new MainViewModel();
        public void fileParsing(string filename)
        {
            string[] parsing = filename.Split('.');

            if(parsing[1] == "xml")
            {
                readXml(filename);
            }
            else if(parsing[1] == "txt")
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
            bool check1 = true, check2 = false, check3 = false;

            foreach(string x in lines)
            {
                if (check1)
                {

                }
            }
        }

        private void readXml(string filename)
        {

        }

        private void readJson(string filename)
        {

        }
    }
}

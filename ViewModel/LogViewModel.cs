using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV_final.ViewModel
{
    class LogViewModel :BaseViewModel
    {
        public ObservableCollection<string> LogData { get => _logData; set => _logData = value; }
        private ObservableCollection<string> _logData;

        public LogViewModel()
        {
            LogData = new ObservableCollection<string>();
        }
        public void AddLog(Type VMName, string Origin, string New)
        {
            string[] temp = VMName.ToString().Split(new char[] { '.' });
            string[] method = temp[temp.Length - 1].ToString().Split(new string[] { "ViewModel" }, StringSplitOptions.None);
            LogData.Add(method[0] + ":\t" + Origin + " -> " + New);
        }

        public void AddLog(Type VMName, string Origin)
        {
            string[] temp = VMName.ToString().Split(new char[] { '.' });
            string[] method = temp[temp.Length - 1].ToString().Split(new string[] { "ViewModel" }, StringSplitOptions.None);
            LogData.Add(method[0] + ":\t" + Origin);
        }
        public void FailLog(Type VMName, string Origin, string New)
        {
            string[] temp = VMName.ToString().Split(new char[] { '.' });
            string[] method = temp[temp.Length - 1].ToString().Split(new string[] { "ViewModel" }, StringSplitOptions.None);
            LogData.Add("<<FAIL>> " + method[0] + ":\t" + Origin + " -> " + New);
        }
        public void FailLog(Type VMName, string Origin)
        {
            string[] temp = VMName.ToString().Split(new char[] { '.' });
            string[] method = temp[temp.Length - 1].ToString().Split(new string[] { "ViewModel" }, StringSplitOptions.None);
            LogData.Add("<<FAIL>> " + method[0] + ":\t" + Origin);
        }
    }
}

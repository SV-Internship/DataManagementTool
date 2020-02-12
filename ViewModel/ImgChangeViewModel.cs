using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Drawing;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Input;

namespace SV_final.ViewModel
{
    class ImgChangeViewModel : BaseViewModel
    {
        public ICommand OpenPathCommand { get; private set; }

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

        private ObservableCollection<string> _visibleFileList;

        public ObservableCollection<string> VisibleFileList
        {
            get { return _visibleFileList; }
            set
            {
                _visibleFileList = value;
                OnPropertyChanged("VisibleFileList");
            }
        }

        private string _destpath;

        public string DestPath
        {
            get { return _destpath; }
            set
            {
                _destpath = value;
                OnPropertyChanged("DestPath");
            }
        }

        private string _imgSelect;

        public string ImgSelect
        {
            get { return _imgSelect; }
            set
            {
                _imgSelect = value;
                Console.WriteLine(_imgSelect);
                OnPropertyChanged("ImgSelect");
            }
        }

        public RelayCommand ImgOpenCommand { get; private set; }
        public RelayCommand WholeImgConvertCommand { get; private set; }

        public RelayCommand SelectImgConvertCommand { get; private set; }

        public ImgChangeViewModel()
        {
            FileList = new ObservableCollection<string>();
            VisibleFileList = new ObservableCollection<string>();
            ImgOpenCommand = new RelayCommand(ImgOpen);
            WholeImgConvertCommand = new RelayCommand(WholeImgConvert);
            SelectImgConvertCommand = new RelayCommand(SelectImgConvert);
            OpenPathCommand = new RelayCommand(OpenPath);
        }

        private void ImgOpen()
        {
            string ImgFilePath = "";

            try
            {
                var dialog = new OpenFileDialog();
                dialog.ValidateNames = false;
                dialog.CheckFileExists = false;
                dialog.CheckPathExists = true;
                dialog.FileName = "default";
                dialog.Filter = "All files|*.*|Image files(*.jpg)|*.jpg";
                dialog.ShowDialog();

                string Path = System.IO.Path.GetFullPath(dialog.FileName);
                Path = Path.Replace("default", "").Trim();
                ImgFilePath = Path;

                string[] parser = Path.Split('.');
                Console.WriteLine(Path);

                if(parser.Length == 2)
                {
                    string imgpath = Path;
                    FileList.Add(imgpath);
                    string temp = imgpath;
                    string[] fileparse = temp.Split('\\');
                    VisibleFileList.Add(fileparse[fileparse.Length - 1]);
                }
                
                else
                {
                    string[] parse = Path.Split('.');
                    string[] files = Directory.GetFiles(parse[0]);

                    foreach(string x in files)
                    {
                        FileList.Add(x);
                        string temp = x;
                        string[] fileparse = temp.Split('\\');
                        VisibleFileList.Add(fileparse[fileparse.Length - 1]);
                    }
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine($"An exception occurred from {MethodBase.GetCurrentMethod().Name}");
                Console.WriteLine(ex.ToString());
            }
        }

        private void WholeImgConvert()
        {
            foreach(string x in FileList)
            {
                Image target = Image.FromFile(x);
                string finaldest = DestPath + VisibleFileList[FileList.IndexOf(x)];
                string[] fileparse = finaldest.Split('.');
                Bitmap bmp = new Bitmap(target);
                if (target != null)
                    bmp.Save(DestPath + fileparse[1] + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void SelectImgConvert()
        {
            Image target = Image.FromFile(FileList[VisibleFileList.IndexOf(ImgSelect)]);
            string finaldest = DestPath + VisibleFileList[VisibleFileList.IndexOf(ImgSelect)];
            string[] fileparse = finaldest.Split('.');
            Bitmap bmp = new Bitmap(target);
            if(target != null)
                bmp.Save(DestPath + fileparse[1] + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void OpenPath()
        {
            try
            {
                CommonOpenFileDialog dlg = new CommonOpenFileDialog();
                dlg.IsFolderPicker = true;
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    DestPath = dlg.FileName;
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

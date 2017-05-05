using System;
using System.Collections.Generic;
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
using GOLDENBOX.ViewModel;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace GOLDENBOX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModelParticipantList _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModelParticipantList();
            _viewModel = (ViewModelParticipantList)DataContext;
        }

        private void GetXlsPath(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if(fd.ShowDialog() == true)
            {
                _viewModel.XlsPath = fd.FileName;
            }
        }

        private void GetTopResultPath(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (fd.ShowDialog() == true)
            {
                _viewModel.TopResult = fd.FileName;
            }
        }

        private void GetMiddleResultPath(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (fd.ShowDialog() == true)
            {
                _viewModel.MiddleResult = fd.FileName;
            }
        }

        private void GetLowResultPath(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fd = new Microsoft.Win32.OpenFileDialog();
            fd.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (fd.ShowDialog() == true)
            {
                _viewModel.LowResult = fd.FileName;
            }
        }

        public void GetOutputFolder(object sender,RoutedEventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.ShowNewFolderButton = true;
            var result = fb.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                _viewModel.OutputFolder = fb.SelectedPath;
            }
        }

        public void resetAll(object sender, RoutedEventArgs e)
        {
            _viewModel = new ViewModelParticipantList();
        }
    }
}

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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace WOW_DPS_Upper.Windows
{
    /// <summary>
    /// Логика взаимодействия для wMain.xaml
    /// </summary>
    public partial class wMain : Window
    {
        public wMain()
        {
            InitializeComponent();
        }

        OpenFileDialog filePath = new OpenFileDialog();

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            spAll.Visibility = Visibility.Visible;
            spAll.IsEnabled = true;
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            spAll.Visibility = Visibility.Collapsed;
            spAll.IsEnabled = false;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            tbScale.Text = "0";
            sScale.Value = 0;
        }

        private void sScale_Update(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbScale.Text = Math.Round(sScale.Value, 2).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            filePath.InitialDirectory = "C:\\";
            filePath.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            filePath.FilterIndex = 2;
            filePath.RestoreDirectory = true;

            if (filePath.ShowDialog() == true)
            {
                // Set the path of specified file
                tbPath.Text = filePath.FileName;
            }
        }
    }
}

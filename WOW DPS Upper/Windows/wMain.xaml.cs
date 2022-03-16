using System;
using System.Collections.Generic;
using System.IO;
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

        // Содержит путь к файлу с логами
        OpenFileDialog filePath = new OpenFileDialog();

        // Содержит разбитый по параметрам файл
        string[][] RowsElements;

        private void Select_Path(object sender, RoutedEventArgs e)
        {
            filePath.InitialDirectory = "C:\\";
            filePath.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            filePath.FilterIndex = 2;
            filePath.RestoreDirectory = true;

            if (filePath.ShowDialog() == true)
            {
                // Указывает путь к файлу в поле tbPath
                tbPath.Text = filePath.FileName;
                RowsElements = null;
                UpdateFile();
            }
        }

        /// <summary>
        /// Открывает указанный файл и парсит ники игроков
        /// </summary>
        private void UpdateFile()
        {
            if (RowsElements == null)
            {
                // Открывает указанный файл
                var fileStream = filePath.OpenFile();

                // Создает переменные для хранения данных файла
                string RawFile = string.Empty;
                string[] FileRows;

                // Считывает весь файл в переменную
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    RawFile = reader.ReadToEnd();
                }

                // Разделяем файл на строки
                FileRows = RawFile.Split('\n');

                // Здесь будут храниться параметры, перечисленные через запятую в строке
                RowsElements = new string[FileRows.Length][];

                // Разделяем каждую строку на элементы
                for (int i = 0; i < FileRows.Length; i++)
                {
                    RowsElements[i] = FileRows[i].Split(',');
                }

                if (RowsElements?[0]?.Length == null || RowsElements[0].Length < 4 || (RowsElements[0][2] != "ADVANCED_LOG_ENABLED" && RowsElements[0][3] != "1"))
                {
                    MessageBox.Show("Данный файл не является логом или у вас не включена расширенная запись логов!", "Ошибка открытия файла", MessageBoxButton.OK, MessageBoxImage.Error);
                    filePath = new OpenFileDialog();
                    tbPath.Text = string.Empty;
                }
            }
            List<string> Players = new List<string>();

            for (int i = 3; i < RowsElements.Length-1; i++)
            {
                if (Players.IndexOf(RowsElements[i][2]) == -1)
                {
                    Players.Add(RowsElements[i][2]);
                }
            }

            cbName.ItemsSource = Players;
        }
    }
}

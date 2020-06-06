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
using System.Diagnostics;
using System.Windows.Controls.Ribbon;
using System.ComponentModel;

namespace TaskManager
{
    public partial class MainWindow : RibbonWindow
    {
        public List<Proc> ItemsList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            LoadItems();
        }

        void LoadItems()
        {
            //processListView.Items.Clear();
            List<Proc> listProc = new List<Proc>();

            foreach (Process process in Process.GetProcesses())
            {
                listProc.Add(new Proc() { Id = process.Id, Name = process.ProcessName, Size = process.PeakVirtualMemorySize64 });
            }
            
            processListView.ItemsSource = listProc;
            
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(processListView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private void StartProcessRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("calc");
            MessageBox.Show("Запущен калькулятор.");
        }

        private void SerchRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != "")
            {
                for(int i = processListView.Items.Count - 1; i>=0; i--)
                {
                    Proc item = new Proc();
                    item = (Proc)processListView.Items[i];
                    if(item.Name.ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        MessageBox.Show("Данный процесс запущен. Реализовать выделение данного процесса в списке не удалось... В WPF не работает метод SelectedItem.");
                        break;
                    }
                }
            }
        }

        private void ColorGrayRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            grid.Background = new SolidColorBrush(Colors.Gray);
            RibbonWin.Background = new SolidColorBrush(Colors.Gray);
            processListView.Background = new SolidColorBrush(Colors.Gray);
            footer.Background = new SolidColorBrush(Colors.Gray);
        }

        private void ColorGreenRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            grid.Background = new SolidColorBrush(Colors.Green);
            RibbonWin.Background = new SolidColorBrush(Colors.Green);
            processListView.Background = new SolidColorBrush(Colors.Green);
            footer.Background = new SolidColorBrush(Colors.Green);
        }

        private void ColorBlueRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            grid.Background = new SolidColorBrush(Colors.Blue);
            RibbonWin.Background = new SolidColorBrush(Colors.Blue);
            processListView.Background = new SolidColorBrush(Colors.Blue);
            footer.Background = new SolidColorBrush(Colors.Blue);
        }

        private void ColorRedRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            grid.Background = new SolidColorBrush(Colors.Red);
            RibbonWin.Background = new SolidColorBrush(Colors.Red);
            processListView.Background = new SolidColorBrush(Colors.Red);
            footer.Background = new SolidColorBrush(Colors.Red);
        }

        private void ProcessKillButton_Click(object sender, RoutedEventArgs e)
        {
            string name = (processListView.SelectedItem as Proc).Name;
            Process[] process = Process.GetProcesses();

            foreach(Process i in process)
            {
                if (i.ProcessName.ToLower().Contains(name.ToLower()))
                {
                    i.Kill();
                }
            }

            LoadItems();
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            var item = sender as ListViewItem;

            try
            {
                string name = (processListView.SelectedItem as Proc).Name;
                Process process = Process.GetProcessesByName(name)[0];

                string info = String.Format(
                    "Процесс: {0} \n" +
                    "Память: {1} \n" +
                    "Базовый приоритет: {2} \n" +
                    "Дата и время процесса: {3} \n" +
                    "Полное время процесса: {4:T}",
                    process.ProcessName,
                    process.VirtualMemorySize64,
                    process.BasePriority,
                    process.StartTime,
                    (DateTime.Now - process.StartTime).ToString());
                textBlock1.Text = info;
            }

            catch
            {
                textBlock1.Text = "Информация отсутствует.";
            }
            



            //process.StartInfo.FileName = "calc";
            //process.StartInfo.UseShellExecute = false;
            //process.StartInfo.Arguments = "/all";
            //process.StartInfo.RedirectStandardOutput = true;
            //process.Start();
            //textBlock1.Text = process.StandardOutput.ReadToEnd();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}

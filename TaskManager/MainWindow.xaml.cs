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
            processListView.Items.Clear();

            foreach (Process process in Process.GetProcesses())
            {
                Proc item = new Proc();
                item.Id = process.Id;
                item.Name = process.ProcessName;
                item.Size = process.VirtualMemorySize64;

                processListView.Items.Add(item);
            }
        }

        private void StartProcessRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SerchRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != null)
            {
                for(int i = processListView.Items.Count - 1; i>=0; i--)
                {
                    ListView item = new ListView();
                    item = (ListView)processListView.Items[i];
                    if(item.Name.ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        item.Background =new SolidColorBrush(Color.FromRgb(125, 125,55));
                    }
                }
            }
        }

        private void ColorGrayRibbonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ColorGreenRibbonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ColorBlueRibbonButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ColorRedRibbonButton_Click(object sender, RoutedEventArgs e)
        {

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

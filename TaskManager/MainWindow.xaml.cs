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
            Process.Start("calc");
            LoadItems();
        }

        private void SerchRibbonButton_Click(object sender, RoutedEventArgs e)
        {
            string name = (processListView.SelectedItem as Proc).Name;
            Process process = Process.GetProcessesByName(name)[0];

            string info = (DateTime.Now - process.StartTime).TotalSeconds.ToString();
            textBlock1.Text = info;

            //process.StartInfo.FileName = "calc";
            //process.StartInfo.UseShellExecute = false;
            //process.StartInfo.Arguments = "/all";
            //process.StartInfo.RedirectStandardOutput = true;
            //process.Start();
            //textBlock1.Text = process.StandardOutput.ReadToEnd();
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
                    i.Kill();
            }

            LoadItems();
        }
    }
}

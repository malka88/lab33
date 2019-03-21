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

namespace lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime date;
        System.Windows.Threading.DispatcherTimer Timer;

        public MainWindow()
        {
            InitializeComponent();
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(dispatcherTimer_Tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (sch.IsChecked == true)
            {
                string str = lb.SelectedValue as string;
                TimeSpan ts = list[str] - DateTime.Now;
                sl.Content = ts.Seconds;
            }
            else
            {
                sl.Content = "";
            }

            if (mch.IsChecked == true)
            {
                string str = lb.SelectedValue as string;
                TimeSpan ts = list[str] - DateTime.Now;
                ml.Content = ts.Minutes;
            }
            else
            {
                ml.Content = "";
            }

            if (hch.IsChecked == true)
            {
                string str = lb.SelectedValue as string;
                TimeSpan ts = list[str] - DateTime.Now;
                hl.Content = ts.Hours;
            }
            else
            {
                hl.Content = "";
            }

            if (dch.IsChecked == true)
            {
                string str = lb.SelectedValue as string;
                TimeSpan ts = list[str] - DateTime.Now;
                dl.Content = ts.Days;
            }
            else
            {
                dl.Content = "";
            }
        }

        private void hch_Checked(object sender, RoutedEventArgs e)
        {


        }

        private void mch_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void sch_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void dch_Checked(object sender, RoutedEventArgs e)
        {

        }

        public Dictionary<string, DateTime> list = new Dictionary<string, DateTime>();

        private void create_Click(object sender, RoutedEventArgs e)
        {
            Window1 create_timer = new Window1();
            if (create_timer.ShowDialog() == true)
            {
                date = new DateTime(create_timer.cal.SelectedDate.Value.Year, create_timer.cal.SelectedDate.Value.Month, create_timer.cal.SelectedDate.Value.Day, int.Parse(create_timer.tH.Text), int.Parse(create_timer.tM.Text), int.Parse(create_timer.tS.Text));
                if (date < DateTime.Now)
                {
                    MessageBox.Show("Вы ввели дату из прошлого!");
                }
                else
                {
                    lb.Items.Add(create_timer.tText.Text);
                    list.Add(create_timer.tText.Text, date);
                }
            }
        }

        private void look_Click(object sender, RoutedEventArgs e)
        {
            string str = lb.SelectedValue as string;
            TimeSpan ts = list[str] - DateTime.Now;
            MessageBox.Show("Дни: " + ts.Days + " Время: " + ts.Hours + ":" + ts.Minutes + ":" + ts.Seconds);
        }

        private void mo_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            dlg.ShowDialog();

            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(dlg.FileName);
            while ((line = file.ReadLine()) != null)
            {
                string name = line;
                lb.Items.Add(line);
                lb.Items.Add(name);
                line = file.ReadLine();
                DateTime dt = DateTime.Parse(line);
                if(dt < DateTime.Now)
                {
                    MessageBox.Show("В файле содержится дата из прошлого!");
                }
                else
                {
                    lb.Items.Add(name);
                    list.Add(name, dt);
                }
            }

            file.Close();
        }

        private void ms_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            dlg.ShowDialog();

            List<string> lines = new List<string>();

            using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(dlg.FileName))
            {
                foreach (string line in lb.Items)
                {
                    outputFile.WriteLine(line.ToString());
                    DateTime dt = list[line];
                    outputFile.WriteLine(dt.ToString());
                }
            }
        }
    }
}

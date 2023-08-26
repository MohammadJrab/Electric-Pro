using DocumentFormat.OpenXml.Bibliography;
using MaterialDesignThemes.Wpf;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Button = System.Windows.Controls.Button;

namespace ElectricPro.View.On_Off
{
    /// <summary>
    /// Interaction logic for on_off.xaml
    /// </summary>
    public partial class on_off : Page
    {
        public on_off()
        {

            InitializeComponent();

            var mainWindow2 = Application.Current.MainWindow as MainWindow;

            taskListView.ItemsSource = mainWindow2.myDataTable.DefaultView;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {


            getStations();

        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


        }


        public void getStations()
        {
            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    var command = new OracleCommand("SELECT station FROM stations", connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            FilterByCombo.Items.Add(reader.GetString(0));
                        });
                    }

                }
            }
            // "خطأ في الاتصال بقاعدة البيانات"
            catch (OracleException ex) { DisplatMessage(); }

        }

        public void DisplatMessage()
        {
            int milliseconds = 8000;
            Dispatcher.Invoke(() => SnackbarFive.IsActive = true);

            Thread.Sleep(milliseconds);
            Dispatcher.Invoke(() => SnackbarFive.IsActive = false);



        }
        private void FilterByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                FilterByComboExit.Items.Clear();
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    var command = new OracleCommand("SELECT exit FROM Exits where station='" + FilterByCombo.SelectedItem + "' ", connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        FilterByComboExit.Items.Add(reader.GetString(0));

                    }

                }
                FilterByComboExit.SelectedIndex = 0;
            }
            // "خطأ في الاتصال بقاعدة البيانات"
            catch (OracleException ex) { DisplatMessage(); }


        }


        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskPopup.IsOpen = true;
        }

        private void CancelTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskPopup.IsOpen = false;
            startDate.Text = "";
            endDate.Text = "";

        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            dataRowView.Row.Delete();
        }

      

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;

            mainWindow.myDataTable.Rows.Add(FilterByCombo.SelectedItem.ToString(), FilterByComboExit.SelectedItem.ToString(), startDate.Text, endDate.Text);
            AddTaskPopup.IsOpen = false;
            startDate.Text = "";
            endDate.Text = "";
        }
    }

}
    


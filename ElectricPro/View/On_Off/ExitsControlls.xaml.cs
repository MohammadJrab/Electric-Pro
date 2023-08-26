using ControlzEx.Standard;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using ElectricPro.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
using GridView = System.Windows.Controls.GridView;
using ListView = System.Windows.Controls.ListView;

namespace ElectricPro.View.On_Off
{
    /// <summary>
    /// Interaction logic for ExitsControlls.xaml
    /// </summary>
    public partial class ExitsControlls : Page
    {
        List<MainDetails> data = new List<MainDetails>();
        public DataTable myDataTable = new DataTable();

        public ExitsControlls()
        {
            InitializeComponent();
            myDataTable.Columns.Add("Station");
            myDataTable.Columns.Add("Exit");
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            GetExits();
            ListView.ItemsSource =myDataTable.DefaultView;
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
        public void DisplatMessage()
        {
            int milliseconds = 8000;
            Dispatcher.Invoke(() => SnackbarFive.IsActive = true);

            Thread.Sleep(milliseconds);
            Dispatcher.Invoke(() => SnackbarFive.IsActive = false);



        }

        public void getStations()
        {
            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    data.Clear();

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


        private void FilterByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetExits();
        }

        public void GetExits()
        {

            try
            {
                myDataTable.Rows.Clear();

                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    var command = new OracleCommand("SELECT station,exit FROM Exits where station='" + FilterByCombo.SelectedItem + "' ", connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRow newRow = myDataTable.NewRow();
                        newRow["Station"] = reader["Station"].ToString();
                        newRow["Exit"] = reader["Exit"].ToString();
                        myDataTable.Rows.Add(newRow);


           
                    }

                }
            }
            // "خطأ في الاتصال بقاعدة البيانات"
            catch (OracleException ex) { DisplatMessage(); }


        }


        public void changeState(string state,string exit)
        {

            try
            {
                    string sql = $"UPDATE Ex_Status SET STATE = '" + state + "' where exit = '" + exit+ "'";

                    using (var connection = new OracleConnection(Connection.ConnectionString))
                    {
                        using (OracleCommand command = new OracleCommand(sql, connection))
                        {


                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            connection.Close();

                            if (rowsAffected == 1)
                            {
                     

                            }
                            else
                            {

                 
                            }
                        }
                    }
                
         

            }
            catch (OracleException ex)
            {

            }


        }

        private void TurnOn_Click(object sender, RoutedEventArgs e)
        {
            DateTime Nowdt = DateTime.Now;

          string  formattedDate = Nowdt.ToString("MM/dd/yyyy HH:mm:ss");
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            DoTaskAsync(dataRowView.Row[1].ToString(), "1", formattedDate);


        }


        private void TurnOff_Click(object sender, RoutedEventArgs e)
        {
            
            DateTime Nowdt = DateTime.Now;

            string formattedDate = Nowdt.ToString("MM/dd/yyyy HH:mm:ss");
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            DoTaskAsyncOff(dataRowView.Row[1].ToString(), "0", formattedDate);

        }



        public async Task DoTaskAsync(string exit, string state, string startDateTime)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            try
            {
                await Task.Run(async () =>
            {



                this.Dispatcher.Invoke(() =>
                {
                    mainWindow.MessageTextturnAlarm.Content = "جاري محاولة تشغيل مخرج " + exit + " ";
                    mainWindow.turnAlarm.IsActive = true;
                });
                changeState(state, exit);
                await Task.Delay(60000);

                if (LastStateN(exit, startDateTime) == true)
                {
                    if (LastTable(exit) > 10)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            mainWindow.MessageTextStatusSeccess.Content = "تم تشغيل مخرج " + exit + " ";
                            mainWindow.SnackbarStatusSeccess.IsActive = true;
                        });


                    }
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {

                        mainWindow.MessageTextStatus.Content = "فشل تشغيل مخرج " + exit + " ";
                        mainWindow.SnackbarStatus.IsActive = true;
                    });
                }
            


            });
        }  catch (Exception ex)
            {
                this.Dispatcher.Invoke(() =>
                {

                    mainWindow.MessageTextStatus.Content = "فشل تشغيل مخرج " + exit + " ";
                    mainWindow.SnackbarStatus.IsActive = true;
                });
            }
        }

        public async Task DoTaskAsyncOff(string exit, string state, string startDateTime)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            try
            {
                await Task.Run(async () =>
            {


                this.Dispatcher.Invoke(() =>
                {
                    mainWindow.MessageTextturnAlarm.Content = "جاري محاولة إيقاف مخرج " + exit + " ";
                    mainWindow.turnAlarm.IsActive = true;
                });
                changeState(state, exit);
                await Task.Delay(60000);

                if (LastStateN(exit, startDateTime) == true)
                {

                    this.Dispatcher.Invoke(() =>
                    {
                        mainWindow.MessageTextStatusSeccess.Content = "تم تشغيل مخرج " + exit + " ";
                        mainWindow.SnackbarStatusSeccess.IsActive = true;
                    });



                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {

                        mainWindow.MessageTextStatus.Content = "فشل تشغيل مخرج " + exit + " ";
                        mainWindow.SnackbarStatus.IsActive = true;
                    });
                }
            



            });
            }
                 catch (Exception ex)
            {
                this.Dispatcher.Invoke(() =>
                {

                    mainWindow.MessageTextStatus.Content = "فشل تشغيل مخرج " + exit + " ";
                    mainWindow.SnackbarStatus.IsActive = true;
                });
            }
        }
        private int LastTable(string exitName)
        {
            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    DateTime sDate = DateTime.Now;
                    int amper = 0;
                    string formattedDate = sDate.ToString("MM/dd/yyyy HH:mm:ss");
                    string sql = "SELECT * FROM ( SELECT Amper FROM Details where Exit='" + exitName + "' and dateTime BETWEEN '" + formattedDate.Substring(0, 11) + "00:00:01" + "' and '" + formattedDate.Substring(0, 11) + "23:59:59" + "' ORDER BY DateTime DESC ) WHERE ROWNUM = 1 ";
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(sql, connection);

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        amper = int.Parse(reader["AMPER"].ToString());


                    }
                    return amper;

                    connection.Close();
                }
            }
            catch (Exception ex) { return 0; }


        }
        private bool LastStateN(string exitName, string startTime)
        {
            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    DateTime sDate = DateTime.Now;
                    string formattedDate = sDate.ToString("MM/dd/yyyy HH:mm:ss");
                    string sql = "SELECT * FROM ( SELECT STATUS,Amper FROM Details where Exit='" + exitName + "' and dateTime BETWEEN '" + startTime + "' and '" + formattedDate.Substring(0, 11) + "23:59:59" + "' ORDER BY DateTime DESC ) WHERE ROWNUM = 1 ";
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(sql, connection);

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader["STATUS"].ToString() == "off" && int.Parse(reader["AMPER"].ToString()) <= 10)

                        {
                            return true;
                            connection.Close();

                        }
                        else { return false; }
                    }
                    else { return false; }




                }

            }
            catch (Exception ex) { return false; }


        }


        private void MessageTextStatusSeccess_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarStatusSeccess.IsActive = false;


        }

        private void MessageTextStatus_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarStatus.IsActive = false;

        }

        private void MessageText_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarMes.IsActive = false;
        }

        private void MessageTextturnAlarm_ActionClick(object sender, RoutedEventArgs e)
        {
            turnAlarm.IsActive = false;
        }
    }
}

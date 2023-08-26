
using ElectricPro.Models;
using ElectricPro.View.Reports;
using MaterialDesignThemes.Wpf;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectricPro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer timerApp;

        public MainWindow()
        {
            InitializeComponent();
            
          
  

            timerApp = new Timer(1000); // 1000 milliseconds = 1 second
            timerApp.Elapsed += new ElapsedEventHandler(OnTimerElapsed);
            timerApp.AutoReset = true;
            timerApp.Enabled = true;
            //play();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }
        
          

        //}
        string formattedDate="";
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime Nowdt = DateTime.Now;

             formattedDate = Nowdt.ToString("MM/dd/yyyy HH:mm:ss");

            if (myDataTable.Rows.Count > 0)
            {

                for (int i = myDataTable.Rows.Count - 1; i >= 0; i--)
                {

                    if (myDataTable.Rows[i][2].ToString() == formattedDate)
                    {

                        _ = DoTaskAsync(myDataTable.Rows[i][1].ToString(), myDataTable.Rows[i][3].ToString(), formattedDate);


                    }

                }
            }
        }

        public async Task DoTaskAsync(string exit, string endDateTime, string startDateTime)
        {
            try
            {
                await Task.Run(async () =>
            {


                this.Dispatcher.Invoke(() =>
                {
                    MessageTextStatusSeccess.Content = "جاري محاولة تشغيل مخرج " + exit + " ";
                    SnackbarStatusSeccess.IsActive = true;
                });
                updateOrcl(exit);
                //await Task.Delay(60000);

                //if (LastStateN(exit, startDateTime) == true) {
                //if (LastTable(exit) > 10)
                //{
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextStatusSeccess.Content = "تم تشغيل مخرج " + exit + " ";
                    SnackbarStatusSeccess.IsActive = true;
                });
                DateTime Nowdt = DateTime.Now;

                formattedDate = Nowdt.ToString("MM/dd/yyyy HH:mm:ss");
                DateTime startDate = DateTime.ParseExact(formattedDate, "MM/dd/yyyy HH:mm:ss", null);
                DateTime endDate = DateTime.ParseExact(endDateTime, "MM/dd/yyyy HH:mm:ss", null);
                Timer timer = new Timer();
                timer.Interval = (endDate - startDate).TotalMilliseconds;
                timer.Elapsed += (sender, e) => updateStateOff(exit, endDateTime); ;
                timer.AutoReset = false;
                timer.Enabled = true;

                //}
            });
                //else
                //{
                //    this.Dispatcher.Invoke(() =>
                //    {
                //        for (int i = myDataTable.Rows.Count - 1; i >= 0; i--)
                //        {


                //            if (myDataTable.Rows[i][1].ToString() == exit && myDataTable.Rows[i][3].ToString() == endDateTime)
                //            {
                //                myDataTable.Rows[i].Delete();
                //            }
                //        }
                //        MessageTextStatus.Content = "فشل تشغيل مخرج " + exit + " ";
                //        SnackbarStatus.IsActive = true;
                //    });
                //}
            //});

            }



            catch (Exception ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    for (int i = myDataTable.Rows.Count - 1; i >= 0; i--)
                    {


                        if (myDataTable.Rows[i][1].ToString() == exit && myDataTable.Rows[i][3].ToString() == endDateTime)
                        {
                            myDataTable.Rows[i].Delete();
                        }
                    }
                    MessageTextStatus.Content = "فشل تشغيل مخرج " + exit + " ";
                    SnackbarStatus.IsActive = true;
                });
            }

        }


  



        public void updateOrcl(string exit)
        {
            string sql = $"UPDATE ex_status SET state = 1 WHERE EXIT = '" + exit + "'";

            using (var connection = new OracleConnection(Connection.ConnectionString))
            {
                using (OracleCommand command = new OracleCommand(sql, connection))
                {


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    //if (rowsAffected == 1)
                    //{
                    //    MessageTextStatus.Content = "تم تشغيل المخرج '" + exit + "'";

                    //    MessageBox.Show("تم تعديل الحالة بنجاح", "الحالة", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    //}
                    //else
                    //{
                    //    MessageTextStatus.Content = "فشل تشغيل المخرج '" + exit + "'";
                    //    MessageBox.Show("! قشل تعديل الحالة", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}
                }
            }

        }
        public void updateStateOff(string exit,string EndTime)
        {
            string sql = $"UPDATE ex_status SET state = 0 WHERE EXIT = '" + exit + "'";

            using (var connection = new OracleConnection(Connection.ConnectionString))
            {
                using (OracleCommand command = new OracleCommand(sql, connection))
                {


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    this.Dispatcher.Invoke(() =>
                    {
                        for (int i = myDataTable.Rows.Count - 1; i >= 0; i--)
                        {
                         

                            if (myDataTable.Rows[i][1].ToString() == exit && myDataTable.Rows[i][3].ToString() == EndTime)
                            {
                                myDataTable.Rows[i].Delete();
                            }
                        }
                        MessageTextStatus.Content = "تم إيقاف مخرج " + exit + "";
                        SnackbarStatus.IsActive = true;
                    });
                    

                }
            }

        }

        private int LastTable(string exitName)
        {
            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    DateTime sDate = DateTime.Now;
                    int amper=0;
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
                    string sql = "SELECT * FROM ( SELECT Amper FROM Details where Exit='" + exitName + "' and dateTime BETWEEN '" + startTime+ "' and '" + formattedDate.Substring(0, 11) + "23:59:59" + "' ORDER BY DateTime DESC ) WHERE ROWNUM = 1 ";
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(sql, connection);
                    
                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        if  (int.Parse(reader["AMPER"].ToString()) > 10)

                        {
                            return true;
                            connection.Close();

                        }
                        else {return false; }
                    }
                    else { return false; }




                }

            }
            catch (Exception ex) { return false; }


        }



        public DataTable myDataTable = new DataTable();

   
        //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        base.OnMouseLeftButtonDown(e);
        //        DragMove();
        //    }
        //    catch (Exception) { }
        //}


        private void Close_App_Click(object sender, RoutedEventArgs e)
        {
            Close();

        }

 

        private void Min_App_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void ExitDetailsM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Reports/ExitsStateReport.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void MainScreen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/MainP.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ExitDurM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Reports/ExitDetialsReport.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AmperChartN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Reports/AmperCharts.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void StaionDurM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Reports/stationDurReport.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AddExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Exits/Add_Exits.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void EditExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Exits/Edit_Exits.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void DeleteExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Exits/Remove_Exits.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void DeleteStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Stations/Remove_Stations.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void EditStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Stations/Edit_Stations.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Stations/Add_Stations.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void ExitsNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Exits/ExitsState.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void On_Off_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/On_Off/on_off.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void MessageText_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarMes.IsActive = false;
        }

        private void MessageTextStatus_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarStatus.IsActive = false;
        


        }

    

        private void MessageTextStatusSeccess_ActionClick_1(object sender, RoutedEventArgs e)
        {
            SnackbarStatusSeccess.IsActive = false;
        }

        private void On_Off_manual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/On_Off/ExitsControlls.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void EditSn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/Exits/EditSn.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void MessageTextturnAlarm_ActionClick(object sender, RoutedEventArgs e)
        {
            turnAlarm.IsActive = false;
        }

        private void usersM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                myFrame.Navigate(new System.Uri("/View/usersMann.xaml", UriKind.Relative));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void WindwosM_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = this;
            myDataTable.Columns.Add("StationName");
            myDataTable.Columns.Add("ExitName");
            myDataTable.Columns.Add("StartDateTime");
            myDataTable.Columns.Add("EndDateTime");

            if (users.permission != "مدير")
            {
                usersM.IsEnabled = false;
                if (users.TurnMenu == "0")
                {
                    turnMenu.IsEnabled = false;
                }
                if (users.ExitsMenu == "0")
                {
                    ExitsMenu.IsEnabled = false;
                }
                if (users.StationMenu == "0")
                {
                    StationsMenu.IsEnabled = false;
                }
                if (users.StationDurM == "0")
                {
                    StaionDurM.IsEnabled = false;
                }
                if (users.ExitDurM == "0")
                {
                    ExitDurM.IsEnabled = false;
                }
                if (users.ExitDetailsM == "0")
                {
                    ExitDetailsM.IsEnabled = false;
                }
                if (users.AmperChartM == "0")
                {
                    AmperChartN.IsEnabled = false;
                }

            }
        }
    }
}

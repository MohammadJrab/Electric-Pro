using ClosedXML.Excel;
using ControlzEx.Standard;
using ElectricPro.Models;
using LiveCharts.Maps;
using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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

namespace ElectricPro.View.Exits
{
    /// <summary>
    /// Interaction logic for ExitsState.xaml
    /// </summary>
    public partial class ExitsState : Page
    {
        public ExitsState()
        {
            InitializeComponent();
            GetStations();
          




        }
        List<string> ExitsList = new List<string>();
        List<string> stationsList = new List<string>();


        /// <summary>
        /// Get Stations List 
        /// </summary>
        /// 
        public List<string> GetStations()
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
                        //FilterByCombo.Items.Add(reader.GetString(0));
                        stationsList.Add(reader.GetString(0));
                    }
                    return stationsList;

                }
            }
            // "خطأ في الاتصال بقاعدة البيانات"
            catch (OracleException ex) { MessageBox.Show(ex.Message, "تحذير", MessageBoxButton.OK, MessageBoxImage.Error); return null; }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stationName"></param>



        public List<string> getExit(string stationName)
        {
            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    var command = new OracleCommand("SELECT exit FROM Exits where station='" + stationName + "' ", connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ExitsList.Add(reader.GetString(0));

                    }
                    return ExitsList;
                }

            }
            // "خطأ في الاتصال بقاعدة البيانات"
            catch (OracleException ex) { MessageBox.Show(ex.Message, "تحذير", MessageBoxButton.OK, MessageBoxImage.Error); return null; }


        }

        /// <summary>
        /// variable for Table
        /// </summary>
        String oldState;
        String oldR;
        String oldS;
        String oldT;
        public DataTable dt = new DataTable();
        String dat1 = "";
        String dat2 = "";
        public DataTable dtMain = new DataTable();
        public DataTable LastDt = new DataTable();


        string dtTm = "";

        private void refreshTable(string exitName)
        {

            try
            {
                //if (ExitsList != null && ExitsList.Count > 0)
                //{

                    using (var connection = new OracleConnection(Connection.ConnectionString))
                    {
                        //dt.Rows.Clear();
                        //dt.Clear();
                        //dt.Columns.Clear();
                        //dtMain.Rows.Clear();
                        //dtMain.Clear();
                        //dtMain.Columns.Clear();
                        this.Dispatcher.Invoke(() =>
                        {
                            DateTime sDate = DateTime.Now;

                            string formattedDate = sDate.ToString("MM/dd/yyyy HH:mm:ss");

                            string sql = "SELECT  Amper,Status,R,S,T,dateTime,exit,station  FROM Details where Exit='"+exitName+"' and dateTime BETWEEN '" + formattedDate.Substring(0, 11) + "00:00:01" + "' and '" + formattedDate.Substring(0, 11) + "23:59:59" + "' ORDER BY datetime asc";
                            connection.Open();
                            OracleCommand cmd = new OracleCommand(sql, connection);
                            OracleDataReader reader = cmd.ExecuteReader();
                            OracleDataAdapter adapter = new OracleDataAdapter(cmd);

                            String dat1 = "";
                            String dat2 = "";
                            string station = "";
                            string exit = "";
                            while (reader.Read())
                            {

                                DataRow newRow = dt.NewRow();

                                int Amper = int.Parse(reader["AMPER"].ToString());
                                if (Amper > 30)
                                {
                                    newRow["الحالة"] = "يعمل";

                                }
                                else
                                    if (Amper <= 30)
                                {
                                    if (reader.GetString(1) == "on")
                                    {
                                        newRow["الحالة"] = "محجوز";
                                    }


                                    else if (reader.GetString(1) == "off")
                                    {
                                        newRow["الحالة"] = "لايعمل";
                                    }
                                }



                                ///////////

                                newRow["AMPER"] = reader["AMPER"].ToString();
                                newRow["المخرج"] = reader["exit"].ToString();
                                newRow["المحطة"] = reader["station"].ToString();
                                exit = reader["exit"].ToString();
                                station = reader["station"].ToString();

                                ///////////

                                if (reader.GetString(2) == "on")
                                {
                                    newRow["R"] = "يعمل";
                                }


                                else if (reader.GetString(2) == "off")
                                {
                                    newRow["R"] = "لا يعمل";
                                }


                                if (reader.GetString(3) == "on")
                                {
                                    newRow["S"] = "يعمل";
                                }


                                else if (reader.GetString(3) == "off")
                                {
                                    newRow["S"] = "لا يعمل";
                                }
                                ////////////

                                if (reader.GetString(4) == "on")
                                {
                                    newRow["T"] = "يعمل";
                                }


                                else if (reader.GetString(4) == "off")
                                {
                                    newRow["T"] = "لا يعمل";
                                }
                                ////////////

                                newRow["DATETIME"] = reader["DATETIME"].ToString();
                                DateTime stDate = DateTime.Now;

                                string nowT = stDate.ToString("MM/dd/yyyy HH:mm:ss");
                                dtTm = reader["DATETIME"].ToString();

                                DateTime startDate = DateTime.ParseExact(dtTm, "MM/dd/yyyy HH:mm:ss", null);
                                DateTime endDate = DateTime.ParseExact(nowT, "MM/dd/yyyy HH:mm:ss", null);
                                TimeSpan timeElapsed = endDate - startDate;

                                double total = timeElapsed.TotalHours;
                                TimeSpan timeHour = TimeSpan.FromHours(total);

                                int hours = (int)total;
                                int minutes = (int)((total - hours) * 60);
                                int seconds = (int)(((total - hours) * 60 - minutes) * 60);

                                string formattedTimes = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

             
                                string resultTime = formattedTimes;
                                newRow["مدة التشغيل"] = resultTime;

                                //dt.Rows.Add(newRow);

                                /////////////
                                if (oldState != newRow["الحالة"].ToString())
                                {

                                    dt.Rows.Add(newRow);
                                    oldState = newRow["الحالة"].ToString();
                                    oldR = newRow["R"].ToString();
                                    oldS = newRow["S"].ToString();
                                    oldT = newRow["T"].ToString();




                                }

                                ///////////////

                            }
                            connection.Close();

                            //GetTotalHour(station, exit);
 
                        });
                    }

                //}
                //else
                //{

                //    System.Windows.MessageBox.Show("يرجى إختيار المخرج", "تحذير", MessageBoxButton.OK, MessageBoxImage.Error);


                //}

            }
            catch (OracleException ex) { System.Windows.MessageBox.Show($"{ex.Message}", "تحذير", MessageBoxButton.OK, MessageBoxImage.Error); }

        }


        private void LastTable(string stationName,string exitName,string Laststate, string startTime, string onTime)
        {
            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    DateTime sDate = DateTime.Now;

                    string formattedDate = sDate.ToString("MM/dd/yyyy HH:mm:ss");
                    string sql = "SELECT * FROM ( SELECT Amper,DateTime FROM Details where Exit='" + exitName + "' and dateTime BETWEEN '" + formattedDate.Substring(0, 11) + "00:00:01" + "' and '" + formattedDate.Substring(0, 11) + "23:59:59" + "' ORDER BY DateTime DESC ) WHERE ROWNUM = 1 ";
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(sql, connection);

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        DataRow newRow = LastDt.NewRow();
                        newRow["Amper"] = reader["AMPER"].ToString();
                        newRow["LastUpdate"] = reader["DATETIME"].ToString();
                        if (Laststate == "يعمل")
                        {
                            newRow["StartTime"] = startTime;
                        }
                        else
                        {
                            newRow["StartTime"] = "--:--:--";


                        }
                        if (Laststate == "يعمل")
                        {
                            newRow["OnTime"] = onTime;
                        }
                        else
                        {
                            newRow["OnTime"] = "--:--:--";
                        }

                        newRow["State"] = Laststate;
                        newRow["Exit"] = exitName;
                        newRow["Station"] = stationName;

                
                        LastDt.Rows.Add(newRow);


                        connection.Close();
                 




                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);  }


        }



        //MainDetails[] mainDetails;
        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
           
                ///
                oldState = "";
                oldR = "";
                oldS = "";
                oldT = "";
  

                /// Clear Table
                dt.Columns.Clear();
                dtMain.Columns.Clear();
                LastDt.Columns.Clear();
                dt.Clear();
                LastDt.Clear();
                dtMain.Clear();

            /// Add coulmn
                dt.Columns.Add("مدة التشغيل");
                dt.Columns.Add("DATETIME");
                dt.Columns.Add("T");
                dt.Columns.Add("S");
                dt.Columns.Add("R");
                dt.Columns.Add("AMPER");
                dt.Columns.Add("الحالة");
                dt.Columns.Add("المخرج");
                dt.Columns.Add("المحطة");

            /// Add coulmn
            LastDt.Columns.Add("LastUpdate");
            LastDt.Columns.Add("StartTime");
            LastDt.Columns.Add("OnTime");
            LastDt.Columns.Add("Amper");
            LastDt.Columns.Add("State");
            LastDt.Columns.Add("Exit");
            LastDt.Columns.Add("Station");

                dtMain.Columns.Add("dir");
                dtMain.Columns.Add("endTime");
                dtMain.Columns.Add("startTime");
                dtMain.Columns.Add("المخرج");
                dtMain.Columns.Add("المحطة");

                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
       

            

        }




        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            stationsList.Clear();

            GetStations();

            if (stationsList.Count > 0)
            {
                for (int i = 0; i < stationsList.Count; i++)
                {
                    getExit(stationsList[i]);
                    for (int j = 0; j < ExitsList.Count; j++)
                    {
                        
                       refreshTable(ExitsList[j]);
            string ex = "";
            string st = "";
            string Laststate = "";
            string startTime = "";
            string onTime = "";
                        if (dt.Rows.Count > 0)
                        {

               for (int s = 0; s<dt.Rows.Count; ++s)
            {
    
                ex = dt.Rows[s][7].ToString();
                st = dt.Rows[s][8].ToString();
                Laststate = dt.Rows[s][6].ToString();
                startTime = dt.Rows[s][1].ToString();
                onTime = dt.Rows[s][0].ToString();
            }
            LastTable(st, ex, Laststate, startTime, onTime);

                        dt.Clear();
                        oldState = "";
                        oldR = "";
                        oldS = "";
                        oldT = "";
                        dat1 = "";
                        dat2 = "";
                        }
                    }
                    ExitsList.Clear();

                }
                this.Dispatcher.Invoke(() =>
                {
                    dataGrid.ItemsSource = LastDt.DefaultView;
                });

                for(int q = 0; q < LastDt.Rows.Count; q++)
                {
                    string station = LastDt.Rows[q][6].ToString();
                    string exit = LastDt.Rows[q][5].ToString();
                    string amper = LastDt.Rows[q][3].ToString();
                    string StartTime = LastDt.Rows[q][1].ToString();
                    string onTime = LastDt.Rows[q][2].ToString();
                    string LastUpdate = LastDt.Rows[q][0].ToString();
                    string status = LastDt.Rows[q][4].ToString();
                    string ColorCard;

                    if (LastDt.Rows[q][4].ToString() == "يعمل")
                    { ColorCard = "Red"; }
                    else if (LastDt.Rows[q][4].ToString() == "لا يعمل")
                    { ColorCard = "Green"; }
                    else { ColorCard = "Orange"; }
             
            
              //     mainDetails = new MainDetails[]
              //{
 
              // new MainDetails(station,exit,amper,StartTime,onTime,status,LastUpdate,ColorCard),
     
              // };


                }
             

            }







        }




     




        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
     
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            enterBtn.IsEnabled = true;


        }





    
    }

}
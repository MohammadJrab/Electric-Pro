using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using ElectricPro.Models;
using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ElectricPro.View.Reports
{
    /// <summary>
    /// Interaction logic for stationDurReport.xaml
    /// </summary>
    public partial class stationDurReport : Page
    {
        public stationDurReport()
        {
            InitializeComponent();
            BackgroundWorker worker = new BackgroundWorker();
            GetStations();

            //FilterByCombo.Items.Add("جميع المحطات");
            if (stationsList.Count > 0)
            {
                FilterByCombo.Items.Clear();
                //stationsList.Add("جميع المحطات");
                FilterByCombo.ItemsSource = stationsList;
                FilterByCombo.SelectedItem="جميع المحطات";

            }

            dataGrid.ItemsSource = dtStations.DefaultView;

        }
        //private void Worker_DoWorkS(object sender, DoWorkEventArgs e)
        //{



        //}

        //private void Worker_ProgressChangedS(object sender, ProgressChangedEventArgs e)
        //{
        //}

        //private void Worker_RunWorkerCompletedS(object sender, RunWorkerCompletedEventArgs e)
        //{


        //}

        public void DisplatMessage()
        {
            int milliseconds = 8000;
            Dispatcher.Invoke(() => SnackbarFive.IsActive = true);

            Thread.Sleep(milliseconds);
            Dispatcher.Invoke(() => SnackbarFive.IsActive = false);



        }
        List<string> ExitsList=new List<string>();
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
            catch (OracleException ex) { DisplatMessage(); return null; }

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
            catch (OracleException ex) { DisplatMessage(); return null; }


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
        public DataTable dtStations = new DataTable();
        private void refreshTable(string exitName)
        {

            try
            {
                if (ExitsList != null && ExitsList.Count > 0)
                {

                    using (var connection = new OracleConnection(Connection.ConnectionString))
                    {
                        dt.Clear();
                        dtMain.Clear();
                        //dt.Columns.Clear();
                        //dtMain.Rows.Clear();
                        //dtMain.Clear();
                        //dtMain.Columns.Clear();
                        this.Dispatcher.Invoke(() =>
                        {
                            string sql = "SELECT  Amper,Status,R,S,T,dateTime,exit,station FROM Details where dateTime BETWEEN '" + startDate.Text + "' and '" + endDate.Text + "' and Exit='" + exitName + "' order by datetime asc  ";
                        connection.Open();
                        OracleCommand cmd = new OracleCommand(sql, connection);
                        OracleDataReader reader = cmd.ExecuteReader();
                        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                        //adapter.Fill(dt);

                        String dat1 = "";
                        String dat2 = "";
                        string station = "";
                        string exit = "";
                        while (reader.Read())
                        {

                            DataRow newRow = dt.NewRow();

                            int Amper = int.Parse(reader["AMPER"].ToString());
                            if (Amper > 10)
                            {
                                newRow["الحالة"] = "يعمل";

                            }
                            else
                                if (Amper <= 10)
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


                            ///////////////
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

                        GetTotalHour(station, exit);
                        });
                    }

                }
                else
                {

                    System.Windows.MessageBox.Show("يرجى إختيار المخرج", "تحذير", MessageBoxButton.OK, MessageBoxImage.Error);


                }

            }
            catch (OracleException ex) { DisplatMessage(); }

        }

        int totalHou = 0;
        int totalMin = 0;
        int totalSec = 0;

        int totalHouTot = 0;
        int totalMinTot = 0;
        int totalSecTot = 0;
        public void GetTotalHour(string station, string exit)
        {

            foreach (DataRow row in dt.Rows)
            {
                var value = row[5];
                var date = row[0];

                if (value == "يعمل")
                {
                    dat1 = date.ToString();
                }
                else if (value == "لايعمل")
                {
                    dat2 = date.ToString();
                    if (dat1 != "" & dat2 != "")
                    {
                        DataRow newRow = dtMain.NewRow();
                        newRow["المخرج"] = exit;
                        newRow["المحطة"] = station;
                        newRow["startTime"] = dat1;


                        newRow["endTime"] = dat2;
                        DateTime sDate = DateTime.ParseExact(dat1, "MM/dd/yyyy HH:mm:ss", null);
                        DateTime eDate = DateTime.ParseExact(dat2, "MM/dd/yyyy HH:mm:ss", null);
                        TimeSpan timeElapsed = eDate - sDate;

                        double total = timeElapsed.TotalHours;
                        TimeSpan timeHour = TimeSpan.FromHours(total);

                        int hours = (int)total;
                        int minutes = (int)((total - hours) * 60);
                        int seconds = (int)(((total - hours) * 60 - minutes) * 60);

                        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

                        totalHou += hours;
                        totalMin += minutes;
                        totalSec += seconds;
                        totalHouTot += hours;
                        totalMinTot += minutes;
                        totalSecTot += seconds;
                        string resultTime = formattedTime;
                        newRow["dir"] = resultTime;



                        dtMain.Rows.Add(newRow);

                    }
                    dat1 = "";
                    dat2 = "";
                    
                }
            }
            if (dtMain.Rows.Count > 0)
            {

 
                if (totalSec >= 60)
                {
                    int extraMinutes = totalSec / 60;
                    totalSec += extraMinutes;
                    totalSec = totalSec % 60;
                }
                if (totalHou >= 60)
                {
                    int extraMinutes = totalSec / 60;
                    totalMin += extraMinutes;
                    totalSec = totalSec % 60;
                }

                if (totalMin >= 60)
                {
                    int extraHours = totalMin / 60;
                    totalHou += extraHours;
                    totalMin = totalMin % 60;
                }
             

                string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", totalHou, totalMin, totalSec);



                DataRow newRow2 = dtStations.NewRow();
                newRow2["المخرج"] = exit;
                newRow2["المحطة"] = station;
                newRow2["المدة"] = formattedTime;
                dtStations.Rows.Add(newRow2);

            }

        }

        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FilterByCombo.SelectedItem == "جميع المحطات")
            {
                ///
                oldState = "";
                oldR = "";
                oldS = "";
                oldT = "";
                totalHou = 0;
                totalMin = 0;
                totalSec = 0;

                 totalHouTot = 0;
                 totalMinTot = 0;
                 totalSecTot = 0;
                /// Clear Table
                dt.Columns.Clear();
                dtMain.Columns.Clear();
                dt.Clear();
                dtMain.Clear();
                dtStations.Columns.Clear();
                dtStations.Clear();

                /// Add coulmn

                dt.Columns.Add("DATETIME");
                dt.Columns.Add("T");
                dt.Columns.Add("S");
                dt.Columns.Add("R");
                dt.Columns.Add("AMPER");
                dt.Columns.Add("الحالة");
                dt.Columns.Add("المخرج");
                dt.Columns.Add("المحطة");

                dtMain.Columns.Add("dir");
                dtMain.Columns.Add("endTime");
                dtMain.Columns.Add("startTime");
                dtMain.Columns.Add("المخرج");
                dtMain.Columns.Add("المحطة");

                dtStations.Columns.Add("المدة");
                dtStations.Columns.Add("المخرج");
                dtStations.Columns.Add("المحطة");

                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
            else
            {

                ///
                oldState = "";
                oldR = "";
                oldS = "";
                oldT = "";
                totalHou = 0;
                totalMin = 0;
                totalSec = 0;
                 totalHouTot = 0;
                 totalMinTot = 0;
                 totalSecTot = 0;

                /// Clear Table
                dt.Columns.Clear();
                dtMain.Columns.Clear();
                dt.Clear();
                dtMain.Clear();
                dtStations.Columns.Clear();
                dtStations.Clear();

                /// Add coulmn

                dt.Columns.Add("DATETIME");
                dt.Columns.Add("T");
                dt.Columns.Add("S");
                dt.Columns.Add("R");
                dt.Columns.Add("AMPER");
                dt.Columns.Add("الحالة");
                dt.Columns.Add("المخرج");
                dt.Columns.Add("المحطة");

                dtMain.Columns.Add("dir");
                dtMain.Columns.Add("endTime");
                dtMain.Columns.Add("startTime");
                dtMain.Columns.Add("المخرج");
                dtMain.Columns.Add("المحطة");
                dtStations.Columns.Add("المدة");
                dtStations.Columns.Add("المخرج");
                dtStations.Columns.Add("المحطة");

                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork_Filter;
                worker.ProgressChanged += Worker_ProgressChanged_Filter;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted_Filter;
                worker.RunWorkerAsync();

            }

        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //stationsList.Clear();

            //GetStations();

            if (stationsList.Count > 0)
            {
                for (int i = 0; i < stationsList.Count; i++)
                {
                    dt.Clear();
                    oldState = "";
                    oldR = "";
                    oldS = "";
                    oldT = "";
                    dat1 = "";
                    dat2 = "";
                    totalHou = 0;
                    totalMin = 0;
                    totalSec = 0;
                    getExit(stationsList[i]);
                    if (ExitsList.Count > 0)
                    {
                        for (int j = 0; j < ExitsList.Count; j++)
                    {
                        refreshTable(ExitsList[j]);
            

                            }
                            dt.Clear();
                        oldState = "";
                        oldR = "";
                        oldS = "";
                        oldT = "";
                        dat1 = "";
                        dat2 = "";
                        totalHou = 0;
                        totalMin = 0;
                        totalSec = 0;
                    }
                
                }
                    ExitsList.Clear();

                
            }


            //if (totalSecTot >= 60)
            //{
            //    int extraMinutes = totalSec / 60;
            //    totalSec += extraMinutes;
            //    totalSec = totalSec % 60;
            //}
            //if (totalHouTot >= 60)
            //    {
            //        int extraMinutes = totalSec / 60;
            //        totalMin += extraMinutes;
            //        totalSec = totalSec % 60;
            //    }

            //    if (totalMinTot >= 60)
            //    {
            //        int extraHours = totalMin / 60;
            //        totalHou += extraHours;
            //        totalMin = totalMin % 60;
            //    }
             
            //    string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", totalHouTot, totalMinTot, totalSecTot);

            //    string resultTime = formattedTime;
            //this.Dispatcher.Invoke(() =>
            //{

            //    totalHtxt.Text = resultTime;
            //});

        }




        private void Worker_DoWork_Filter(object sender, DoWorkEventArgs e)
        {
            if (stationsList.Count > 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    getExit(FilterByCombo.SelectedItem.ToString());
                });

                for (int j = 0; j < ExitsList.Count; j++)
                    {
                        refreshTable(ExitsList[j]);
                        dt.Clear();
                        oldState = "";
                        oldR = "";
                        oldS = "";
                        oldT = "";
                        dat1 = "";
                        dat2 = "";
                    totalHou = 0;
                    totalMin= 0;
                    totalSec = 0;
                    }
                    ExitsList.Clear();

                
            }
//            if (dtStations.Rows.Count > 0)
//            {

//                if (totalSecTot >= 60)
//            {
//                int extraMinutes = totalSec / 60;
//                totalSecTot += extraMinutes;
//                    totalSecTot = totalSec % 60;
//            }
//            if (totalHouTot >= 60)
//            {
//                int extraMinutes = totalSec / 60;
//                    totalMinTot += extraMinutes;
//                    totalSecTot = totalSec % 60;
//            }

//            if (totalMinTot >= 60)
//            {
//                int extraHours = totalMin / 60;
//                    totalHouTot += extraHours;
//                    totalMinTot = totalMin % 60;
//            }

//            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", totalHou, totalMin, totalSec);

//            string resultTime = formattedTime;
//            this.Dispatcher.Invoke(() =>
//            {
//                totalHtxt.Text = resultTime;
//            });
//}
        }




        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        private void Worker_ProgressChanged_Filter(object sender, ProgressChangedEventArgs e)
        {
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            enterBtn.IsEnabled = true;

          
        }

        private void Worker_RunWorkerCompleted_Filter(object sender, RunWorkerCompletedEventArgs e)
        {
            enterBtn.IsEnabled = true;


        }
        private void ExportToExcel(DataGrid dataGrid)
        {
            //if (FilterByComboExit.SelectedItem != null && FilterByComboExit.Text != "" && FilterByCombo.Text != null && FilterByCombo.SelectedItem != null)
            //{
            if (FilterByCombo.Items.Count > 0)
            {
                var dataTable = new DataTable();

                foreach (var column in dataGrid.Columns)
                {
                    dataTable.Columns.Add(column.Header.ToString());
                }



                foreach (var item in dataGrid.Items)
                {
                    var row = dataTable.NewRow();
                    var index = 0;
                    foreach (var column in dataGrid.Columns)
                    {
                        var cell = column.GetCellContent(item);
                        if (cell != null)
                        {
                            var textBlock = cell as TextBlock;
                            if (textBlock != null)
                            {
                                row[index] = textBlock.Text;
                            }
                        }
                        index++;
                    }
                    dataTable.Rows.Add(row);

                }
                DataRow newRow = dataTable.NewRow();



                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add($"جدول مدة التشغيل للمحطات ");

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                }

                for (int i = 0; i < dtStations.Rows.Count; i++)
                {
                    var row = dtStations.Rows[i];
                    for (int j = 0; j < dtStations.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = row[j];
                    }
                }

                //worksheet.Cell(2, 5).Value = FilterByComboExit.SelectedItem;
                //worksheet.Cell(2, 6).Value = FilterByCombo.SelectedItem;

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    DefaultExt = ".xlsx",
                    FileName = FilterByCombo.SelectedItem.ToString()

                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                }

            }
            else { MessageBox.Show("إختر المحطة","خطأ",MessageBoxButton.OK,MessageBoxImage.Error); }
        }

            

            


        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel(dataGrid);

        }

        private void FilterByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (FilterByCombo.SelectedItem == "جميع المحطات")
            //{
            //    ///
            //    oldState = "";
            //    oldR = "";
            //    oldS = "";
            //    oldT = "";
            //    totalHtxt.Text = "";
            //    totalHou = 0;
            //    totalMin = 0;
            //    totalSec = 0;
            //    totalHouTot = 0;
            //    totalMinTot = 0;
            //    totalSecTot = 0;
            //    /// Clear Table
            //    dt.Columns.Clear();
            //    dtMain.Columns.Clear();
            //    dt.Clear();
            //    dtMain.Clear();
            //    dtStations.Columns.Clear();
            //    dtStations.Clear();
            //    /// Add coulmn
            //    /// Add coulmn

            //    dt.Columns.Add("DATETIME");
            //    dt.Columns.Add("T");
            //    dt.Columns.Add("S");
            //    dt.Columns.Add("R");
            //    dt.Columns.Add("AMPER");
            //    dt.Columns.Add("الحالة");
            //    dt.Columns.Add("المخرج");
            //    dt.Columns.Add("المحطة");

            //    dtMain.Columns.Add("dir");
            //    dtMain.Columns.Add("endTime");
            //    dtMain.Columns.Add("startTime");
            //    dtMain.Columns.Add("المخرج");
            //    dtMain.Columns.Add("المحطة");

            //    dtStations.Columns.Add("المدة");
            //    dtStations.Columns.Add("المخرج");
            //    dtStations.Columns.Add("المحطة");

            //    BackgroundWorker worker = new BackgroundWorker();
            //    worker.WorkerReportsProgress = true;
            //    worker.DoWork += Worker_DoWork;
            //    worker.ProgressChanged += Worker_ProgressChanged;
            //    worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            //    worker.RunWorkerAsync();
            //}
            //else 
            //{

                ///
                oldState = "";
                oldR = "";
                oldS = "";
                oldT = "";
                totalHou = 0;
                totalMin = 0;
                totalSec = 0;
                totalHouTot = 0;
                totalMinTot = 0;
                totalSecTot = 0;
                /// Clear Table
                dt.Columns.Clear();
                dtMain.Columns.Clear();
                dt.Clear();
                dtMain.Clear();
                dtStations.Columns.Clear();
                dtStations.Clear();
                /// Add coulmn

                dt.Columns.Add("DATETIME");
                dt.Columns.Add("T");
                dt.Columns.Add("S");
                dt.Columns.Add("R");
                dt.Columns.Add("AMPER");
                dt.Columns.Add("الحالة");
                dt.Columns.Add("المخرج");
                dt.Columns.Add("المحطة");

                dtMain.Columns.Add("dir");
                dtMain.Columns.Add("endTime");
                dtMain.Columns.Add("startTime");
                dtMain.Columns.Add("المخرج");
                dtMain.Columns.Add("المحطة");

                dtStations.Columns.Add("المدة");
                dtStations.Columns.Add("المخرج");
                dtStations.Columns.Add("المحطة");

                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork_Filter;
                worker.ProgressChanged += Worker_ProgressChanged_Filter;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted_Filter;
                worker.RunWorkerAsync();

            //}

        }
    }

}

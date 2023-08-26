using ClosedXML.Excel;
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
    /// Interaction logic for ExitsStateReport.xaml
    /// </summary>
    public partial class ExitsStateReport : Page
    {
        public ExitsStateReport()
        {
            InitializeComponent();
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
        String oldState;
        String oldR;
        String oldS;
        String oldT;
        String dat1 = "";
        String dat2 = "";
        public DataTable dt = new DataTable();
        public DataTable dtMain = new DataTable();
        private void refreshTable()
        {

            try
            {
                if (FilterByComboExit.SelectedItem != null && FilterByComboExit.Text != "")
                {

                    using (var connection = new OracleConnection(Connection.ConnectionString))
                    {
                        dt.Rows.Clear();
                        dt.Clear();
                        dt.Columns.Clear();
                        dtMain.Rows.Clear();
                        dtMain.Clear();
                        dtMain.Columns.Clear();
                        string sql = "SELECT  Amper,Status,R,S,T,dateTime FROM Details where dateTime BETWEEN '" + startDate.Text + "' and '" + endDate.Text + "' and Exit='" + FilterByComboExit.SelectedItem.ToString() + "' order by datetime asc  ";
                        connection.Open();
                        OracleCommand cmd = new OracleCommand(sql, connection);
                        OracleDataReader reader = cmd.ExecuteReader();
                        OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                        //adapter.Fill(dt);
                        dt.Columns.Add("DATETIME");
                        dt.Columns.Add("T");
                        dt.Columns.Add("S");
                        dt.Columns.Add("R");
                        dt.Columns.Add("AMPER");
                        dt.Columns.Add("الحالة");
                        dtMain.Columns.Add("dir");
                        dtMain.Columns.Add("endTime");
                        dtMain.Columns.Add("startTime");
                        String dat1 = "";
                        String dat2 = "";
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

                        GetTotalHour();
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                }
                else
                {

                    System.Windows.MessageBox.Show("يرجى إختيار المخرج", "تحذير", MessageBoxButton.OK, MessageBoxImage.Error);


                }
            }
            catch (OracleException ex) { DisplatMessage(); }

        }


        public void GetTotalHour()
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

                        newRow["startTime"] = dat1;


                        newRow["endTime"] = dat2;
                        DateTime sDate = DateTime.ParseExact(dat1, "MM/dd/yyyy HH:mm:ss", null); ;
                        DateTime eDate = DateTime.ParseExact(dat2, "MM/dd/yyyy HH:mm:ss", null); ;
                        TimeSpan timeElapsed = eDate - sDate;

                        double total = timeElapsed.TotalHours;
                        TimeSpan timeHour = TimeSpan.FromHours(total);

                        string resultTime = timeHour.ToString();
                        newRow["dir"] = resultTime;


                        dtMain.Rows.Add(newRow);

                    }
                    dat1 = "";
                    dat2 = "";
                    //}
                }
            }

        }
        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            oldState = "";
            oldR = "";
            oldS = "";
            oldT = "";
            //totalHtxt.Text = "";
            if (FilterByCombo.Text == null && FilterByComboExit.Text == null)
            {
                System.Windows.MessageBox.Show("يرجى إختيار المخرج", "تحذير", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                refreshTable();
                //if (dtMain.Rows.Count > 0)
                //{
                //    TimeSpan timeElapsed = TimeSpan.Zero;
                //    foreach (DataRow row in dtMain.Rows)
                //    {
                //        timeElapsed = timeElapsed + TimeSpan.Parse(row["dir"].ToString());
                //    }
                //    double totals = timeElapsed.TotalHours;
                //    TimeSpan timeHour = TimeSpan.FromHours(totals);
                //    string resultTime = timeHour.ToString();
                //    totalHtxt.Text = resultTime.ToString();

                //}


            }

        }
        private void ExportToExcel(DataGrid dataGrid)
        {
            if (FilterByComboExit.SelectedItem != null && FilterByComboExit.Text != "" && FilterByCombo.Text != null && FilterByCombo.SelectedItem != null)
            {

                var dataTable = new DataTable();

                foreach (var column in dataGrid.Columns)
                {
                    dataTable.Columns.Add(column.Header.ToString());
                }
                //dataTable.Columns.Add("مجموع ساعات العمل");
                dataTable.Columns.Add("اسم المخرج");
                dataTable.Columns.Add("اسم المحطة");

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


                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add($"جدول حالة مخرج " + FilterByComboExit.SelectedItem + "");

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var row = dt.Rows[i];
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = row[j];
                    }
                }
                //worksheet.Cell(2, 4).Value = totalHtxt.Text;

                worksheet.Cell(2, 5).Value = FilterByComboExit.SelectedItem;
                worksheet.Cell(2, 6).Value = FilterByCombo.SelectedItem;

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    DefaultExt = ".xlsx",
                    FileName = FilterByComboExit.SelectedItem.ToString()
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                }
            }

            else
            {
                System.Windows.MessageBox.Show("يرجى إختيار المخرج", "تحذير", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel(dataGrid);

        }
    }
}

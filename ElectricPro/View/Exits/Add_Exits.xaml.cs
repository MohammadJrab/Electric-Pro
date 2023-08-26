using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Text.RegularExpressions;

namespace ElectricPro.View.Exits
{
    /// <summary>
    /// Interaction logic for Add_Exits.xaml
    /// </summary>
    public partial class Add_Exits : Page
    {
        public Add_Exits()
        {
            InitializeComponent();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;

            worker.RunWorkerAsync();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {


            getStations();
            //getSn();
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
        //public void getSn()
        //{
        //    try
        //    {
        //        using (var connection = new OracleConnection(Connection.ConnectionString))
        //        {
        //            connection.Open();
        //            var command = new OracleCommand("select sn from DETAILS where exit IS NULL group by sn", connection);
        //            var reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                this.Dispatcher.Invoke(() =>
        //                {
        //                    SnCombo.Items.Add(reader.GetString(0));
        //                });
        //            }

        //        }
        //    }
        //    // "خطأ في الاتصال بقاعدة البيانات"
        //    catch (OracleException ex) { DisplatMessage(); }

        //}

        private void SnCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            inserExit();
        }
        private void inserExit()
        {
            try
            {
               

                    if (NewExitTxt.Text.Length > 0 && FilterByCombo.Items.Count != 0&&snTxt.Text.Length>0)
                    {

                        string sql = $"INSERT INTO Exits (Station,Exit,SN) VALUES ('"+ FilterByCombo .SelectedItem.ToString()+ "','" + NewExitTxt.Text + "','"+snTxt.Text+"') ";

                        using (var connection = new OracleConnection(Connection.ConnectionString))
                        {
                            using (OracleCommand command = new OracleCommand(sql, connection))
                            {


                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                connection.Close();
                                //LinkSN();
                            if (rowsAffected == 1)
                            {
                                setStatus();

                                MessageTextState.Content = "تم إضافة المخرج ";
                                SnackbarState.IsActive = true;
                                NewExitTxt.Text = "";
                                snTxt.Text = "";


                            }
                            else
                            {

                                MessageTextState.Content = "فشل إضافة المخرج " + FilterByCombo.SelectedItem.ToString() + "";
                                SnackbarState.IsActive = true;
                                NewExitTxt.Text = "";
                                snTxt.Text = "";
                            }
                        }
                        }
                    }
                    else
                    {
                        
                            MessageTextState.Content = "يرجى إدخال اسم المخرج";
                            SnackbarState.IsActive = true;
                    }

               
        }
            catch (OracleException ex)
            {
                if (ex.ErrorCode.ToString() == "-2147467259")
                {
                    MessageTextState.Content = "المخرج او السيريال موجود مسبقاً";
                    SnackbarState.IsActive = true;
                }
                else
                {
                    MessageTextState.Content = "فشل إضافة المخرج " + NewExitTxt.Text + "";
                    SnackbarState.IsActive = true;
                }
            }

        }
        public void setStatus()
        {



                if (NewExitTxt.Text.Length > 0 && FilterByCombo.Items.Count != 0 && snTxt.Text.Length > 0)
                {

                    string sql = $"INSERT INTO Ex_status (station,exit,state,sn) VALUES ('" + FilterByCombo.SelectedItem.ToString() + "','" + NewExitTxt.Text + "',2,'"+ snTxt.Text+ "') ";

                    using (var connection = new OracleConnection(Connection.ConnectionString))
                    {
                        using (OracleCommand command = new OracleCommand(sql, connection))
                        {


                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            connection.Close();
                    
                        }
                    }
                }
                else
                {
               
                        MessageTextState.Content = "يرجى إدخال اسم المخرج و السيريال";
                        SnackbarState.IsActive = true;
                }




        }
        //public void LinkSN() 
        //{
        //    //try
        //    //{
        //        this.Dispatcher.Invoke(() =>
        //        {

        //            if (NewExitTxt.Text.Length > 0 && FilterByCombo.Items.Count != 0 && SnCombo.Items.Count != 0)
        //            {

        //                string sql = $"UPDATE details SET exit = '" + NewExitTxt.Text + "' where sn = '" + SnCombo.SelectedItem.ToString() + "'";

        //                using (var connection = new OracleConnection(Connection.ConnectionString))
        //                {
        //                    using (OracleCommand command = new OracleCommand(sql, connection))
        //                    {


        //                        connection.Open();
        //                        int rowsAffected = command.ExecuteNonQuery();
        //                        connection.Close();

        //                        if (rowsAffected == 1)
        //                        {


        //                            MessageTextState.Content = "تم إضافة المخرج ";
        //                            SnackbarState.IsActive = true;
        //                            NewExitTxt.Text = "";


        //                        }
        //                        else
        //                        {

        //                            MessageTextState.Content = "فشل إضافة مخرج " + FilterByCombo.SelectedItem.ToString() + "";
        //                            SnackbarState.IsActive = true;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                this.Dispatcher.Invoke(() =>
        //                {
        //                    MessageTextState.Content = "يرجى إدخال اسم المخرج";
        //                    SnackbarState.IsActive = true;
        //                });
        //            }

        //        });
        //    //}
        //    //catch (OracleException ex)
        //    //{
        //    //    this.Dispatcher.Invoke(() =>
        //    //    {
        //    //        MessageTextState.Content = "فشل إضافة مخرج " + NewExitTxt.Text + "";
        //    //        SnackbarState.IsActive = true;
        //    //    });
        //    //}


        //}
  

        private void MessageTextState_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarState.IsActive = false;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

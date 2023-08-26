using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Page = System.Windows.Controls.Page;

namespace ElectricPro.View.Stations
{
    /// <summary>
    /// Interaction logic for Edit_Stations.xaml
    /// </summary>
    public partial class Edit_Stations : Page
    {
        public Edit_Stations()
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


        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker2 = new BackgroundWorker();
            worker2.WorkerReportsProgress = true;
            worker2.DoWork += Worker_DoWork2;
            worker2.RunWorkerAsync();
        }
        private void Worker_DoWork2(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if (newStationName.Text.Length > 0&&FilterByCombo.Items.Count!=0)
                    {

                        string sql = $"UPDATE stations SET station = '" + newStationName.Text + "' where station = '" + FilterByCombo.SelectedItem.ToString() + "'";

                        using (var connection = new OracleConnection(Connection.ConnectionString))
                        {
                            using (OracleCommand command = new OracleCommand(sql, connection))
                            {


                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                connection.Close();

                                if (rowsAffected == 1)
                                {
                                    edit_Ex_State();
                                    edit_Exit();
                                    editDetails();

                                    MessageTextState.Content = "تم تعديل المحطة ";
                                    SnackbarState.IsActive = true;
                                    newStationName.Text = "";
                                    FilterByCombo.Items.Clear();
                                    BackgroundWorker worker = new BackgroundWorker();
                                    worker.WorkerReportsProgress = true;
                                    worker.DoWork += Worker_DoWork;

                                    worker.RunWorkerAsync();
                                    FilterByCombo.SelectedIndex = 0;

                                    //MessageBox.Show("تم تعديل الحالة بنجاح", "الحالة", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                                }
                                else
                                {

                                    MessageTextState.Content = "فشل تعديل محطة " + FilterByCombo.SelectedItem.ToString() + "";
                                    SnackbarState.IsActive = true;
                                    //MessageBox.Show("! قشل تعديل الحالة", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            MessageTextState.Content = "يرجى إدخال اسم المحطة";
                            SnackbarState.IsActive = true;
                        });
                    }

                });
            }
            catch (OracleException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextState.Content = "فشل إضافة محطة " + FilterByCombo.SelectedItem.ToString() + "";
                    SnackbarState.IsActive = true;
                });
            }

        }
        public void editDetails()
        {


            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if (newStationName.Text.Length > 0 && FilterByCombo.Items.Count != 0)
                    {

                        string sql = $"UPDATE Details SET Station = '" + newStationName.Text + "' where Station = '" + FilterByCombo.SelectedItem.ToString() + "'";

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
                    
                

                });
            }
            catch (OracleException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextState.Content = "فشل تعديل محطة " + FilterByCombo.SelectedItem.ToString() + "";
                    SnackbarState.IsActive = true;
                });
            }


        }



        public void edit_Ex_State()
        {


            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if (newStationName.Text.Length > 0 && FilterByCombo.Items.Count != 0)
                    {

                        string sql = $"UPDATE Ex_Status SET Station = '" + newStationName.Text + "' where Station = '" + FilterByCombo.SelectedItem.ToString() + "'";

                        using (var connection = new OracleConnection(Connection.ConnectionString))
                        {
                            using (OracleCommand command = new OracleCommand(sql, connection))
                            {


                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                connection.Close();
                                //if (rowsAffected > 0)
                                //{

                                //    editُ_Exit();

                                //}
                                //else
                                //{



                                //}
                            }
                        }
                    }

                });
            }
            catch (OracleException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextState.Content = "فشل تعديل محطة " + FilterByCombo.SelectedItem.ToString() + "";
                    SnackbarState.IsActive = true;
                });
            }


        }

        public void edit_Exit()
        {


            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if (newStationName.Text.Length > 0 && FilterByCombo.Items.Count != 0)
                    {

                        string sql = $"UPDATE Exits SET Station = '" + newStationName.Text + "' where Station = '" + FilterByCombo.SelectedItem.ToString() + "'";

                        using (var connection = new OracleConnection(Connection.ConnectionString))
                        {
                            using (OracleCommand command = new OracleCommand(sql, connection))
                            {


                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                connection.Close();
                                //if (rowsAffected > 0)
                                //{


                                //}
                                //else
                                //{



                                //}
                            }
                        }
                    }

                });
            }
            catch (OracleException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextState.Content = "فشل تعديل محطة " + FilterByCombo.SelectedItem.ToString() + "";
                    SnackbarState.IsActive = true;
                });
            }


        }

        private void MessageTextState_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarState.IsActive = false;
        }
    }
}

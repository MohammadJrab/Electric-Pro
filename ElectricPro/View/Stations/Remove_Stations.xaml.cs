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

namespace ElectricPro.View.Stations
{
    /// <summary>
    /// Interaction logic for Remove_Stations.xaml
    /// </summary>
    public partial class Remove_Stations : Page
    {
        public Remove_Stations()
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


    
        private void Worker_DoWork2(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if ( FilterByCombo.Items.Count != 0)
                    {

                        string sql = $"delete from stations where station = '" + FilterByCombo.SelectedItem.ToString() + "'";

                        using (var connection = new OracleConnection(Connection.ConnectionString))
                        {
                            using (OracleCommand command = new OracleCommand(sql, connection))
                            {


                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                connection.Close();

                                if (rowsAffected == 1)
                                {
                                    delete_Ex_State();
                                    MessageTextState.Content = "تم حذف المحطة ";
                                    SnackbarState.IsActive = true;
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

                                    MessageTextState.Content = "فشل حذف محطة " + FilterByCombo.SelectedItem.ToString() + "";
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
                            MessageTextState.Content = "يرجى إختيار اسم المحطة";
                            SnackbarState.IsActive = true;
                        });
                    }

                });
            }
            catch (OracleException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextState.Content = "فشل حذف محطة " + FilterByCombo.SelectedItem.ToString() + "";
                    SnackbarState.IsActive = true;
                });
            }

        }
        public void deleteDetails()
        {


            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if (FilterByCombo.Items.Count != 0)
                    {

                        string sql = $"delete from Details  where Station = '" + FilterByCombo.SelectedItem.ToString() + "'";

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
                    MessageTextState.Content = "فشل حذف محطة " + FilterByCombo.SelectedItem.ToString() + "";
                    SnackbarState.IsActive = true;
                });
            }


        }



        public void delete_Ex_State()
        {


            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if (FilterByCombo.Items.Count != 0)
                    {

                        string sql = $"delete from Ex_Status where Station = '" + FilterByCombo.SelectedItem.ToString() + "'";

                        using (var connection = new OracleConnection(Connection.ConnectionString))
                        {
                            using (OracleCommand command = new OracleCommand(sql, connection))
                            {


                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                connection.Close();
                                if (rowsAffected == 1)
                                {

                                    delete_Exit();

                                }
                                else
                                {



                                }
                            }
                        }
                    }

                });
            }
            catch (OracleException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextState.Content = "فشل حذف محطة " + FilterByCombo.SelectedItem.ToString() + "";
                    SnackbarState.IsActive = true;
                });
            }


        }

        public void delete_Exit()
        {


            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if (FilterByCombo.Items.Count != 0)
                    {

                        string sql = $"delete from Exists  where Station = '" + FilterByCombo.SelectedItem.ToString() + "'";

                        using (var connection = new OracleConnection(Connection.ConnectionString))
                        {
                            using (OracleCommand command = new OracleCommand(sql, connection))
                            {


                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                connection.Close();
                                if (rowsAffected == 1)
                                {


                                    deleteDetails();
                                }
                                else
                                {



                                }
                            }
                        }
                    }

                });
            }
            catch (OracleException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextState.Content = "فشل حذف محطة " + FilterByCombo.SelectedItem.ToString() + "";
                    SnackbarState.IsActive = true;
                });
            }


        }
        private void MessageTextState_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarState.IsActive = false;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker2 = new BackgroundWorker();
            worker2.WorkerReportsProgress = true;
            worker2.DoWork += Worker_DoWork2;
            worker2.RunWorkerAsync();
        }
    }
}

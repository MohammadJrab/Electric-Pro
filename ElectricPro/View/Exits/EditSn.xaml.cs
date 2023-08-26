﻿using Oracle.ManagedDataAccess.Client;
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

namespace ElectricPro.View.Exits
{
    /// <summary>
    /// Interaction logic for EditSn.xaml
    /// </summary>
    public partial class EditSn : Page
    {
        public EditSn()
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

        private void FilterByCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ExitByCombo.Items.Clear();
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    var command = new OracleCommand("SELECT exit FROM Exits where station='" + FilterByCombo.SelectedItem + "' ", connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ExitByCombo.Items.Add(reader.GetString(0));

                    }

                }
                ExitByCombo.SelectedIndex = 0;
            }
            // "خطأ في الاتصال بقاعدة البيانات"
            catch (OracleException ex) { DisplatMessage(); }


        }




        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {


                if (NewSnTxt.Text.Length > 0 && ExitByCombo.Items.Count != 0)
                {

                    string sql = $"UPDATE Exits SET sn = '" + NewSnTxt.Text + "' where exit = '" + ExitByCombo.SelectedItem.ToString() + "'";

                    using (var connection = new OracleConnection(Connection.ConnectionString))
                    {
                        using (OracleCommand command = new OracleCommand(sql, connection))
                        {


                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            connection.Close();

                            if (rowsAffected == 1)
                            {
                                editDetails();
                                MessageTextState.Content = "تم تعديل السيريال ";
                                SnackbarState.IsActive = true;
                                NewSnTxt.Text = "";
                                FilterByCombo.Items.Clear();
                                ExitByCombo.Items.Clear();
                                BackgroundWorker worker = new BackgroundWorker();
                                worker.WorkerReportsProgress = true;
                                worker.DoWork += Worker_DoWork;
                                worker.RunWorkerAsync();
                                FilterByCombo.SelectedIndex = 0;

                            }
                            else
                            {

                                MessageTextState.Content = "يرجى إدخال السيريال";
                                SnackbarState.IsActive = true;
                            }
                        }
                    }
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        MessageTextState.Content = "يرجى إدخال السيريال او اختيار المخرج";
                        SnackbarState.IsActive = true;
                    });
                }

            }
            catch (OracleException ex)
            {
                if (ex.ErrorCode.ToString() == "-2147467259")
                {
                    MessageTextState.Content = "السيريال موجود مسبقاً";
                    SnackbarState.IsActive = true;
                }
                else
                {
                    MessageTextState.Content = "فشل تعديل السيريال " + NewSnTxt.Text + "";
                    SnackbarState.IsActive = true;
                }
            }

        }

        public void editDetails()
        {


            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if (NewSnTxt.Text.Length > 0 && ExitByCombo.Items.Count != 0)
                    {

                        string sql = $"UPDATE Details SET sn = '" + NewSnTxt.Text + "' where exit = '" + ExitByCombo.SelectedItem.ToString() + "'";

                        using (var connection = new OracleConnection(Connection.ConnectionString))
                        {
                            using (OracleCommand command = new OracleCommand(sql, connection))
                            {


                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                connection.Close();

                                if (rowsAffected == 1)
                                {
                                    editState();



                                }
                                else
                                {

                                    MessageTextState.Content = "فشل تعديل السيريال " + NewSnTxt.Text + "";
                                    SnackbarState.IsActive = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            MessageTextState.Content = "يرجى إدخال السيريال";
                            SnackbarState.IsActive = true;
                        });
                    }

                });
            }
            catch (OracleException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextState.Content = "فشل تعديل السيريال " + FilterByCombo.SelectedItem.ToString() + "";
                    SnackbarState.IsActive = true;
                });
            }


        }


        public void editState()
        {


            try
            {
                this.Dispatcher.Invoke(() =>
                {

                    if (NewSnTxt.Text.Length > 0 && ExitByCombo.Items.Count != 0)
                    {

                        string sql = $"UPDATE EX_STATUS SET sn = '" + NewSnTxt.Text + "' where exit = '" + ExitByCombo.SelectedItem.ToString() + "'";

                        using (var connection = new OracleConnection(Connection.ConnectionString))
                        {
                            using (OracleCommand command = new OracleCommand(sql, connection))
                            {


                                connection.Open();
                                int rowsAffected = command.ExecuteNonQuery();
                                connection.Close();

                                //if (rowsAffected == 1)
                                //{



                                //}
                                //else
                                //{

                                //    MessageTextState.Content = "فشل تعديل مخرج " + ExitByCombo.SelectedItem.ToString() + "";
                                //    SnackbarState.IsActive = true;
                                //}
                            }
                        }
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            MessageTextState.Content = "يرجى إدخال السيريال";
                            SnackbarState.IsActive = true;
                        });
                    }

                });
            }
            catch (OracleException ex)
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageTextState.Content = "فشل تعديل السيريال";
                    SnackbarState.IsActive = true;
                });
            }


        }
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
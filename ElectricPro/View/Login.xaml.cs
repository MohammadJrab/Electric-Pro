using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using ElectricPro.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElectricPro.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
     
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        public Login()
        {
            InitializeComponent();
            txtUsername.Focus();

        }
        private String username, password, sql, sql2;
        private connectionState conn = new connectionState();
        private OracleCommand command;

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            username = txtUsername.Text;
            password = txtPassword.Password;
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {

                MessageBox.Show("اسم المستخدم او كلمة المرور فارغة");
            }
            else
            {

                sql = "Select username,password from users where username='" + username + "'and password='" + password + "'";
                sql2 = "Select permission,TurnMenu,ExitsMenu,StationMenu,StationDurM,ExitDurM,ExitDetailsM,AmperChartM from users where username='" + username + "'";

                if (conn.OpenConnection() == true)
                {
                    try
                    {

                        command = new OracleCommand(sql, conn.get_Connection());
                        object a = command.ExecuteScalar();
                        if (a == null)
                        {
                            MessageTextturnAlarm.Content ="خطأ في اسم المستخدم او كلمة المرور";
                            turnAlarm.IsActive = true;
                            txtUsername.Text = "";
                            txtPassword.Password = "";
                            txtUsername.Focus();

                        }
                        else
                        {

                            command = new OracleCommand(sql2, conn.get_Connection());
                            var reader = command.ExecuteReader();
                            //object b = command.ExecuteScalar();
                            while (reader.Read())
                            {
                                users.permission = reader.GetString(0);
                                users.TurnMenu = reader.GetString(1);
                                users.ExitsMenu = reader.GetString(2);
                                users.StationMenu = reader.GetString(3);
                                users.StationDurM = reader.GetString(4);
                                users.ExitDurM = reader.GetString(5);
                                users.ExitDetailsM = reader.GetString(6);
                                users.AmperChartM = reader.GetString(7);
                            }
                            MainWindow mn = new MainWindow();
                            this.Close();
                            mn.Show();



                        }

                    }
                    catch (OracleException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
                else if (conn.OpenConnection() == false)
                {
                    MessageBox.Show("خطأ في الاتصال بالسيرفر", "تحذير", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtUsername.Focus();

                }
            }
            txtUsername.Text = "";
            txtPassword.Password = "";
            conn.close_conn();
        }

        private void MessageTextturnAlarm_ActionClick(object sender, RoutedEventArgs e)
        {
            turnAlarm.IsActive = false;

        }

        private bool VerifyUser(string username, string password)
        {
            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {

                    string sql = "select Status from Users where username = '" + username + "' and password = '" + password + "' ";
                    connection.Open();
                    OracleCommand cmd = new OracleCommand(sql, connection);
                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        if (Convert.ToInt32(reader["Status"]) == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex) { return false; }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception ex) { }
        }


        private void Close_App_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

    }
}
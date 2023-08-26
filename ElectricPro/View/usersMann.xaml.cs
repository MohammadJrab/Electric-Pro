using DocumentFormat.OpenXml.Bibliography;
using ElectricPro.Models;
using ElectricPro.View.Reports;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
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

namespace ElectricPro.View
{
    /// <summary>
    /// Interaction logic for usersMann.xaml
    /// </summary>
    public partial class usersMann : Page
    {
        public DataTable UsersDataTable = new DataTable();

        public usersMann()
        {
            InitializeComponent();
            UsersDataTable.Columns.Add("username");
            UsersDataTable.Columns.Add("password");
            UsersDataTable.Columns.Add("TurnMenu");
            UsersDataTable.Columns.Add("ExitsMenu");
            UsersDataTable.Columns.Add("StationMenu");
            UsersDataTable.Columns.Add("StationDurm");
            UsersDataTable.Columns.Add("ExitDurM");
            UsersDataTable.Columns.Add("ExitDetailsM");
            UsersDataTable.Columns.Add("AmperChartM");

            UsersListView.ItemsSource = UsersDataTable.DefaultView;


            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {


            getUsers();

        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


        }


        public void getUsers()
        {
            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    var command = new OracleCommand("SELECT USERNAME,Password,TURNMENU,ExitsMenu,StationMenu,StationDurm,ExitDurM,ExitDetailsM,AmperChartM FROM users where not PERMISSION = 'مدير' ", connection);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string TurnMenu, ExitsMenu, StationMenu, StationDurm, ExitDurM, ExitDetailsM, AmperChartM;

                        if (reader.GetString(2) == "0")
                        {
                            TurnMenu = "لا";
                        }
                        else
                        {
                            TurnMenu = "نعم";
                        }


                        if (reader.GetString(3) == "0")
                        {
                            ExitsMenu = "لا";
                        }
                        else
                        {
                            ExitsMenu = "نعم";
                        }


                        if (reader.GetString(4) == "0")
                        {
                            StationMenu = "لا";
                        }
                        else
                        {
                            StationMenu = "نعم";
                        }

                        if (reader.GetString(5) == "0")
                        {
                            StationDurm = "لا";
                        }
                        else
                        {
                            StationDurm = "نعم";
                        }

                        if (reader.GetString(6) == "0")
                        {
                            ExitDurM = "لا";
                        }
                        else
                        {
                            ExitDurM = "نعم";
                        }

                        if (reader.GetString(7) == "0")
                        {
                            ExitDetailsM = "لا";
                        }
                        else
                        {
                            ExitDetailsM = "نعم";
                        }

                        if (reader.GetString(8) == "0")
                        {
                            AmperChartM = "لا";
                        }
                        else
                        {
                            AmperChartM = "نعم";
                        }

                        UsersDataTable.Rows.Add(reader.GetString(0), reader.GetString(1), TurnMenu, ExitsMenu, StationMenu, StationDurm, ExitDurM, ExitDetailsM, AmperChartM);
                    }

                }
            }
            // "خطأ في الاتصال بقاعدة البيانات"
            catch (OracleException ex) { DisplatMessage(); }

        }

        public void DisplatMessage()
        {
            int milliseconds = 8000;
            Dispatcher.Invoke(() => SnackbarFive.IsActive = true);

            Thread.Sleep(milliseconds);
            Dispatcher.Invoke(() => SnackbarFive.IsActive = false);



        }



        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskPopup.IsOpen = true;
        }

        private void CancelTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTaskPopup.IsOpen = false;
            txtUsername.Text = "";
            txtPassword.Password = "";

        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            deleteUser(dataRowView.Row[0].ToString());
            UsersDataTable.Rows.Clear();
            getUsers();


        }



        private void enterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Password != "")
            {
                if (txtUsername.Text.Length >= 1 && txtPassword.Password.Length >= 2)
                {
                    string username, password, TurnMenu, ExitsMenu, StationMenu, StationDurm, ExitDurM, ExitDetailsM, AmperChartM;

                    username = txtUsername.Text;
                    password = txtPassword.Password;
                    if (TurnMenuChk.IsChecked == true)
                    {
                        TurnMenu = "1";
                    }
                    else
                    {
                        TurnMenu = "0";

                    }
                    if (ExitsMenuChk.IsChecked == true)
                    {
                        ExitsMenu = "1";
                    }
                    else
                    {
                        ExitsMenu = "0";

                    }
                    if (StationChk.IsChecked == true)
                    {
                        StationMenu = "1";
                    }
                    else
                    {
                        StationMenu = "0";

                    }
                    if (StationDurmChk.IsChecked == true)
                    {
                        StationDurm = "1";
                    }
                    else
                    {
                        StationDurm = "0";

                    }
                    if (ExitDurMChk.IsChecked == true)
                    {
                        ExitDurM = "1";
                    }
                    else
                    {
                        ExitDurM = "0";

                    }
                    if (ExitDetailsMChk.IsChecked == true)
                    {
                        ExitDetailsM = "1";
                    }
                    else
                    {
                        ExitDetailsM = "0";

                    }
                    if (AmperChartMChk.IsChecked == true)
                    {
                        AmperChartM = "1";
                    }
                    else
                    {
                        AmperChartM = "0";

                    }

                    insertUser(username, password, TurnMenu, ExitsMenu, StationMenu, StationDurm, ExitDurM, ExitDetailsM, AmperChartM);
                    UsersDataTable.Rows.Clear();
                    getUsers();
                    txtUsername.Text = "";
                    txtPassword.Password = "";
                    AddTaskPopup.IsOpen = false;

                }
                else { MessageBox.Show("يجب ان يكون عدد أحرف اسم المستخدم أكبر من حرف ", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
            else { MessageBox.Show("قم بإدخال اسم المستخدم وكلمة المرور", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error); }


        }
        string username;
        private void updateUser_Click(object sender, RoutedEventArgs e)
        {
            username = "";
            changePass.IsChecked = false;
            NewtxtPasswordUser.IsEnabled = false;
            NewtxtPasswordUser.Password = "";
            EditPopup.IsOpen = true;
            DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;
            username = dataRowView.Row[0].ToString();
            getPer(dataRowView.Row[0].ToString());

        }

        public void getPer(string username)
        {

            try
            {

                string sql = $"select TurnMenu, ExitsMenu, StationMenu, StationDurm,  ExitDurM, ExitDetailsM, AmperChartM from users  where username = '" + username + "' ";

                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {
                        connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader.GetString(0) == "1")
                            { PerTurnMenuChk.IsChecked = true; }
                            else { PerTurnMenuChk.IsChecked = false; }

                            if (reader.GetString(1) == "1")
                            { PerExitsMenuChk.IsChecked = true; }
                            else { PerExitsMenuChk.IsChecked = false; }

                            if (reader.GetString(2) == "1")
                            { PerStationChk.IsChecked = true; }
                            else { PerStationChk.IsChecked = false; }


                            if (reader.GetString(3) == "1")
                            { PerStationDurmChk.IsChecked = true; }
                            else { PerStationDurmChk.IsChecked = false; }

                            if (reader.GetString(4) == "1")
                            { PerExitDurMChk.IsChecked = true; }
                            else { PerExitDurMChk.IsChecked = false; }

                            if (reader.GetString(5) == "1")
                            { PerExitDetailsMChk.IsChecked = true; }
                            else { PerExitDetailsMChk.IsChecked = false; }

                            if (reader.GetString(6) == "1")
                            { PerAmperChartMChk.IsChecked = true; }
                            else { PerAmperChartMChk.IsChecked = false; }



                        }

                    }
                }



            }
            catch (OracleException ex) { }









        }

        public void insertUser(string username, string password, string TurnMenu, string ExitsMenu, string StationMenu, string StationDurm, string ExitDurM, string ExitDetailsM, string AmperChartM)
        {

            try
            {
                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    var command = new OracleCommand("insert into users(username,password,PERMISSION,TURNMENU,ExitsMenu,StationMenu,StationDurm,ExitDurM,ExitDetailsM,AmperChartM) values('" + username + "','" + password + "','مستخدم','" + TurnMenu + "','" + ExitsMenu + "','" + StationMenu + "','" + StationDurm + "','" + ExitDurM + "','" + ExitDetailsM + "','" + AmperChartM + "') ", connection);
                    var reader = command.ExecuteNonQuery();

                }
            }
            // "خطأ في الاتصال بقاعدة البيانات"
            catch (OracleException ex)
            {
                if (ex.ErrorCode.ToString() == "-2147467259")
                {
                    MessageTextState.Content = "اسم المستخدم موجود مسبقاً";
                    SnackbarState.IsActive = true;
                }
                else
                {
                    MessageTextState.Content = "فشل إضافة المستخدم";
                    SnackbarState.IsActive = true;
                }
            }
        }


        public void deleteUser(string username)
        {

            try
            {

                string sql = $"delete from users where username = '" + username + "' ";

                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {


                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();
                        if (rowsAffected == 1)
                        {
                            MessageTextState.Content = "تم حذف المستخدم";
                            SnackbarState.IsActive = true;

                        }
                        else
                        {
                            MessageTextState.Content = "فشل حذف المستخدم";
                            SnackbarState.IsActive = true;


                        }

                    }
                }



            }
            catch (OracleException ex) { }






        }

        public void editUser(string username, string TurnMenu, string ExitsMenu, string StationMenu, string StationDurm, string ExitDurM, string ExitDetailsM, string AmperChartM)
        {

            try
            {

                string sql = $"update users set TurnMenu='" + TurnMenu + "' , ExitsMenu='" + ExitsMenu + "' , StationMenu='" + StationMenu + "'  , STATIONDURM='" + StationDurm + "' ,  EXITDURM='" + ExitDurM + "' , EXITDETAILSM='" + ExitDetailsM + "' , AMPERCHARTM='" + AmperChartM + "' where username = '" + username + "' ";

                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {


                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 1)
                        {
                            MessageTextState.Content = "تم تعديل المستخدم";
                            SnackbarState.IsActive = true;
                            NewtxtPasswordUser.Password = "";
                            changePass.IsChecked = false;
                            PerStationChk.IsChecked = false;
                            PerAmperChartMChk.IsChecked = false;
                            PerExitDetailsMChk.IsChecked = false;
                            PerExitDurMChk.IsChecked = false;
                            PerExitsMenuChk.IsChecked = false;
                            PerStationChk.IsChecked = false;
                            PerStationDurmChk.IsChecked = false;
                            PerTurnMenuChk.IsChecked = false;
                            UsersDataTable.Rows.Clear();
                            getUsers();
                            EditPopup.IsOpen = false;

                        }
                        else
                        {

                            MessageTextState.Content = "فشل تعديل المستخدم";
                            SnackbarState.IsActive = true;
                            NewtxtPasswordUser.Password = "";
                            changePass.IsChecked = false;
                            PerStationChk.IsChecked = false;
                            PerAmperChartMChk.IsChecked = false;
                            PerExitDetailsMChk.IsChecked = false;
                            PerExitDurMChk.IsChecked = false;
                            PerExitsMenuChk.IsChecked = false;
                            PerStationChk.IsChecked = false;
                            PerStationDurmChk.IsChecked = false;
                            PerTurnMenuChk.IsChecked = false;
                            UsersDataTable.Rows.Clear();
                            getUsers();
                            EditPopup.IsOpen = false;


                        }
                        connection.Close();

                    }
                }



            }
            catch (OracleException ex)
            {

                MessageTextState.Content = "فشل تعديل المستخدم";
                SnackbarState.IsActive = true;
                NewtxtPasswordUser.Password = "";
                changePass.IsChecked = false;
                PerStationChk.IsChecked = false;
                PerAmperChartMChk.IsChecked = false;
                PerExitDetailsMChk.IsChecked = false;
                PerExitDurMChk.IsChecked = false;
                PerExitsMenuChk.IsChecked = false;
                PerStationChk.IsChecked = false;
                PerStationDurmChk.IsChecked = false;
                PerTurnMenuChk.IsChecked = false;
                UsersDataTable.Rows.Clear();
                getUsers();
                EditPopup.IsOpen = false;

            }
        }
        public void editPassUser(string username, string passwordNew)
        {

            try
            {

                string sql = $"update users set password='" + passwordNew + "' where username = '" + username + "' ";

                using (var connection = new OracleConnection(Connection.ConnectionString))
                {
                    using (OracleCommand command = new OracleCommand(sql, connection))
                    {


                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        connection.Close();
                        if (rowsAffected == 1)
                        {
                            MessageTextState.Content = "تم تعديل المستخدم";
                            SnackbarState.IsActive = true;
                            NewtxtPasswordUser.Password = "";
                            changePass.IsChecked = false;
                            PerStationChk.IsChecked = false;
                            PerAmperChartMChk.IsChecked = false;
                            PerExitDetailsMChk.IsChecked = false;
                            PerExitDurMChk.IsChecked = false;
                            PerExitsMenuChk.IsChecked = false;
                            PerStationChk.IsChecked = false;
                            PerStationDurmChk.IsChecked = false;
                            PerTurnMenuChk.IsChecked = false;
                            UsersDataTable.Rows.Clear();
                            getUsers();
                            EditPopup.IsOpen = false;
                        }
                        else
                        {
                            MessageTextState.Content = "فشل تعديل المستخدم";
                            SnackbarState.IsActive = true;
                            NewtxtPasswordUser.Password = "";
                            changePass.IsChecked = false;
                            PerStationChk.IsChecked = false;
                            PerAmperChartMChk.IsChecked = false;
                            PerExitDetailsMChk.IsChecked = false;
                            PerExitDurMChk.IsChecked = false;
                            PerExitsMenuChk.IsChecked = false;
                            PerStationChk.IsChecked = false;
                            PerStationDurmChk.IsChecked = false;
                            PerTurnMenuChk.IsChecked = false;
                            UsersDataTable.Rows.Clear();
                            getUsers();
                            EditPopup.IsOpen = false;

                        }

                    }
                }



            }
            catch (OracleException ex)
            {
                MessageTextState.Content = "فشل تعديل المستخدم";
                SnackbarState.IsActive = true;
                NewtxtPasswordUser.Password = "";
                changePass.IsChecked = false;
                PerStationChk.IsChecked = false;
                PerAmperChartMChk.IsChecked = false;
                PerExitDetailsMChk.IsChecked = false;
                PerExitDurMChk.IsChecked = false;
                PerExitsMenuChk.IsChecked = false;
                PerStationChk.IsChecked = false;
                PerStationDurmChk.IsChecked = false;
                PerTurnMenuChk.IsChecked = false;
                UsersDataTable.Rows.Clear();
                getUsers();
                EditPopup.IsOpen = false;

            }




        }

        private void MessageTextState_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarState.IsActive = false;

        }

        private void pop2closeBtn_Click(object sender, RoutedEventArgs e)
        {
            username = "";
            EditPopup.IsOpen = false;
            NewtxtPasswordUser.Password = "";
            changePass.IsChecked = false;
            PerStationChk.IsChecked = false;
            PerAmperChartMChk.IsChecked = false;
            PerExitDetailsMChk.IsChecked = false;
            PerExitDurMChk.IsChecked = false;
            PerExitsMenuChk.IsChecked = false;
            PerStationChk.IsChecked = false;
            PerStationDurmChk.IsChecked = false;
            PerTurnMenuChk.IsChecked = false;
            UsersDataTable.Rows.Clear();
            getUsers();

        }

        private void EditEnterBtn_Click(object sender, RoutedEventArgs e)
        {
            string TurnMenu = "0";
            string ExitsMenu = "0";
            string StationMenu = "0";
            string StationDurm = "0";
            string ExitDurM = "0";
            string ExitDetailsM = "0";
            string AmperChartM = "0";
            string Newpassword = NewtxtPasswordUser.Password;
            if (PerTurnMenuChk.IsChecked == true)
            {
                TurnMenu = "1";
            }
            else if (PerTurnMenuChk.IsChecked == false)
            {
                TurnMenu = "0";

            }
            if (PerExitsMenuChk.IsChecked == true)
            {
                ExitsMenu = "1";
            }
            else if (PerExitsMenuChk.IsChecked == false)
            {
                ExitsMenu = "0";

            }
            if (PerStationChk.IsChecked == true)
            {
                StationMenu = "1";
            }
            else if (PerStationChk.IsChecked == false)

            {
                StationMenu = "0";

            }
            if (PerStationDurmChk.IsChecked == true)
            {
                StationDurm = "1";
            }
            else if (PerStationDurmChk.IsChecked == false)
            {
                StationDurm = "0";

            }
            if (PerExitDurMChk.IsChecked == true)
            {
                ExitDurM = "1";
            }
            else if (PerExitDurMChk.IsChecked == false)
            {
                ExitDurM = "0";

            }
            if (PerExitDetailsMChk.IsChecked == true)
            {
                ExitDetailsM = "1";
            }
            else if (PerExitDetailsMChk.IsChecked == false)

            {
                ExitDetailsM = "0";

            }
            if (PerAmperChartMChk.IsChecked == true)
            {
                AmperChartM = "1";
            }
            else if (PerAmperChartMChk.IsChecked == false)
            {
                AmperChartM = "0";

            }

            editUser(username, TurnMenu, ExitsMenu, StationMenu, StationDurm, ExitDurM, ExitDetailsM, AmperChartM);

            if (username != "" && Newpassword != "")
            {
                editPassUser(username, Newpassword);

            }



        }

        private void changePass_Checked(object sender, RoutedEventArgs e)
        {
            if (changePass.IsChecked == true)
            {
                NewtxtPasswordUser.IsEnabled = true;

            }
            else { NewtxtPasswordUser.IsEnabled = false; }
        }

        private void EditAdminEnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewtxtPasswordAdmin.Password.Length > 0)
            {
                string adminPassword = NewtxtPasswordAdmin.Password;
                editPassAdmin(adminPassword);
            }
            else
            {

                MessageTextState.Content = "يرجى إدخال كلمة المرور الجديدة";
                SnackbarState.IsActive = true;

            }
        }

        private void popAdminCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            EditAdminPopup.IsOpen = false;

        }

        private void EditAdminPass_Click(object sender, RoutedEventArgs e)
        {
            EditAdminPopup.IsOpen = true;
            NewtxtPasswordAdmin.Password = "";

        }
        public void editPassAdmin(string passwordNew)
        {

            try
            {

                string sql = $"update users set password='" + passwordNew + "' where PERMISSION = 'مدير' ";

            using (var connection = new OracleConnection(Connection.ConnectionString))
            {
                using (OracleCommand command = new OracleCommand(sql, connection))
                {


                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    if (rowsAffected == 1)
                    {
                        MessageTextState.Content = "تم تعديل كلمة المرور";
                        SnackbarState.IsActive = true;

                        EditAdminPopup.IsOpen = false;
                    }
                    else
                    {
                        MessageTextState.Content = "فشل تعديل كلمة المرور";
                        SnackbarState.IsActive = true;

                        EditAdminPopup.IsOpen = false;

                    }

                }
            }



        }
                catch (OracleException ex)
                {
                    MessageTextState.Content = "فشل تعديل كلمة المرور";
                    SnackbarState.IsActive = true;
                    EditAdminPopup.IsOpen = false;

                }


}
        

    }
}

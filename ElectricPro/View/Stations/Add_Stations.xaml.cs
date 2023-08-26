using ElectricPro.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ElectricPro.View.Stations
{
    /// <summary>
    /// Interaction logic for Add_Stations.xaml
    /// </summary>
    public partial class Add_Stations : Page
    {
        public Add_Stations()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (add_StationTxt.Text.Length > 0)
                {
                    string sql = $"INSERT INTO stations (station) VALUES ('" + add_StationTxt.Text + "') ";

                    using (var connection = new OracleConnection(Connection.ConnectionString))
                    {
                        using (OracleCommand command = new OracleCommand(sql, connection))
                        {


                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            connection.Close();

                            if (rowsAffected == 1)
                            {

                                MessageTextState.Content = "تم إضافة محطة " + add_StationTxt.Text + "";
                                SnackbarState.IsActive = true;
                                //MessageBox.Show("تم تعديل الحالة بنجاح", "الحالة", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            }
                            else
                            {

                                MessageTextState.Content = "فشل إضافة محطة " + add_StationTxt.Text + "";
                                SnackbarState.IsActive = true;
                                //MessageBox.Show("! قشل تعديل الحالة", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                else
                {

                    MessageText.Content = "يرجى إدخال اسم المحطة";
                    SnackbarMes.IsActive = true;
                }

            }
            catch (OracleException ex)
            {
                if (ex.ErrorCode.ToString() == "-2147467259")
                {
                    MessageTextState.Content = "" + add_StationTxt.Text + " موجود مسبقاً ";
                    SnackbarState.IsActive = true;
                }
                else
                {
                    MessageTextState.Content = "فشل إضافة محطة " + add_StationTxt.Text + "";
                    SnackbarState.IsActive = true;
                }
            }
        }



        
 

        private void MessageText_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarMes.IsActive = false;
        }

        private void MessageTextState_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarState.IsActive = false;
        }
    }
}

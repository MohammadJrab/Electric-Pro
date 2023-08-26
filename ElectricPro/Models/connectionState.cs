using ElectricPro.View;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ElectricPro.Models
{
    internal class connectionState
    {
        

            private void initialize()
            {
            }
            OracleConnection conn = new OracleConnection
               (Connection.ConnectionString);
      

            public bool OpenConnection()
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (OracleException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            MessageBox.Show("...لا يمكن الإتصال بالسيرفر");
                            break;
                        case 1045:
                            MessageBox.Show(".. خطأ في اسم المستخدم او كلمة السر");
                            break;

                    }
                }
                return false;

            }
            public void close_conn()
            {

                this.conn.Close();
            }

            public OracleConnection get_Connection()
            {

                return this.conn;
            }

 
            



        }

    }
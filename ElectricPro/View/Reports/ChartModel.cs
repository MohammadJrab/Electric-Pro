//using LiveCharts.Configurations;
//using LiveCharts.Wpf;
//using LiveCharts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Media;
//using Oracle.ManagedDataAccess.Client;
//using ElectricPro.View.Reports;
//using LiveCharts.Defaults;
//using System.ComponentModel;

//namespace ElectricPro.View.Reports
//{
//    public class ChartViewModel : INotifyPropertyChanged

//{
//    public ChartViewModel()
//    {
//            ChartValues<DateTimePoint> chartsV = GetDataFromOracle();
//             ChartValues<ObservablePoint> mjr = new ChartValues<ObservablePoint>();
//            for (int i = 0; i <= chartsV.Count; i++)
//            {
//                mjr.Add(new ObservablePoint(chartsV[i].dateTime.Ticks, chartsV[i].Amper));
//                LabelFormatter[i] = chartsV[i].dateTime.ToString();
//            }
//            this.SeriesValues = mjr;
//    }
//        private ChartValues<DateTimePoint> GetDataFromOracle()
//        {
//            ChartValues<DateTimePoint> chartDataClasss = new ChartValues<DateTimePoint>();

//            string query = "SELECT  DateTime,Amper FROM Details  order by datetime asc ";

//            using (OracleConnection connection = new OracleConnection(Connection.ConnectionString))
//            {
//                connection.Open();
//                using (OracleCommand command = new OracleCommand(query, connection))
//                {
//                    using (OracleDataReader reader = command.ExecuteReader())
//                    {
//                        while (reader.Read())
//                        {

//                            DateTime sDate = DateTime.ParseExact(reader.GetString(0), "MM/dd/yyyy HH:mm:ss", null);


//                            int amperValue = reader.GetInt32(1);
//                            DateTimePoint cdc = new DateTimePoint();
//                            cdc.dateTime = sDate;
//                            cdc.Amper = amperValue;
//                            chartDataClasss.Add(cdc);
                            
//                            //chartDataClasss.Add(sDate,amperValue);
//                        }
//                    }
//                }
//            }

//            return chartDataClasss;
//        }
//      //  private static string ConvertTicksToDateTimeString(double value)
//      //=> new DateTime((long)value).ToString();

//        public ChartValues<ObservablePoint> SeriesValues { get; }
//            public string[] LabelFormatter;

//            public event PropertyChangedEventHandler PropertyChanged;
//         }

//    public class DateTimePoint
//    {
//        public DateTimePoint()
//        {
//        }

//        public DateTime dateTime { get; set; }
//        public int Amper { get; set; }
//    }
//}


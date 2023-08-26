using ClosedXML.Excel;
using LiveCharts.Wpf;
using LiveCharts;
using Microsoft.Win32;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
using DocumentFormat.OpenXml.Spreadsheet;
using Page = System.Windows.Controls.Page;
using System.IO;
using System.Windows.Threading;
using System.Drawing;
using System.Drawing.Printing;
using Brushes = System.Windows.Media.Brushes;
using Rectangle = System.Drawing.Rectangle;
using Colors = System.Windows.Media.Colors;
using System.Threading;
using System.ComponentModel;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DataTable = DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle.DataTable;
using LiveCharts.Helpers;
using System.Collections.ObjectModel;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using Point = System.Windows.Point;
using GradientStop = System.Windows.Media.GradientStop;
using Color = System.Windows.Media.Color;
using DocumentFormat.OpenXml.Vml;
using System.Runtime.ConstrainedExecution;
using System.Linq.Expressions;
using DocumentFormat.OpenXml.Office2010.ExcelAc;

namespace ElectricPro.View.Reports
{
    /// <summary>
    /// Interaction logic for AmperCharts.xaml
    /// </summary>
    public partial class AmperCharts : Page, INotifyPropertyChanged
    {
        public AmperCharts()
        {
            InitializeComponent();
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();

        }


        private static string ConvertTicksToDateTimeString(double value)
             => new DateTime((long)value).ToString();
        public Func<double, string> LabelFormatter => ConvertTicksToDateTimeString;

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {



            getStations();


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

        private SeriesCollection seriesCollection;

        private Thread updateThread;



        private async void
loadData()
        {

            App.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    if (FilterByComboExit.HasItems == true)
                    {

                        seriesCollection = new SeriesCollection();

                        ChartValues<DateTimePoint> chartsV = GetDataFromOracle(startDate.Text, endDate.Text, FilterByComboExit.SelectedItem.ToString());
                        ChartValues<ObservablePoint> mjr = new ChartValues<ObservablePoint>();
                        for (int i = 0; i < chartsV.Count; i++)
                        {
                            mjr.Add(new ObservablePoint(chartsV[i].dateTime.Ticks, chartsV[i].Amper));
                            ////LabelFormatter[i] = chartsV[i].dateTime.ToString();
                        }
                        this.SeriesValues = mjr;
                        DataContext = this;
                        AmperChart.Series = seriesCollection;
                        StepLineSeries lineSeries = new StepLineSeries
                        {
                            Title = "(A) الأمبير",
                            Values = SeriesValues,
                            PointGeometry = null,
                            Fill = Brushes.Transparent,
                            Effect = null,
                            ToolTip = null,


                        };
                        //AmperChart.Series.Clear();

                        seriesCollection.Add(lineSeries);

                    }



                    AmperChart.AxisY.Add(new Axis
                    {
                        MinValue = 0,
                        MaxValue = 140,
                        Title = "(A) الأمبير",
                        DisableAnimations = true,
                        ToolTip = null,
                        LabelFormatter = value => value.ToString()
                    });

                    AmperChart.AxisX.Add(new Axis
                    {
                        FontSize = 12,
                        //Foreground = Brushes.Gray,
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(0, 0, 0, 10),
                        Title = "الزمن",
                        DisableAnimations = true,
                        LabelFormatter = LabelFormatter,
                        //Labels = DateTime.Select(x => x.ToString("MM/dd/yyyy HH:mm:ss")).ToArray(),
                        UseLayoutRounding = true,
                        ToolTip = null


                    });

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            });


        }

        //static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.128.0.20)(PORT=1521))) (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cluster)));User Id=mjrsy;Password=admin;";


        private async Task DoWorkAsync()
        {
            // Perform background task or long-running operation here
            try
            {
                if (FilterByComboExit.HasItems == true)
                {

                    seriesCollection = new SeriesCollection();

                    ChartValues<DateTimePoint> chartsV = GetDataFromOracle(startDate.Text, endDate.Text, FilterByComboExit.SelectedItem.ToString());
                    ChartValues<ObservablePoint> mjr = new ChartValues<ObservablePoint>();
                    for (int i = 0; i < chartsV.Count; i++)
                    {
                        mjr.Add(new ObservablePoint(chartsV[i].dateTime.Ticks, chartsV[i].Amper));
                        ////LabelFormatter[i] = chartsV[i].dateTime.ToString();
                    }
                    this.SeriesValues = mjr;
                    DataContext = this;
                    //AmperChart.Series = seriesCollection;
                    //    StepLineSeries lineSeries = new StepLineSeries
                    //    {
                    //        Title = "(A) الأمبير",
                    //        Values = SeriesValues,
                    //        PointGeometry = null,
                    //        Fill = Brushes.Transparent,
                    //        Effect = null,
                    //        ToolTip=null,


                    //    };
                    //    //AmperChart.Series.Clear();

                    //    seriesCollection.Add(lineSeries);

                    //}



                    //AmperChart.AxisY.Add(new Axis
                    //{
                    //    MinValue = 0,
                    //    MaxValue = 140,
                    //    Title = "(A) الأمبير",
                    //    DisableAnimations = true,
                    //    ToolTip = null,
                    //    LabelFormatter = value => value.ToString()
                    //});

                    //AmperChart.AxisX.Add(new Axis
                    //{
                    //    FontSize = 12,
                    //    //Foreground = Brushes.Gray,
                    //    FontWeight = FontWeights.Bold,
                    //   Margin=new Thickness(0,0,0,10),
                    //    Title = "الزمن",
                    //    DisableAnimations = true,
                    //    LabelFormatter = LabelFormatter,
                    //    //Labels = DateTime.Select(x => x.ToString("MM/dd/yyyy HH:mm:ss")).ToArray(),
                    //    UseLayoutRounding = true,
                    //  ToolTip = null


                    //});
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }


        string startTime, endTime, exit;
        private async void enterBtn_Click(object sender, RoutedEventArgs e)
        {

            //Amper.Clear();
            //AmperStr.Clear();
            //DateTime1.Clear();

            if (FilterByCombo.Text == null && FilterByComboExit.Text == null)
            {
                System.Windows.MessageBox.Show("يرجى إختيار المخرج", "تحذير", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                try
                {

                    if (startDate.Text != "" && endDate.Text != "")
                    {
                        startTime = "";
                        endTime = "";
                        exit = "";
                        //AmperChart.Series.Clear();
                        //AmperChart.AxisY.Clear();
                        //AmperChart.AxisX.Clear();
                        progresssRing.Visibility = Visibility.Visible;
                        enterBtn.IsEnabled = false;
                        //if(seriesCollection?.Count>0)
                        //seriesCollection.Clear();
                        //await DoWorkAsync();
                        //if(ser.Values!=null)
                        //ser?.Values.Clear();
                        //if(xFor.Labels!=null)
                        //xFor?.Labels.Clear();
                        //if (yFor.Labels != null)

                        //    yFor?.Labels.Clear();

                        //AmperChart.AxisY.Clear();
                        //AmperChart.AxisX.Clear();
                        //AmperStr.Clear();
                        //DateTime1.Clear();
                        //AmperChart.Series[0].Values.Clear();
                        while (seriesCollection?.Count > 0)
                        {
                            seriesCollection.RemoveAt(seriesCollection.Count - 1);
                        }
                        AmperChart.AxisY.Clear();
                        AmperChart.AxisX.Clear();
                        if (SeriesValues != null)
                            SeriesValues.Clear();
                        startTime = startDate.Text;
                        endTime = endDate.Text;
                        exit = FilterByComboExit.SelectedItem.ToString();
                        BackgroundWorker worker2 = new BackgroundWorker();
                        worker2.WorkerReportsProgress = true;
                        worker2.DoWork += Worker_DoWorkEnter;
                        worker2.RunWorkerCompleted += Worker_RunWorkerCompletedEnter;
                        worker2.RunWorkerAsync();


                    }
                    else { MessageBox.Show("يرجى تحديد التاريخ أو المسافة"); }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }


            }

        }

        private async void Worker_DoWorkEnter(object sender, DoWorkEventArgs e)
        {
            //loadData();
            //try
            //{
            
        
            seriesCollection = new SeriesCollection();
            if(SeriesValues!=null)
            SeriesValues.Clear();

            ChartValues<DateTimePoint> chartsV = GetDataFromOracle(startTime, endTime, exit);
            ChartValues<ObservablePoint> mjr = new ChartValues<ObservablePoint>();
            for (int i = 0; i < chartsV.Count; i++)
            {
                mjr.Add(new ObservablePoint(chartsV[i].dateTime.Ticks, chartsV[i].Amper));
                ////LabelFormatter[i] = chartsV[i].dateTime.ToString();
            }
            this.SeriesValues = mjr;
            Application.Current.Dispatcher.Invoke((Action)delegate {
                // your code
            StepLineSeries lineSeries = new StepLineSeries
            {
                Title = "(A) الأمبير",
                Values = SeriesValues,
                PointGeometry = null,
                Fill = Brushes.Transparent,
                Effect = null,
                ToolTip = null,


            };
                AmperChart.Series.Clear();
                seriesCollection.Clear(); 
            seriesCollection.Add(lineSeries);




            AmperChart.AxisY.Add(new Axis
            {
                MinValue = 0,
                MaxValue = 140,
                Title = "(A) الأمبير",
                DisableAnimations = true,
                ToolTip = null,
                LabelFormatter = value => value.ToString()
            });

            AmperChart.AxisX.Add(new Axis
            {
                FontSize = 12,
                //Foreground = Brushes.Gray,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 10),
                Title = "الزمن",
                DisableAnimations = true,
                LabelFormatter = LabelFormatter,
                //Labels = DateTime.Select(x => x.ToString("MM/dd/yyyy HH:mm:ss")).ToArray(),
                UseLayoutRounding = true,
                ToolTip = null


            });
                AmperChart.Series = seriesCollection;

                DataContext = this;
            });

     
            //}

            //catch (Exception ex) { MessageBox.Show(ex.Message); }
        }



        private void Worker_RunWorkerCompletedEnter(object sender, RunWorkerCompletedEventArgs e)
        {
            enterBtn.IsEnabled = true;
            progresssRing.Visibility = Visibility.Collapsed;

        }


        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(AmperChart, "مخطط الأمبير");
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

        }

        private void saveAsPictureBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG (*.png)|*.png";
                saveFileDialog.FileName = FilterByComboExit.SelectedItem.ToString();
                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;
                    if (!filePath.EndsWith(".png"))
                    {
                        filePath += ".png";

                    }
                    WriteToPng(AmperChart, filePath);


                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void WriteToPng(UIElement element, string filename)
        {
            try
            {
                var rect = new Rect(element.RenderSize);
                var visual = new DrawingVisual();

                using (var dc = visual.RenderOpen())
                {
                    dc.DrawRectangle(new VisualBrush(element), null, rect);
                }

                var bitmap = new RenderTargetBitmap(
                    (int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Default);
                bitmap.Render(visual);

                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                using (var file = File.OpenWrite(filename))
                {
                    encoder.Save(file);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void DisplatMessage()
        {
            int milliseconds = 8000;
            Dispatcher.Invoke(() => SnackbarFive.IsActive = true);

            Thread.Sleep(milliseconds);
            Dispatcher.Invoke(() => SnackbarFive.IsActive = false);



        }






        private ChartValues<DateTimePoint> GetDataFromOracle(string startDate, string endDate, string Exit)
        {
            ChartValues<DateTimePoint> chartDataClasss = new ChartValues<DateTimePoint>();

            string query = "SELECT  DateTime,Amper FROM Details where DateTime Between '"+ startDate + "' and '"+ endDate + "' and Exit='"+ Exit + "'  order by datetime asc ";

            using (OracleConnection connection = new OracleConnection(Connection.ConnectionString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand(query, connection))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            DateTime sDate = DateTime.ParseExact(reader.GetString(0), "MM/dd/yyyy HH:mm:ss", null);


                            int amperValue = reader.GetInt32(1);
                            DateTimePoint cdc = new DateTimePoint();
                            cdc.dateTime = sDate;
                            cdc.Amper = amperValue;
                            chartDataClasss.Add(cdc);

                            //chartDataClasss.Add(sDate,amperValue);
                        }
                    }
                }
            }

            return chartDataClasss;
        }
        //  private static string ConvertTicksToDateTimeString(double value)
        //=> new DateTime((long)value).ToString();

        public ChartValues<ObservablePoint> SeriesValues { get; set; }
        //public string[] LabelFormatter;

        public event PropertyChangedEventHandler PropertyChanged;

 
    }
    public class DateTimePoint
        {
            public DateTimePoint()
            {
            }

            public DateTime dateTime { get; set; }
            public int Amper { get; set; }
        }
  
}


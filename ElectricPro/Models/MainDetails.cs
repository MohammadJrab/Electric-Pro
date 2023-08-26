using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricPro.Models
{
    public class MainDetails
    {
        public string Station { get; set; }
        public string Exit { get; set; }
        public string Amper { get; set; }
        public string StartTime { get; set; }
        public string OnTime { get; set; }
        public string Status { get; set; }
        public string LastUpdate{ get; set; }
        public string ColorCard { get; set; }


        //public MainDetails(string station, string exit, string amper, string startTime, string onTime, string status,  string lastUpdate, string colorCard)
        //{
        //    Station = station;
        //    Exit = exit;
        //    Amper = amper;
        //    StartTime = startTime;
        //    OnTime = onTime;
        //    Status = status;
        //    LastUpdate = lastUpdate;
        //    ColorCard = colorCard;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ElectricPro
{
    public class Connection
    {
        // Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.128.0.20)(PORT=1521))) (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cluster)));User Id=mjrsy;Password=admin;
        //Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id = mjrsy; Password=admin;Connection Timeout =1
        public static String ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.128.0.20)(PORT=1521))) (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cluster)));User Id=mjrsy;Password=admin;Connection Timeout =1";
        //public static String ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id = mjrsy; Password=admin;Connection Timeout =1";

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApp
{
    static class Config
    {
        static public string DB_SERVER = "192.168.0.8";
        static public string DB_USERID = "student";
        static public string DB_PASSWORD = "studentpass";
        static public string DB_DATABASE = "thein";
        static public string DB_DATASOURSE = "datasource = " + DB_SERVER + "; database=" + DB_DATABASE + ";userid=" + DB_USERID + ";password=" + DB_PASSWORD;
    }
}

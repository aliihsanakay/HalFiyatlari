using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalFiyatlari.Business.Helper
{
    public class GlobalSettings
    {
        public static string ConnectionString
        {
            get
            {
                string cs = string.Empty;
                MySqlConnectionStringBuilder msbuilder = new MySqlConnectionStringBuilder();

                msbuilder.Server = "localhost";
                msbuilder.Port = 3306;
                msbuilder.Database = "halapp";
                msbuilder.UserID = "adminuser";
                msbuilder.Password = "";
                msbuilder.ConnectionTimeout = 120;

                cs = msbuilder.ToString();

                return cs;
            }
        }
    }
}

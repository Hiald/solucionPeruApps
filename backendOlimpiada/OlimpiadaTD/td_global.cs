using System;
using System.Configuration;


namespace OlimpiadaTD
{
    public class td_global
    {
        public String sqlConexion { get; set; }

        public td_global()
        {
            sqlConexion = ConfigurationManager.ConnectionStrings["cnSqlServer"].ConnectionString;
        }
    }
}

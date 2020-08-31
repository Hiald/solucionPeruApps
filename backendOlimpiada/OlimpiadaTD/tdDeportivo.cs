using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using OlimpiadaAD;
using OlimpiadaED;

namespace OlimpiadaTD
{
    public class tdDeportivo : td_global
    {
        adDeportivo iadDeportivo;

        public int tdRegistrarDeportivo(string tdlocalizacion, string tdjefeOrganizacion, string tdareatotal, int tdsedeid)
        {
            try
            {
                int iresultado = -2;
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    con.Open();
                    iadDeportivo = new adDeportivo(con);
                    iresultado = iadDeportivo.adRegistrarDeportivo(tdlocalizacion, tdjefeOrganizacion, tdareatotal, tdsedeid);
                }
                return iresultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ed_deportivo> tdListarDeportivo(int tdtipolistado, int tddeportivoid)
        {
            try
            {
                List<ed_deportivo> loenDeportivo = new List<ed_deportivo>();
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    con.Open();
                    iadDeportivo = new adDeportivo(con);
                    loenDeportivo = iadDeportivo.adListarDeportivo(tdtipolistado, tddeportivoid);
                }
                return loenDeportivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int tdActualizarDeportivo(int tddeportivoid, string tdlocalizacion, string tdjefeorganizacion, string tdareatotal, int tdsedeid)
        {
            try
            {
                int iresultado = -2;
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    con.Open();
                    iadDeportivo = new adDeportivo(con);
                    iresultado = iadDeportivo.adActualizarDeportivo(tddeportivoid, tdlocalizacion, tdjefeorganizacion, tdareatotal, tdsedeid);
                }
                return iresultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int tdEliminarDeportivo(int tddeportivoid)
        {
            try
            {
                int iresultado = -2;
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    con.Open();
                    iadDeportivo = new adDeportivo(con);
                    iresultado = iadDeportivo.adEliminarDeportivo(tddeportivoid);

                    return iresultado;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

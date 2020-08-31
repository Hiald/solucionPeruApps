using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using OlimpiadaAD;
using OlimpiadaED;

namespace OlimpiadaTD
{
    public class tdSede : td_global
    {
        adSede iadSede;

        public int tdRegistrarSede(string tdnombre, int tdcomplejo, decimal tdresupuesto)
        {
            try
            {
                int iresultado = -2;
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    con.Open();
                    iadSede = new adSede(con);
                    iresultado = iadSede.adRegistrarSede(tdnombre, tdcomplejo, tdresupuesto);
                }
                return iresultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ed_sede> tdListarSede(int tdtipolistado, int tdsede)
        {
            try
            {
                List<ed_sede> loenSede = new List<ed_sede>();
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    con.Open();
                    iadSede = new adSede(con);
                    loenSede = iadSede.adListarSede(tdtipolistado, tdsede);
                }
                return loenSede;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int tdActualizarSede(int tdsedeid, string tdnombre, int tdcomplejo, decimal tdpresupuesto)
        {
            try
            {
                int iresultado = -2;
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    con.Open();
                    iadSede = new adSede(con);
                    iresultado = iadSede.adActualizarSede(tdsedeid, tdnombre, tdcomplejo, tdpresupuesto);
                }
                return iresultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int tdEliminarSede(int tdsedeid)
        {
            try
            {
                int iresultado = -2;
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    con.Open();
                    iadSede = new adSede(con);
                    iresultado = iadSede.adEliminarSede(tdsedeid);

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

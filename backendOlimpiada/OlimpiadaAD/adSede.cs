using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using OlimpiadaED;

namespace OlimpiadaAD
{
    public class adSede : ad_global
    {
        public adSede(SqlConnection cn)
        {
            cnSqlServer = cn;
        }

        public int adRegistrarSede(string adnombre, int adcomplejo, decimal adpresupuesto)
        {
            try
            {
                int result = -1;
                SqlCommand cmd = new SqlCommand("SP_SEDE_CREAR", cnSqlServer);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = adnombre;
                cmd.Parameters.Add("@complejo_numero", SqlDbType.Int).Value = adcomplejo;
                cmd.Parameters.Add("@presupuesto", SqlDbType.Decimal).Value = adpresupuesto;
                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ed_sede> adListarSede(int adtipolistado, int adsede)
        {
            try
            {
                List<ed_sede> loenSede = new List<ed_sede>();
                using (SqlCommand cmd = new SqlCommand("SP_SEDE_LISTAR", cnSqlServer))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tipolistado", SqlDbType.Int).Value = adtipolistado;
                    cmd.Parameters.Add("@sede_id", SqlDbType.Int).Value = adsede;
                    using (SqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            ed_sede senSede = null;
                            int pos_sedeid = mdrd.GetOrdinal("sede_id");
                            int pos_nombre = mdrd.GetOrdinal("nombre");
                            int pos_complejonumero = mdrd.GetOrdinal("complejo_numero");
                            int pos_presupuesto = mdrd.GetOrdinal("presupuesto");
                            while (mdrd.Read())
                            {
                                senSede = new ed_sede();
                                senSede.Isede_id = (mdrd.IsDBNull(pos_sedeid) ? 0 : mdrd.GetInt32(pos_sedeid));
                                senSede.Snombre = (mdrd.IsDBNull(pos_nombre) ? "-" : mdrd.GetString(pos_nombre));
                                senSede.Icomplejo_numero = (mdrd.IsDBNull(pos_complejonumero) ? 0 : mdrd.GetInt32(pos_complejonumero));
                                senSede.Dpresupuesto = (mdrd.IsDBNull(pos_presupuesto) ? 0 : mdrd.GetDecimal(pos_presupuesto));
                                loenSede.Add(senSede);
                            }
                        }
                    }
                    return loenSede;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int adActualizarSede(int adsedeid, string adnombre, int adcomplejo, decimal adpresupuesto)
        {
            try
            {
                int result = -1;
                SqlCommand cmd = new SqlCommand("SP_SEDE_ACTUALIZAR", cnSqlServer);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@sede_id", SqlDbType.Int).Value = adsedeid;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = adnombre;
                cmd.Parameters.Add("@complejo_numero", SqlDbType.Int).Value = adcomplejo;
                cmd.Parameters.Add("@presupuesto", SqlDbType.Decimal).Value = adpresupuesto;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int adEliminarSede(int adsedeid)
        {
            try
            {
                int result = -1;
                SqlCommand cmd = new SqlCommand("SP_SEDE_ELIMINAR", cnSqlServer);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@sede_id", SqlDbType.Int).Value = adsedeid;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

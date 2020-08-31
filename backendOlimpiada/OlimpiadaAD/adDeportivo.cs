using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using OlimpiadaED;

namespace OlimpiadaAD
{
    public class adDeportivo : ad_global
    {
        public adDeportivo(SqlConnection cn)
        {
            cnSqlServer = cn;
        }

        public int adRegistrarDeportivo(string adlocalizacion, string adjefeOrganizacion, string adareatotal, int adsedeid)
        {
            try
            {
                int result = -1;
                SqlCommand cmd = new SqlCommand("SP_DEPORTIVO_CREAR", cnSqlServer);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@localizacion", SqlDbType.VarChar, 100).Value = adlocalizacion;
                cmd.Parameters.Add("@jefe_organizacion", SqlDbType.VarChar, 150).Value = adjefeOrganizacion;
                cmd.Parameters.Add("@area_total", SqlDbType.VarChar, 150).Value = adareatotal;
                cmd.Parameters.Add("@sede_id", SqlDbType.Int).Value = adsedeid;
                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ed_deportivo> adListarDeportivo(int adtipolistado, int addeportivoid)
        {
            try
            {
                List<ed_deportivo> loenDeportivo = new List<ed_deportivo>();
                using (SqlCommand cmd = new SqlCommand("SP_DEPORTIVO_LISTAR", cnSqlServer))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tipolistado", SqlDbType.Int).Value = adtipolistado;
                    cmd.Parameters.Add("@deportivo_id", SqlDbType.Int).Value = addeportivoid;
                    using (SqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            ed_deportivo senDeportivo = null;
                            int pos_deportivoid = mdrd.GetOrdinal("deportivo_id");
                            int pos_localizacion = mdrd.GetOrdinal("localizacion");
                            int pos_jefeorganizacion = mdrd.GetOrdinal("jefe_organizacion");
                            int pos_areatotal = mdrd.GetOrdinal("area_total");
                            int pos_sedeid = mdrd.GetOrdinal("sede_id");
                            int pos_nombre = mdrd.GetOrdinal("nombre");

                            while (mdrd.Read())
                            {
                                senDeportivo = new ed_deportivo();
                                senDeportivo.Ideportivo_id = (mdrd.IsDBNull(pos_deportivoid) ? 0 : mdrd.GetInt32(pos_deportivoid));
                                senDeportivo.Slocalizacion = (mdrd.IsDBNull(pos_localizacion) ? "-" : mdrd.GetString(pos_localizacion));
                                senDeportivo.Sjefe_organizacion = (mdrd.IsDBNull(pos_jefeorganizacion) ? "-" : mdrd.GetString(pos_jefeorganizacion));
                                senDeportivo.Sarea_total = (mdrd.IsDBNull(pos_areatotal) ? "-" : mdrd.GetString(pos_areatotal));
                                senDeportivo.Isede_id = (mdrd.IsDBNull(pos_sedeid) ? 0 : mdrd.GetInt32(pos_sedeid));
                                senDeportivo.SNombre = (mdrd.IsDBNull(pos_nombre) ? "-" : mdrd.GetString(pos_nombre));
                                loenDeportivo.Add(senDeportivo);
                            }
                        }
                    }
                    return loenDeportivo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int adActualizarDeportivo(int addeportivoid, string adlocalizacion, string adjefeorganizacion, string adareatotal, int adsedeid)
        {
            try
            {
                int result = -1;
                SqlCommand cmd = new SqlCommand("SP_DEPORTIVO_ACTUALIZAR", cnSqlServer);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@deportivo_id", SqlDbType.Int).Value = addeportivoid;
                cmd.Parameters.Add("@localizacion", SqlDbType.VarChar, 100).Value = adlocalizacion;
                cmd.Parameters.Add("@jefe_organizacion", SqlDbType.VarChar, 150).Value = adjefeorganizacion;
                cmd.Parameters.Add("@area_total", SqlDbType.VarChar, 150).Value = adareatotal;
                cmd.Parameters.Add("@sede_id", SqlDbType.Int).Value = adsedeid;
                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int adEliminarDeportivo(int addeportivoid)
        {
            try
            {
                int result = -1;
                SqlCommand cmd = new SqlCommand("SP_DEPORTIVO_ELIMINAR", cnSqlServer);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@deportivo_id", SqlDbType.Int).Value = addeportivoid;
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

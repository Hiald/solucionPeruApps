using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using OlimpiadaED;

namespace OlimpiadaAD
{
    public class adUsuario : ad_global
    {
        public adUsuario(SqlConnection cn)
        {
            cnSqlServer = cn;
        }

        public int adRegistrarUsuario(string adusuario, string adclave)
        {
            try
            {
                int result = -1;
                // se apertura un comando sql, en este caso adjunto el procedimiento y la conexion tomada de webconfig
                SqlCommand cmd = new SqlCommand("SP_USUARIO_CREAR", cnSqlServer);
                //se declara que es un procedimiento almacenado
                cmd.CommandType = CommandType.StoredProcedure;
                //parametros
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 100).Value = adusuario;
                cmd.Parameters.Add("@clave", SqlDbType.VarChar, 100).Value = adclave;
                //obtengo solo el primer resultado de la primera celda y lo convierto a int32 (el id generado de ser asi)
                result = Convert.ToInt32(cmd.ExecuteScalar());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ed_usuario adListarUsuario(string adusuario, string adclave)
        {
            try
            {
                // declaro una instancia de la clase usuario en vacio
                ed_usuario senUsuario = null;
                using (SqlCommand cmd = new SqlCommand("SP_USUARIO_LISTAR", cnSqlServer))
                {
                   // procedimiento almacenado se declara y los parametros
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 100).Value = adusuario;
                    cmd.Parameters.Add("@clave", SqlDbType.VarChar, 100).Value = adclave;
                    //ejecuto un data reader, para leer los datos obtenidos
                    using (SqlDataReader mdrd = cmd.ExecuteReader())
                    {
                        if (mdrd != null)
                        {
                            //obtengo los puntos como indices de los campos declarados
                            int pos_usuarioid = mdrd.GetOrdinal("usuario_id");
                            int pos_usuario = mdrd.GetOrdinal("usuario");
                            while (mdrd.Read())
                            {
                                //llamo a una instancia vacia de usuario
                                senUsuario = new ed_usuario();
                                //obtengo los valores de acuerdo a los indices obtenidos anteriormente, y los convierto de acuerdo a tu tipo de dato
                                senUsuario.idusuario = (mdrd.IsDBNull(pos_usuarioid) ? 0 : mdrd.GetInt32(pos_usuarioid));
                                senUsuario.Susuario = (mdrd.IsDBNull(pos_usuario) ? "-" : mdrd.GetString(pos_usuario));
                            }
                        }
                    }
                    //devolvemos la clase usuario ya llenada
                    return senUsuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

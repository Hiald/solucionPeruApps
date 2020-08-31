using System.Data.SqlClient;
using System;
using OlimpiadaAD;
using OlimpiadaED;

namespace OlimpiadaTD
{
    public class tdUsuario : td_global
    {
        adUsuario itdUsuario;

        public int tdRegistrarUsuario(string tdusuario, string tdclave)
        {
            try
            {
                int iresultado = -2;
                //declaro una transaccion sql
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    //aperturo la transaccion
                    con.Open();
                    //llamo a la instancia de usuario con la conexion
                    itdUsuario = new adUsuario(con);
                    //asigno al resultado de la transaccion
                    iresultado = itdUsuario.adRegistrarUsuario(tdusuario, tdclave);
                }
                //Retorno el resultado, a este punto la conexion ya se ha cerrado
                //De ocurrir algun error, salta al exception y ocurre un rollback en el base de datos
                return iresultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ed_usuario tdListarUsuario(string tdusuario, string tdclave)
        {
            try
            {
                ed_usuario loenUsuario = new ed_usuario();
                using (SqlConnection con = new SqlConnection(sqlConexion))
                {
                    con.Open();
                    itdUsuario = new adUsuario(con);
                    loenUsuario = itdUsuario.adListarUsuario(tdusuario, tdclave);
                }
                return loenUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

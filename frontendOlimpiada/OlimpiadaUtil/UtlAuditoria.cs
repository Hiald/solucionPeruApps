using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace OlimpiadaUtil
{
    public class UtlAuditoria
    {
        private const string SESSION_IDUSUARIO = "SESSION_IDUSUARIO";
        private const string SESSION_SNOMBRE = "SESSION_SUSUARIO";

        #region "Obtiene Datos del Usuario"

        public static int ObtenerIdUsuario()
        {
            return ((HttpContext.Current.Session[SESSION_IDUSUARIO] == null) ? 0 : int.Parse(HttpContext.Current.Session[SESSION_IDUSUARIO].ToString()));
        }
        public static string ObtenerNombre()
        {
            return ((HttpContext.Current.Session[SESSION_SNOMBRE] == null) ? "" : HttpContext.Current.Session[SESSION_SNOMBRE].ToString());
        }

        public static string ObtenerFechaSistema()
        {
            return DateTime.Now.ToShortDateString();
        }

        public static void SetSessionValues(Dictionary<string, string> DVariables)
        {
            try
            {
                HttpContext.Current.Session[SESSION_IDUSUARIO] = DVariables["USUARIOID"];
                HttpContext.Current.Session[SESSION_SNOMBRE] = DVariables["NOMBRE"];
                HttpContext.Current.Session.Timeout = 24 * 60;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public static bool ValidarSession()
        {
            try
            {
                if (HttpContext.Current.Session[SESSION_IDUSUARIO] == null ||
                HttpContext.Current.Session[SESSION_SNOMBRE] == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool CerrarSession()
        {
            try
            {
                HttpContext.Current.Session[SESSION_IDUSUARIO] = null;
                HttpContext.Current.Session[SESSION_SNOMBRE] = null;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static int MostrarTiempoSesion()
        {
            try
            {
                var iTiempoSesion = HttpContext.Current.Session.Timeout;
                return iTiempoSesion;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

    }
}

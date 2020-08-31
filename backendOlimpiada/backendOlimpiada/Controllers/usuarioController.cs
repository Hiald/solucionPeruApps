using Newtonsoft.Json;
using System;
using System.Web.Http;
using OlimpiadaED;
using OlimpiadaTD;

namespace backendOlimpiada.Controllers
{
    public class usuarioController : ApiController
    {
        tdUsuario itdUsuario;

        [HttpGet]
        public int APIRegistrarUsuario(string wsusuario, string wsclave)
        {
            try
            {
                int iresultado = -2;
                //instancio usuario
                itdUsuario = new tdUsuario();
                //asigno al resultado la ejecucion
                iresultado = itdUsuario.tdRegistrarUsuario(wsusuario, wsclave);
                return iresultado;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        [HttpGet]
        public string APIListarUsuario(string wusuario, string wclave, string valor)
        {
            try
            {
                ed_usuario enSede = new ed_usuario();
                itdUsuario = new tdUsuario();
                enSede = itdUsuario.tdListarUsuario(wusuario, wclave);
                return JsonConvert.SerializeObject(enSede);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

    }
}
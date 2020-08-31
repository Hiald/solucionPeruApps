using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using OlimpiadaED;
using OlimpiadaTD;

namespace backendOlimpiada.Controllers
{
    public class deportivoController : ApiController
    {
        tdDeportivo itdDeportivo;

        [HttpGet]
        public int APIRegistrarDeportivo(string wslocalizacion, string wsjefeOrganizacion, string wsareatotal, int wssedeid)
        {
            try
            {
                int iresultado = -2;
                itdDeportivo = new tdDeportivo();
                iresultado = itdDeportivo.tdRegistrarDeportivo(wslocalizacion, wsjefeOrganizacion, wsareatotal, wssedeid);
                return iresultado;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        [HttpGet]
        public string APIListarDeportivo(int wstipolistado, int wsdeportivoid)
        {
            try
            {
                List<ed_deportivo> loenDeportivo = new List<ed_deportivo>();
                itdDeportivo = new tdDeportivo();
                loenDeportivo = itdDeportivo.tdListarDeportivo(wstipolistado, wsdeportivoid);
                return JsonConvert.SerializeObject(loenDeportivo);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [HttpGet]
        public int APIActualizarDeportivo(int wsdeportivoid, string wslocalizacion, string wsjefeorganizacion, string wsareatotal, int wssedeid)
        {
            try
            {
                int iresultado = -2;
                itdDeportivo = new tdDeportivo();
                iresultado = itdDeportivo.tdActualizarDeportivo(wsdeportivoid, wslocalizacion, wsjefeorganizacion, wsareatotal, wssedeid);
                return iresultado;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        [HttpGet]
        public int APIEliminarDeportivo(int wdeportivoid)
        {
            try
            {
                int iresultado = -2;
                itdDeportivo = new tdDeportivo();
                iresultado = itdDeportivo.tdEliminarDeportivo(wdeportivoid);
                return iresultado;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

    }
}
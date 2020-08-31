using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using OlimpiadaED;
using OlimpiadaTD;

namespace backendOlimpiada.Controllers
{
    public class sedeController : ApiController
    {
        tdSede itdSede;

        [HttpGet]
        public int APIRegistrarSede(string wsnombre, int wscomplejo, decimal wsresupuesto)
        {
            try
            {
                int iresutado = -2;
                itdSede = new tdSede();
                iresutado = itdSede.tdRegistrarSede(wsnombre, wscomplejo, wsresupuesto);
                return iresutado;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        [HttpGet]
        public string APIListarSede(int wstipolistado, int sedeid)
        {
            try
            {
                List<ed_sede> loenSede = new List<ed_sede>();
                itdSede = new tdSede();
                loenSede = itdSede.tdListarSede(wstipolistado, sedeid);
                return JsonConvert.SerializeObject(loenSede);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [HttpGet]
        public int APIActualizarSede(int wssedeid, string wsnombre, int wscomplejo, decimal wspresupuesto)
        {
            try
            {
                int iresutado = -2;
                itdSede = new tdSede();
                iresutado = itdSede.tdActualizarSede(wssedeid, wsnombre, wscomplejo, wspresupuesto);
                return iresutado;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

        [HttpGet]
        public int APIEliminarSede(int wsedeid)
        {
            try
            {
                int iresultado = -2;
                itdSede = new tdSede();
                iresultado = itdSede.tdEliminarSede(wsedeid);
                return iresultado;
            }
            catch (Exception ex)
            {
                return -2;
            }
        }

    }
}
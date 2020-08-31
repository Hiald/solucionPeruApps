using OlimpiadaED;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using frontendOlimpiada.Filter;

namespace frontendOlimpiada.Controllers
{
    public class sedeController : Controller
    {
        [SecuritySession]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ListarSede(int wstipolistado, int sedeid)
        {
            try
            {
                var objResultado = new object();
                List<ed_sede> loenSede = new List<ed_sede>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteOlimpiadaBE);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ReslistarSede = await client.GetAsync("api/sede/APIListarSede?wstipolistado=" + wstipolistado + "&sedeid="+ sedeid);
                    if (ReslistarSede.IsSuccessStatusCode)
                    {
                        var rwsapilu = ReslistarSede.Content.ReadAsAsync<string>().Result;
                        loenSede = JsonConvert.DeserializeObject<List<ed_sede>>(rwsapilu);
                    }
                    else
                    {
                        loenSede[0].Isede_id = -1;
                    }
                }

                //datos a enviar a datatable
                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = true,
                    iTotalRecords = loenSede.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenSede
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }

        [HttpPost]
        public async Task<JsonResult> RegistrarSede(string wsnombre, int wscomplejo, decimal wsresupuesto)
        {
            try
            {
                var objResultado = new object();

                int iresultadoreg = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteOlimpiadaBE);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reswsru = await client.GetAsync("api/sede/APIRegistrarSede?wsnombre=" + wsnombre
                                                        + "&wscomplejo=" + wscomplejo + "&wsresupuesto=" + wsresupuesto);
                    if (Reswsru.IsSuccessStatusCode)
                    {
                        var lpoSede = Reswsru.Content.ReadAsAsync<string>().Result;
                        iresultadoreg = int.Parse(lpoSede);

                        if (iresultadoreg == -1)
                        {
                            objResultado = new
                            {
                                iResultado = -5,
                                iResultadoIns = "Ha ocurrido un error, inténtelo nuevamente"
                            };
                        }
                    }
                }

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "registrado"
                };

                return Json(objResultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> ActualizarSede(int wssedeid, string wsnombre, int wscomplejo, decimal wspresupuesto)
        {
            try
            {
                var objResultado = new object();

                int iresultadoreg = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteOlimpiadaBE);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reswsru = await client.GetAsync("api/sede/APIActualizarSede?wssedeid=" + wssedeid
                                                        + "&wsnombre=" + wsnombre + "&wscomplejo=" + wscomplejo + "&wspresupuesto=" + wspresupuesto);
                    if (Reswsru.IsSuccessStatusCode)
                    {
                        var lpoUpdSede = Reswsru.Content.ReadAsAsync<string>().Result;
                        iresultadoreg = int.Parse(lpoUpdSede);

                        if (iresultadoreg == -1)
                        {
                            objResultado = new
                            {
                                iResultado = -5,
                                iResultadoIns = "Ha ocurrido un error, inténtelo nuevamente"
                            };
                        }
                    }
                }

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "registrado"
                };

                return Json(objResultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> EliminarSede(int wsedeid)
        {
            try
            {
                var objResultado = new object();

                int iresultadoreg = -1;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteOlimpiadaBE);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reswsru = await client.GetAsync("api/sede/APIEliminarSede?wsedeid=" + wsedeid);
                    if (Reswsru.IsSuccessStatusCode)
                    {
                        var lpoDelSede = Reswsru.Content.ReadAsAsync<string>().Result;
                        iresultadoreg = int.Parse(lpoDelSede);

                        if (iresultadoreg == -1)
                        {
                            objResultado = new
                            {
                                iResultado = -5,
                                iResultadoIns = "Ha ocurrido un error, inténtelo nuevamente"
                            };
                        }
                    }
                }

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "correcto"
                };

                return Json(objResultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
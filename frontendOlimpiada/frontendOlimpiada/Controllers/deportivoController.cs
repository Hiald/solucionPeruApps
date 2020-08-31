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
    public class deportivoController : Controller
    {
        [SecuritySession]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ListarDeportivo(int tipolistado, int deportivoid)
        {
            try
            {
                var objResultado = new object();
                List<ed_deportivo> loenDeportivo = new List<ed_deportivo>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteOlimpiadaBE);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage ReslistarDeportivo = await client.GetAsync("api/deportivo/APIListarDeportivo?wstipolistado=" + tipolistado
                                                            + "&wsdeportivoid=" + deportivoid);
                    if (ReslistarDeportivo.IsSuccessStatusCode)
                    {
                        var rwsapilu = ReslistarDeportivo.Content.ReadAsAsync<string>().Result;
                        loenDeportivo = JsonConvert.DeserializeObject<List<ed_deportivo>>(rwsapilu);
                    }
                    else
                    {
                        loenDeportivo[0].Ideportivo_id = -1;
                    }
                }

                objResultado = new
                {
                    PageStart = 1,
                    pageSize = 100,
                    SearchText = string.Empty,
                    ShowChildren = true,
                    iTotalRecords = loenDeportivo.Count,
                    iTotalDisplayRecords = 1,
                    aaData = loenDeportivo
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpPost]
        public async Task<JsonResult> RegistrarDeportivo(string wslocalizacion, string wsjefeOrganizacion, string wsareatotal, int wssedeid)
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
                    HttpResponseMessage Reswsru = await client.GetAsync("api/deportivo/APIRegistrarDeportivo?wslocalizacion=" + wslocalizacion
                                                        + "&wsjefeOrganizacion=" + wsjefeOrganizacion + "&wsareatotal=" + wsareatotal
                                                        + "&wssedeid=" + wssedeid);
                    if (Reswsru.IsSuccessStatusCode)
                    {
                        var lpoEnCategoriaReg = Reswsru.Content.ReadAsAsync<string>().Result;
                        iresultadoreg = int.Parse(lpoEnCategoriaReg);

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
        public async Task<JsonResult> ActualizarDeportivo(int wdeportivoid, string wlocalizacion, string wjefeorganizacion, string wareatotal, int wsedeid)
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
                    HttpResponseMessage Reswsru = await client.GetAsync("api/deportivo/APIActualizarDeportivo?wsdeportivoid=" + wdeportivoid
                                                        + "&wslocalizacion=" + wlocalizacion + "&wsjefeorganizacion=" + wjefeorganizacion 
                                                        + "&wsareatotal=" + wareatotal + "&wssedeid=" + wsedeid);
                    if (Reswsru.IsSuccessStatusCode)
                    {
                        var lpoUpdDeportivo = Reswsru.Content.ReadAsAsync<string>().Result;
                        iresultadoreg = int.Parse(lpoUpdDeportivo);

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
        public async Task<JsonResult> EliminarDeportivo(int wdeportivoid)
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
                    HttpResponseMessage Reswsru = await client.GetAsync("api/deportivo/APIEliminarDeportivo?wdeportivoid=" + wdeportivoid);
                    if (Reswsru.IsSuccessStatusCode)
                    {
                        var lpoDelDeportivo = Reswsru.Content.ReadAsAsync<string>().Result;
                        iresultadoreg = int.Parse(lpoDelDeportivo);

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
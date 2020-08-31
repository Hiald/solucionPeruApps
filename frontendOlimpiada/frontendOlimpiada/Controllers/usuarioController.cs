using OlimpiadaED;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using OlimpiadaUtil;

namespace frontendOlimpiada.Controllers
{
    public class usuarioController : Controller
    {
        public ActionResult login()
        {
            return View();
        }

        public ActionResult nuevo()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> registrarusuario(string wusuario, string wclave)
        {
            try
            {
                var objResultado = new object();

                int iresultadoCuenta = -1;
                //abrimos conexion http para transaccion
                using (var client = new HttpClient())
                {
                    //llamamos a la ruta realizada en el web config y limpiamos si alguna cabecera realizada anteriormente se encuentra ahi
                    client.BaseAddress = new Uri(MvcApplication.wsRouteOlimpiadaBE);
                    client.DefaultRequestHeaders.Clear();
                    //elegimos en que formato se llevara a cabo la transaccion, en este caso json
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //seleccionamos en modo asincrono la ruta del backend con los parametros
                    HttpResponseMessage Reswsvc = await client.GetAsync("api/usuario/APIRegistrarUsuario?wsusuario=" + wusuario + "&wsclave=" + wclave);

                    //validamos que la peticion fue correcta
                    if (Reswsvc.IsSuccessStatusCode)
                    {
                        //si lo fue, obtenemos el formato en json del contenido del resultado y le damos el formato correspondiente
                        var lpoVC = Reswsvc.Content.ReadAsAsync<string>().Result;
                        iresultadoCuenta = int.Parse(lpoVC);

                        //validamos
                        if (iresultadoCuenta == 0)
                        {
                            objResultado = new
                            {
                                iResultado = -2,
                                iResultadoIns = "la cuenta ya está tomada, intenta con otro"
                            };
                            return Json(objResultado);
                        }

                        //error del servidor
                        if (iresultadoCuenta == -1)
                        {
                            objResultado = new
                            {
                                iResultado = -5,
                                iResultadoIns = "Ha ocurrido un error, inténtelo nuevamente"
                            };
                            return Json(objResultado);
                        }
                    }
                }

                ed_usuario oEnUsuario = new ed_usuario();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteOlimpiadaBE);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/usuario/APIListarUsuario?wusuario=" + wusuario + "&wclave=" + wclave + "&valor=o");
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        oEnUsuario = JsonConvert.DeserializeObject<ed_usuario>(rwsapilu);
                    }
                    else
                    {
                        objResultado = new
                        {
                            iResultado = -5,
                            iResultadoIns = "Ha ocurrido un error, inténtelo nuevamente"
                        };
                        return Json(objResultado);
                    }
                }

                //asignamos a variables de sesion los datos obtenidos
                Dictionary<string, string> DVariables = new Dictionary<string, string>();
                DVariables["USUARIOID"] = oEnUsuario.idusuario.ToString();
                DVariables["NOMBRE"] = oEnUsuario.Susuario;
                UtlAuditoria.SetSessionValues(DVariables);

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "Correcto"
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> loginusuario(string wusuario, string wclave)
        {
            try
            {
                var objResultado = new object();
                ed_usuario oEnUsuario = new ed_usuario();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(MvcApplication.wsRouteOlimpiadaBE);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Reslistarusu = await client.GetAsync("api/usuario/APIListarUsuario?wusuario=" + wusuario + "&wclave=" + wclave + "&valor=o");
                    if (Reslistarusu.IsSuccessStatusCode)
                    {
                        var rwsapilu = Reslistarusu.Content.ReadAsAsync<string>().Result;
                        oEnUsuario = JsonConvert.DeserializeObject<ed_usuario>(rwsapilu);

                        if(oEnUsuario == null)
                        {
                            objResultado = new
                            {
                                iResultado = -2,
                                iResultadoIns = "usuario o clave incorrecta"
                            };
                            return Json(objResultado);
                        }
                    }
                    else
                    {
                        objResultado = new
                        {
                            iResultado = -5,
                            iResultadoIns = "Ha ocurrido un error, inténtelo nuevamente"
                        };
                        return Json(objResultado);
                    }
                }

                Dictionary<string, string> DVariables = new Dictionary<string, string>();
                DVariables["USUARIOID"] = oEnUsuario.idusuario.ToString();
                DVariables["NOMBRE"] = oEnUsuario.Susuario;
                UtlAuditoria.SetSessionValues(DVariables);

                objResultado = new
                {
                    iResultado = 1,
                    iResultadoIns = "correcto"
                };
                return Json(objResultado);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }

        }

        [HttpPost]
        public JsonResult cerrarSesion()
        {
            var objResultado = new object();
            try
            {
                bool bResultado = UtlAuditoria.CerrarSession();
                if (bResultado)
                {
                    objResultado = new
                    {
                        iResultado = 1
                    };
                }
                else
                {
                    objResultado = new
                    {
                        iResultado = 2
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(objResultado);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvaluacionServicios.Models;
using EvaluacionServicios.Models.DAL;
using System.Net;
using Kendo.Mvc.UI;
using System.Text.RegularExpressions;

namespace EvaluacionServicios.Controllers
{
    public class ReportesController : Controller
    {
        ReportesDAL objReportes = new ReportesDAL();
        // GET: Reportes
        public ActionResult Index()
        {
            return View(ObtenerListaFormularios());
        }
        public ActionResult Vista()
        {
            
            return View();
        }
        public ActionResult Create()
        {
           return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            string idFormulario = collection["idForm"];

            try
            {
                
                if ((collection["TipoReporte"] == "rango") && ((collection["fechaInicio"] == "0") || (collection["fechaFin"] == "0")))
                {                                        
                    TempData["Error"] = "Error: debe asignar una fecha de inicio y fin, cuando utilice la opción de rango de fechas";
                    ViewBag.IdForm = idFormulario;
                    return View();                    
                }
                else
                {
                    string listaRespuestas;
                    foreach (var key in collection.AllKeys) //extraer la informacion del formulario
                    {

                        listaRespuestas = collection[key];

                    }
                    return RedirectToAction("Vista");
                }
            }
            catch
            {
                TempData["Error"] = "Error de base de datos";
                ViewBag.IdForm = idFormulario;
                return View();
            }

        }

        private Reportes ObtenerListaFormularios()
        {
            Reportes ReportesResult = new Reportes();
            List<Reportes> lstResultados = new List<Reportes>(objReportes.ObtenerFormulariosConResp());
            ReportesResult.lstReporte = lstResultados;
            return ReportesResult;
        }
    }
}

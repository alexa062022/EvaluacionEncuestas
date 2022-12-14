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
        public ActionResult Vista(FormCollection collection)
        {
            ViewBag.IdForm = collection["idForm"];
            ViewBag.sistema = collection["sistema"];            
            string id = collection["idForm"];
            string nombre = collection["sistema"];

            try
            {
                if(collection["TipoReporte"] == "rango") 
                { 
                    DateTime date1 = Convert.ToDateTime(collection["FechaInicio"]);
                    DateTime date2 = Convert.ToDateTime(collection["FechaFin"]);
                    int result = DateTime.Compare(date1, date2);
                    if (result > 0) //verifica si la fecha de inicio es mayor que la fecha fin y da mensaje de error
                    {
                        TempData["Error"] = "Error: La fecha de fin no puede ser menor a la fecha de inicio, por favor vuelva a intentarlo";
                        return RedirectToAction("Index");
                    }
                }
                //llamo a funcion y paso los parametros
                IEnumerable<Reportes> lstReporte = objReportes.CreateReporte(collection["idForm"], collection["TipoReporte"], collection["fechaInicio"], collection["fechaFin"]);
                if (lstReporte.Count() == 0)
                {
                    TempData["Error"] = "Error: No hay datos relacionados a esa encuesta";
                    return RedirectToAction("Index");
                }
                else
                {                        
                   return View(lstReporte);
                }

            }
            catch
            {
                TempData["Error"] = "Error en base de datos";
                return RedirectToAction("Index");
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

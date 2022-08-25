using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvaluacionServicios.Models;
using EvaluacionServicios.Models.DAL;
using System.Net;
using Kendo.Mvc.UI;

namespace EvaluacionServicios.Controllers
{
    public class PregPorFormController : Controller
    {
        FormularioDAL objFormulario = new FormularioDAL();
        //SistemasDAL objsistemas = new SistemasDAL();
        PreguntasDAL objPreguntas = new PreguntasDAL();
        PregPorFormDAL objPregPorForm = new PregPorFormDAL();
        // readonly int cedulaUsuario = EvaluacionServicios.Models.Common.FrontUser.Get().Cedula;
        readonly int cedulaUsuario = 101110111;

        // GET: PregPorForm
        public ActionResult Index()
        {
            ListaCombobox();
            return View();
        }
        private void ListaCombobox()
        {
            IEnumerable<Formulario> lstFormularioResultados = objPregPorForm.ObtenerFormulariosActivos();
            ViewBag.ListaFormulario = lstFormularioResultados;
        }
        public JsonResult ObtenerPreguntasActivas()
        {
            IEnumerable<Preguntas> lstPreguntas = objPreguntas.ObtenerPreguntas("A");
            return Json(lstPreguntas, JsonRequestBehavior.AllowGet);
        }

        // GET: PregPorForm/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                string listaPreg = collection["cargaPreguntas"];
                string idForm = collection["Formulario"];               
                int result = objPregPorForm.IgresarPregPorForm(listaPreg, idForm);
                if (result == 0)
                {
                    TempData["Correcto"] = "La encuesta ha sido creada satisfactoriamente.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Error: No se ingresaron las preguntas, favor intente de nuevo.";
                    return RedirectToAction("Index");
                }                
            }
            catch
            {
                TempData["error"] = "Error: Ocurrio un error en la base de datos.";
                return RedirectToAction("Index");
            }
        }
            
    }
}

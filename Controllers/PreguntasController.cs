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
    public class PreguntasController : Controller
    {
        PreguntasDAL objPreguntas = new PreguntasDAL();
        TipoPreguntaDAL objTipoPreguntas = new TipoPreguntaDAL();
        // readonly int cedulaUsuario = EvaluacionServicios.Models.Common.FrontUser.Get().Cedula;
        //readonly int cedulaUsuario = 101110111;
        public ActionResult Index()
        {
            tipoPregCombobox();
            return View(ObtenerListaPreguntas());
        }
       
        // POST: Preguntas/Create
        [HttpPost]
        public ActionResult Create(string pregunta, int IdTipoPregunta)
        {
            try
            {
                int resultadoInsert;
                resultadoInsert = objPreguntas.IgresarPregunta(pregunta, IdTipoPregunta);
                switch (resultadoInsert)
                {
                    case -1:
                        TempData["error"] = "Error: Ya existe esa pregunta en la base de datos.";
                        break;

                    case -2:
                        TempData["error"] = "Error de Base de datos";
                        break;                   

                    default:
                        TempData["Correcto"] = "Se agregó la pregunta de manera correcta";
                        break;
                }               
            }
            catch
            {
                TempData["error"] = "Error de Base de datos";               
            }
            return RedirectToAction("Index");
        }
        // POST: Preguntas/Edit/5
        [HttpPost]
        public ActionResult Edit(int idPregunta, string preguntaEditar, int Activo)
        {
            try
            {
                int resultadoUpdate;
                resultadoUpdate = objPreguntas.EditarPregunta(idPregunta, preguntaEditar, Activo);

                if (resultadoUpdate == -1)
                {
                    TempData["error"] = "Error: Ya existe esa pregunta en la base de datos.";
                }
                else
                {
                    TempData["Correcto"] = "Se modificó la pregunta de manera correcta";
                }
            }
            catch
            {
                TempData["error"] = "Error de Base de datos";
            }
            return RedirectToAction("Index");
        }

        private Preguntas ObtenerListaPreguntas()
        {
            Preguntas preguntasResult = new Preguntas();
            List<Preguntas> lstResultados = new List<Preguntas>(objPreguntas.ObtenerPreguntas("T"));
            preguntasResult.lstPreguntas = lstResultados;
            return preguntasResult;
        }

        public void tipoPregCombobox()
        {
            IEnumerable<TipoPregunta> lstResultados = objTipoPreguntas.ObtenerTipoPregunta("A");
            ViewBag.ListaTipoPreg = lstResultados;
        }
    }
}

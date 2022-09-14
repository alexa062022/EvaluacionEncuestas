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
    public class EncuestaController : Controller
    {
        EncuestaDAL objEncuesta = new EncuestaDAL();
        // GET: Encuesta
        public ActionResult Index()
        {
            return View();
        }

        // GET: Encuesta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encuesta/Create
        [HttpPost]
        public ActionResult Create(string IdFormulario)
        {            
            try
            {
                if (IdFormulario == string.Empty)
                {
                    TempData["error"] = "Error: El URL ingresado es incorrecto";
                    return RedirectToAction("Index");
                }
                else 
                { 
                    IEnumerable<Encuesta> lstEncuesta = objEncuesta.CreateEncuesta(IdFormulario);
                    if (lstEncuesta.Count() == 0)
                    {
                        TempData["error"] = "Error: El formulario no existe, esta desactivo, o no tiene una encuesta asociada";
                        return RedirectToAction("Index");
                    }
                    else 
                    {                                      
                        foreach (var encuesta in lstEncuesta)
                        {                            
                             ViewBag.NombreSistema = encuesta.NombreSistema;
                             ViewBag.Descripcion = encuesta.Descripcion;
                             ViewBag.IdForm = encuesta.IdFormulario;
                             ViewBag.ColorForm = encuesta.Color;
                             break;                           
                        }
                        return View(lstEncuesta);
                    }
                }
            }
            catch
            {
                TempData["error"] = "Error de base de datos";
                return RedirectToAction("Index");
            }
        }

        // GET: Encuesta/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Encuesta/Edit/5
        [HttpPost]
        public ActionResult GuardarRespuestas(FormCollection collection)
        {
            try
            {
                foreach (var key in collection.AllKeys)
                {
                    var value = collection[key];
                    // etc.
                }

                foreach (var key in collection.Keys)
                {
                    var value = collection[key.ToString()];
                    // etc.
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
    }
}

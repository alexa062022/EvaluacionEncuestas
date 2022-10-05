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
                    TempData["Error"] = "Error: El URL ingresado es incorrecto";
                    return RedirectToAction("Index");
                }
                else 
                { 
                    IEnumerable<Encuesta> lstEncuesta = objEncuesta.CreateEncuesta(IdFormulario);
                    if (lstEncuesta.Count() == 0)
                    {
                        TempData["Error"] = "Error: El formulario no existe, esta desactivo, o no tiene una encuesta asociada";
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
                TempData["Error"] = "Error de base de datos";
                return RedirectToAction("Index");
            }
        }        

        // POST: Encuesta/Guardar
        [HttpPost]
        public ActionResult GuardarRespuestas(FormCollection collection)
        { 
            string idForm = "";
            string listaRespuestas = "";
            try
            {
                foreach (var key in collection.AllKeys) //extraer la informacion del formulario
                {
                    if (key.Contains("pregunta"))
                    {
                        listaRespuestas = listaRespuestas + "[" + collection[key];
                    }
                    else
                    {
                        if ((key.Contains("radio")) || (key.Contains("respuesta")))
                        {
                            listaRespuestas = listaRespuestas + "|" + collection[key];
                        }
                        else
                        {
                            if (key.Contains("justifica"))
                            {
                                listaRespuestas = listaRespuestas + "|" + collection[key] + "]";
                            }
                            else
                            {
                                if (key == "IdFormulario")
                                {
                                    idForm = collection[key];
                                }
                            }

                        }
                    }
                }
                // envio los datos al model
                int resultadoInsert;
                resultadoInsert = objEncuesta.IngresarRespuestas(idForm, listaRespuestas);
                switch (resultadoInsert)
                {
                    case -1:
                        TempData["Error"] = "Error de Base de datos, no se ingresaron los datos";
                        break;
                    
                    default:
                        TempData["Correcto"] = "Se registraron las respuestas correctamente";
                        break;
                }

                //finalizar
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Error"] = "Error de Base de datos";
                return RedirectToAction("Index");
            }
        }        
    }
}

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
                    TempData["Error"] = "Error: El URL es incorrecto";
                    TempData["Error2"] = "Estimado usuario: el url utilizado es incorrecto, favor revisar o contactar con el administrador.";
                    return RedirectToAction("Index");
                }
                else 
                { 
                    IEnumerable<Encuesta> lstEncuesta = objEncuesta.CreateEncuesta(IdFormulario);
                    if (lstEncuesta.Count() == 0)
                    {
                        TempData["Error"] = "Encuesta no habilitada";
                        TempData["Error2"] = "Estimado usuario: La encuesta no existe o ha sido deshabilitada por exceder la fecha límite.";
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
                TempData["Error"] = "Oops.. Se ha presentando un error de base de datos";
                TempData["Error2"] = "Estimado usuario: se produjo un error de base de datos, favor intentar nuevamente o contactar con el administrador.";
                return RedirectToAction("Index");
            }
        }        

        // POST: Encuesta/Guardar
        [HttpPost]
        public ActionResult GuardarRespuestas(FormCollection collection)
        {            
            string idForm = "";
            string listaRespuestas = "";
            bool resultIsMatch = false;            
            
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
                            resultIsMatch = Regex.IsMatch(collection[key], "[A-Za-z0-9]"); //verifica que haya al menos 1 letra o numero en el textarea
                            if (resultIsMatch)
                            {
                                listaRespuestas = listaRespuestas + "|" + collection[key];
                            }
                            else
                            {
                                listaRespuestas = listaRespuestas + "|" + "" ;// agrega espacio, el campo esta vacio tiene solo espacios o simbolos
                            }
                        }
                        else
                        {
                            if (key.Contains("justifica"))
                            {
                                resultIsMatch = Regex.IsMatch(collection[key], "[A-Za-z0-9]"); //verifica que haya al menos 1 letra o numero en el textarea
                                if (resultIsMatch) 
                                {
                                    listaRespuestas = listaRespuestas + "|" + collection[key] + "]";// agrego el valor del campo                                    
                                }
                                else 
                                {
                                    listaRespuestas = listaRespuestas + "|" + "" + "]";// agrega espacio, el campo esta vacio tiene solo espacios o simbolos
                                }                                
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

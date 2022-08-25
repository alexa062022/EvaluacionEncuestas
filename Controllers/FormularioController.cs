using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvaluacionServicios.Models;
using EvaluacionServicios.Models.DAL;
using System.Net;
using Kendo.Mvc.UI;
//using EvaluacionServicios.Tags;

namespace EvaluacionServicios.Controllers
{
    public class FormularioController : Controller
    {
        FormularioDAL objFormulario = new FormularioDAL();
        SistemasDAL objsistemas = new SistemasDAL();
        // readonly int cedulaUsuario = EvaluacionServicios.Models.Common.FrontUser.Get().Cedula;
        readonly int cedulaUsuario = 101110111;

        // GET: Formulario
        public ActionResult Index()
        {
            //mostrar forumularios que existen actualmente
            Formulario formulario = new Formulario();
            List<Formulario> lstFormulario = new List<Formulario>(objFormulario.ObtenerFormularios("T"));
            formulario.lstFormulario = lstFormulario;
            return View(formulario);           
        }        

        // GET: Formulario/Create
        public ActionResult Create()
        {
            ListaCombobox();
            return View();           
        }
        
        // POST: Formulario/Create
        [HttpPost]
        public ActionResult Create(Formulario formulario)
        {            
            int resultadoInsert = 0;
            try
            {                          
                formulario.CedulaUsuario = cedulaUsuario;
                formulario.Activo = 1; // se asigna el valor 1 correspondiente activo a todos los formularios nuevos
               // string color = formulario.Color;
                resultadoInsert = objFormulario.IgresarFormulario(formulario);
                if (resultadoInsert == -1)
                {
                    TempData["error"] = "Error: Existe un formulario activo para el sistema seleccionado";
                    ListaCombobox();
                    return View();
                }
                else
                {
                    TempData["Correcto"] = "Se agregó el formulario de manera correcta";
                    return RedirectToAction("Index");
                }                
            }
            catch
            {
                TempData["error"] = "Se produjo un errror de base de datos";
                ListaCombobox();
                return View();
            }
        }
        public void ListaCombobox()
        {
            IEnumerable<Sistemas> lstSistemasResultados = objsistemas.ObtenerSistemas("A");
            ViewBag.ListaSistemas = lstSistemasResultados;
        }
        // GET: Formulario/Edit/
        public ActionResult Edit( )
        {            
            return View();
        }

        // POST: Formulario/Edit/
        [HttpPost]
        public ActionResult Edit(Formulario formulario)
        {
            int resultadoInsert = 0;
            try
            {
                formulario.CedulaUsuario = cedulaUsuario;

                resultadoInsert = objFormulario.ModificarFormulario(formulario);
                switch (resultadoInsert)
                {
                    case 1:
                        TempData["error"] = "Error: Existe un formulario activo para el sistema seleccionado";
                        break;

                    case 2:
                        TempData["error"] = "Error: El sistema asociado al formulario esta desactivado";
                        break;

                    case 3:
                        TempData["error"] = "Error: Error en base de datos";
                        break;

                    default:
                        TempData["Correcto"] = "Se modificó el sistema de manera correcta";
                        break;
                }               
            }
            catch
            {
                TempData["error"] = "Se produjo un errror en la base de datos";                
            }
            return RedirectToAction("Index");
        }        
    }
}

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
    public class SistemasController : Controller
    {
        SistemasDAL objsistemas = new SistemasDAL();
        // readonly int cedulaUsuario = EvaluacionServicios.Models.Common.FrontUser.Get().Cedula;
        readonly int cedulaUsuario = 101110111;
        // GET: Sistemas
        public ActionResult Index()
        {
            Sistemas sistemasResult = new Sistemas();
            List<Sistemas> lstSistemasResultados = new List<Sistemas>(objsistemas.ObtenerSistemas("T"));
            sistemasResult.lstSistemas = lstSistemasResultados;
            return View(sistemasResult);           
        }
        // GET: Sistemas/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Sistemas/Create
        [HttpPost]
        public ActionResult Create(Sistemas sistema)
        {
            int resultadoInsert;
            sistema.CedulaUsuario = cedulaUsuario;
            sistema.Activo = 1; // por default el sistema nuevo va a estar activo.
            resultadoInsert = objsistemas.IgresarSistema(sistema);
            if(resultadoInsert == -1)
            {
                TempData["error"] = "Error: Existe un sistema con ese nombre actualmente";
                return View();
            }
            else
            {
                TempData["Correcto"] = "Se agregó el sistema de manera correcta";
                return RedirectToAction("Index");
            }           
        }

        // GET: Sistemas/Edit
        public ActionResult Edit()
        {
            return View();
        }

        //// POST: Sistemas/Edit5
        [HttpPost]
        public ActionResult Edit(Sistemas sistema, int estadoModificado)
        {
            int resultadoInsert;
            sistema.CedulaUsuario = cedulaUsuario;
            resultadoInsert = objsistemas.ModificarSistema(sistema, estadoModificado);
            switch (resultadoInsert)
            {
                case 1:
                    TempData["error"] = "Error: Existe un sistema con ese nombre actualmente";
                    break;

                case 2:
                    TempData["error"] = "Error: Existe un formulario activo asociado a este sistema, debe desactivarlo antes de proceder a desactivar el sistema";
                    break;

                case 3:
                    TempData["error"] = "Error: Error en base de datos";
                    break;

                default:
                    TempData["Correcto"] = "Se modificó el sistema de manera correcta";
                    break;
            }            
            return RedirectToAction("Index");
        }         
    }
}

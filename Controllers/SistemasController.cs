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
            List<Sistemas> lstSistemasResultados = new List<Sistemas>(objsistemas.ObtenerSistemas());
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
        public ActionResult Create(FormCollection collection)
        {
            Sistemas sistema;
            int resultadoInsert;

            sistema = AsignarDatos(collection);

            resultadoInsert = objsistemas.IgresarSistema(sistema);
            if(resultadoInsert == -1)
            {
                TempData["error"] = "No se pudo ingresar el sistema, por favor verifique que el sistema no exista actualmente";
                return View();
            }
            else
            {
                TempData["Correcto"] = "Se agregó el registro de manera correcta";
                return RedirectToAction("Index");
            }
           
        }

        // GET: Sistemas/Edit
        public ActionResult Edit(string NombreSistema, string InternoExternoMostrar, string ActivoMostrar, string idSistema)
        {
            return View();
        }

        //// POST: Sistemas/Edit5
        //[HttpPost]
        public ActionResult Edit5(FormCollection collection)
        {
            int resultadoInsert;
            Sistemas sistema;
       
            sistema = AsignarDatos(collection);

            resultadoInsert = objsistemas.ModificarSistema(sistema);
            if (resultadoInsert == -1)
            {
                TempData["error"] = "No se pudo actualizar los datos, por favor intente nuevamente";                
            }
            else
            {
                TempData["Correcto"] = "Se modificó el registro de manera correcta";                
            }
            return RedirectToAction("Index");
        }
        
        public Sistemas AsignarDatos(FormCollection collection)
        {
            Sistemas sistema = new Sistemas();           

            sistema.NombreSistema = collection["NombreSistema"];
            if (collection.AllKeys.Contains("idSistema"))
            {
                sistema.idSistema = collection["idSistema"];
            }

            if (collection["tipoSistema"] == "externo") //si es externo agrega 0 en la base de datos
            {
                sistema.InternoExterno = 0;
            }
            else
            {
                sistema.InternoExterno = 1; //si es interno agrega 1 en la base de datos
            }

            if (collection.AllKeys.Contains("activo"))
            {
                sistema.Activo = 1; //si esta activo agrega 1 en la base de datos
            }
            else
            {
                sistema.Activo = 0; //si esta inactivo agrega 0 en la base de datos
            }
            sistema.CedulaUsuario = cedulaUsuario;
            return (sistema);
        }
    }
}

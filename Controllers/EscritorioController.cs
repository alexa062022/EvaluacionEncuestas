using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EvaluacionServicios.Tags;
using Helper;
using EvaluacionServicios.Models.Common;
using EvaluacionServicios.Models;
using EvaluacionServicios.Models.DAL;
//using System.Net;

namespace EvaluacionServicios.Controllers
{
    [AutenticadoAttribute]
    public class EscritorioController : Controller
    {

        int cedulaUsuario = EvaluacionServicios.Models.Common.FrontUser.Get().Cedula;
        // GET: Escritorio
        public ActionResult Index()
        {
            Usuario usuario = new Usuario();
            int idPerfil = 0;

            if (Session["Cedula"] == null)
            {
                usuario = FrontUser.Get();
                idPerfil = usuario.Rol_id;
                Session.Add("idPerfil", idPerfil);
                Session.Add("Cedula", usuario.Cedula);
            }
            else
            {
                idPerfil = (int)Session["idPerfil"];
                ViewBag.idPerfil = idPerfil;                
            }
            return View();
        }

        public ActionResult Salir()
        {
            SessionHelper.DestroyUserSession();
            return Redirect("~/Login");
        }

    }
}

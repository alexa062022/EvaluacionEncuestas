using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices;
using System.Web.Configuration;
using EvaluacionServicios.Models;
using EvaluacionServicios.Models.DAL;
using EvaluacionServicios.Tags;

namespace EvaluacionServicios.Controllers
{
    [HandleError]
    [NoLoginAttribute]
    public class LoginController : Controller
    {
        string _path;
        string _filterAttribute;
        private Usuario um = new Usuario();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vista()
        {
            return View();
        }

        public ActionResult Salir()
        {
            Helper.SessionHelper.DestroyUserSession();
            return View("Index");
        }

        public string formatearPath(string strPath, bool boolDominios)
        {
            string strFormattedPath = "";
            string[] aTipoGrupo, aTemp = new string[2];
            int nCont;

            try
            {
                // Se obtiene los distintos grupos en un arreglo, vienen de la forma tipo=NombreGrupo

                aTipoGrupo = Microsoft.VisualBasic.Strings.Split(strPath, ",");
                for (nCont = 0; nCont <= aTipoGrupo.Length - 1; nCont++)
                {
                    aTemp = Microsoft.VisualBasic.Strings.Split(aTipoGrupo[nCont], "=");
                    if (!((!boolDominios) & aTemp[0] == "DC"))
                        strFormattedPath = aTemp[1] + "|" + strFormattedPath;
                }

                if (strFormattedPath.Length > 0)
                    strFormattedPath = strFormattedPath.Substring(0, strFormattedPath.Length - 1);

                return strFormattedPath;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public string RealizarAutenticacion(string strLogin, string strPassword)
        {
            DirectoryEntry dirEntEntrada;
            SearchResult srBusqueda;
            string strFormatDir;
            string DomainAndUsername;
            string[] strResultado = new string[4];
            string strUsuario;
            string bandera = "False";

            try
            {
                DomainAndUsername = System.Web.Configuration.WebConfigurationManager.AppSettings["Dominio"].ToString() + @"\" + strLogin;
                dirEntEntrada = new DirectoryEntry("LDAP://" + System.Web.Configuration.WebConfigurationManager.AppSettings["path"].ToString(), DomainAndUsername, strPassword);
                DirectorySearcher search = new DirectorySearcher(dirEntEntrada);

                search.Filter = "(SAMAccountName=" + strLogin + ")";
                // search.PropertiesToLoad.Add("Mail")
                srBusqueda = search.FindOne();

                _path = srBusqueda.Path;
                _filterAttribute = System.Convert.ToString(srBusqueda.Properties["cn"][0]);
                strFormatDir = formatearPath(_path, false);
                strUsuario = _filterAttribute;

                if ((strUsuario == null))
                    bandera = "False";
                else
                    bandera = strUsuario;
            }
            catch (Exception ex)
            {
                strResultado[0] = ("");
            }

            return bandera;
        }


        public ActionResult Autenticar(Usuario model)
        {

            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                this.um.Correo = model.Correo;
                this.um.Password = model.Password;
                string usuario = RealizarAutenticacion(model.Correo, model.Password);

              if (usuario != "False")
                {
                    Session["Cedula"] = null;
                    rm = um.Autenticarse();

                    if (rm.response)
                    {
                        rm.href = Url.Content("~/Escritorio");
                    }
                }
            }
            else
            {
                rm.SetResponse(false, "Debe llenar los campos para poder autenticarse.");
            }

            if (rm.response)
            {
                return RedirectToAction("Index", "Escritorio");
            }
            else
            {
                TempData["Success"] = "Acceso Denegado";
                return View("Index");
            }
        }
    }
}

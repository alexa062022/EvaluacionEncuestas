namespace EvaluacionServicios.Models
{
    using Helper;
    using EvaluacionServicios.Models.DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using System.Linq;

    [Table("Usuario")]
    public partial class Usuario

    {


        readonly private SeguridadDAL objSeguridad = new SeguridadDAL();

        public int Cedula { get; set; }

        public string Nombre { get; set; }
        public string CodUsuario { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }

        public virtual Rol Rol { get; set; }
        public int Rol_id { get; set; }
        public bool EsAreaAgraria { get; set; }

        public bool EsAreaPenal { get; set; }

        public bool EsAreaEtica { get; set; }

        public bool EsJefe { get; set; }

        public int DescripcionRol { get; set; }



        public ResponseModel Autenticarse()
        {
            var rm = new ResponseModel();

            try
            {
                Usuario oUsuario = new Usuario
                {
                    CodUsuario = this.Correo
                };


                var usuario = objSeguridad.ObtenerPermisosUsuario(oUsuario);
                if (usuario.CodUsuario != null && usuario.Cedula != -1)
                {
                    SessionHelper.AddUserToSession(usuario.CodUsuario.ToString());
                    rm.SetResponse(true);
                }
                else
                {

                    if (usuario.Cedula != -1)
                    { rm.SetResponse(false, "Acceso denegado al sistema"); }
                    else
                    {
                        rm.SetResponse(false, "Debe comunicarse con personal de capacitacion para revision de su numero de cedula.");
                    }
                }

            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }

        public Usuario Obtener(string id)
        {
            var usuario = new Usuario();

            try
            {
                usuario = objSeguridad.ObtenerDetallePermisoUsuario(id);
            }
            catch (Exception e)
            {
                throw;
            }

            return usuario;
        }

    }

}
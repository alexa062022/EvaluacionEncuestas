﻿using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluacionServicios.Models.Common
{
    public class FrontUser
    {
        //public static bool TienePermiso(RolesPermisos valor)
        //{
        //    var usuario = FrontUser.Get();
        //    return !usuario.Rol.Permiso.Where(x => x.PermisoID == valor)
        //                       .Any();
        //}

        public static Usuario Get()
        {
            return new Usuario().Obtener(SessionHelper.GetUser());
        }
    }
}
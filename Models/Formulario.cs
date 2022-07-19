using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EvaluacionServicios.Models
{
    public class Formulario
    {
        public int IdFormulario { get; set; }

        public int IdSistema { get; set; }
        
        [Display(Name = "Nombre del Sistema")]
        public string NombreSistema { get; set; }

        [Display(Name = "Descripción del Sistema")] 
        public string Descripcion { get; set; }

        [Display(Name = "Estado")]
        public int Activo { get; set; } //1 = Activo, 0 = Inactivo

        [Display(Name = "Estado")]
        public string ActivoMostrar { get; set; } //1 = Activo, 0 = Inactivo

        [Display(Name = "Fecha Ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Display(Name = "Usuario")]
        public int CedulaUsuario { get; set; }
                
        public virtual ICollection<Formulario> lstFormulario { get; set; }
    }
}
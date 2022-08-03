using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionServicios.Models
{
    public class TipoPregunta
    {
        public int IdTipoPregunta { get; set; }

        //[Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Nombre del Sistema")]
        public string DescTipoPregunta { get; set; }        

        [Display(Name = "Estado")]
        public int Activo { get; set; } //1 = Activo, 0 = Inactivo

        [Display(Name = "Fecha Ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Display(Name = "Usuario")]
        public int CedulaUsuario { get; set; }
        
        //muestra el estado del sistema en un string para ser presentado en pantalla
        [Display(Name = "Estado")]
        public string ActivoMostrar { get; set; }

        public virtual ICollection<TipoPregunta> lstTipoPregunta { get; set; }
    }
}
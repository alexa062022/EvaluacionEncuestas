using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EvaluacionServicios.Models
{
    public class Encuesta
    {
        [Display(Name = "Formulario ID")]
        public int IdFormulario { get; set; }

        public int IdSistema { get; set; }

        [Display(Name = "Sistema")]
        public string NombreSistema { get; set; }

        [Display(Name = "Descripción del Formulario")]
        public string Descripcion { get; set; }

        [Display(Name = "Estado")]
        public int Activo { get; set; } //1 = Activo, 0 = Inactivo 

        public string Color { get; set; }

        public int IdPregunta { get; set; }

        public int IdTipoPregunta{ get; set; }

        [Display(Name = "Pregunta")]
        public string DescPregunta { get; set; }

        
        public virtual ICollection<Encuesta> lstEncuesta { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionServicios.Models
{
    public class Reportes
    {
        [Display(Name = "Formulario ID")]
        public int IdFormulario { get; set; }

        public int IdSistema { get; set; }

        [Display(Name = "Sistema")]
        public string NombreSistema { get; set; }       

        public DateTime FechaFormulario { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int IdPregunta { get; set; }

        public int IdTipoPregunta { get; set; }

        [Display(Name = "Pregunta")]
        public string DescPregunta { get; set; }

        public int Justificacion { get; set; } //1 = agregar justificacion, 0 = sin justificacion         

        public virtual ICollection<Reportes> lstReporte { get; set; }
    }
}
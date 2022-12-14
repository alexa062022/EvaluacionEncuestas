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

        public int opcion1 { get; set; }
        public int opcion2 { get; set; }
        public int opcion3 { get; set; }
        public int opcion4 { get; set; }
        public int opcion5 { get; set; }
        public string valorRespuesta { get; set; }
        public string Justificacion { get; set; }

        public virtual ICollection<Reportes> lstReporte { get; set; }
    }
}
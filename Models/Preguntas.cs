using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionServicios.Models
{
    public class Preguntas
    {
        public int IdPregunta { get; set; }

        public int IdTipoPregunta { get; set; }

        [Display(Name = "Pregunta")]
        public string DescPregunta { get; set; }//1 = Interno, 0 = Externo

        [Display(Name = "Estado")]
        public int Activo { get; set; } //1 = Activo, 0 = Inactivo
        
        [Display(Name = "Estado")]
        public string ActivoMostrar { get; set; }

        [Display(Name = "Tipo de Pregunta")]
        public string TipoPregunta { get; set; }

        public int Justificar { get; set; }

        public virtual ICollection<Preguntas> lstPreguntas { get; set; }
    }
}
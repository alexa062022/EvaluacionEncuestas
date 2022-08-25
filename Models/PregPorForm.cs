using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EvaluacionServicios.Models
{
    public class PregPorForm
    {
        public int Id_Formulario { get; set; }

        public int Id_Pregunta { get; set; }

        public virtual ICollection<PregPorForm> lstPregForm { get; set; }
    }
}
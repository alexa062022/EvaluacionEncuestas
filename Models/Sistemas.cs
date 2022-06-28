using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EvaluacionServicios.Models
{
    public class Sistemas    
    {        
        public string idSistema { get; set; }

        //[Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Nombre del Sistema")]
        public string NombreSistema { get; set; }

        [Display(Name = "Tipo de sistema")] 
        public int InternoExterno { get; set; }//1 = Interno, 0 = Externo

        [Display(Name = "Estado")]
        public int Activo { get; set; } //1 = Activo, 0 = Inactivo

        [Display(Name = "Fecha Ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Display(Name = "Usuario")]
        public int CedulaUsuario { get; set; }

        //muestra el tipo de sistema en un string para ser presentado en pantalla
        [Display(Name = "Tipo de sistema")]
        public string InternoExternoMostrar { get; set; }
        //muestra el estado del sistema en un string para ser presentado en pantalla
        [Display(Name = "Estado")]
        public string ActivoMostrar { get; set; }

        public virtual ICollection<Sistemas> lstSistemas { get; set; }
    
    }
}
namespace EvaluacionServicios.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    public class Rol
    {
        public Rol()
        {
            Usuario = new List<Usuario>();
            Permiso = new List<Permiso>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Permiso> Permiso { get; set; }
    }
}
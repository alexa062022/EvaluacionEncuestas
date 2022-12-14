using EvaluacionServicios.Models.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvaluacionServicios.Models
{
    public class Permiso
    {
        public Permiso()
        {
            Rol = new List<Rol>();
        }

        public RolesPermisos PermisoID { get; set; }

        [Required]
        [StringLength(20)]
        public string Modulo { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        public virtual ICollection<Rol> Rol { get; set; }
    }
}
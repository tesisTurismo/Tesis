using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis.Comun.Modelo
{
    public class Local
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idLocal { get; set; }
        public string nombreLocal { get; set; }
        public string pagWeb { get; set; }
        public string descripcion { get; set; }
        public virtual ICollection<Sucursal> Sucursales { get; set; }
    }
}

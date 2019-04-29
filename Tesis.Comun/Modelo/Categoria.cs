using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis.Comun.Modelo
{
   public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCategoria { get; set; }
        public string foto { get; set; }
        public string nombreCat { get; set; }
        public virtual ICollection<Local> locales { get; set; }
        public virtual ICollection<LugarHistorico> LugarHistoricos  { get; set; }

    }
}

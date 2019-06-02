using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis.Comun.Modelo
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCategoria { get; set; }
 
        public string nombreCat { get; set; }

        public string fotoCategoria { get; set; }
        [JsonIgnore]
       public virtual ICollection<Local> locales { get; set; }
        [JsonIgnore]
        public virtual ICollection<LugarHistorico> LugarHistoricos  { get; set; }
        public string fotoApp
        {
            get
            {
                if (string.IsNullOrEmpty(this.fotoCategoria))
                {
                    return null;
                }

                return $"https://tesisweb.azurewebsites.net/{this.fotoCategoria.Substring(1)}";

            }
        }
    }
}

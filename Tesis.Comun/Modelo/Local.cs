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
    public class Local
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idLocal { get; set; }
        public string foto { get; set; }
        public string nombreLocal { get; set; }
        public string pagWeb { get; set; }
        public string descripcion { get; set; }
        public int idCategoria { get; set; }
        [JsonIgnore]
        public virtual ICollection<Sucursal> Sucursales { get; set; }
        [JsonIgnore]
        public virtual Categoria categoriafk { get; set; }

        public string fotoApp
        {
            get
            {
                if (string.IsNullOrEmpty(this.foto))
                {
                    return "noLocal";
                }

                return $"https://tesisweb.azurewebsites.net/{this.foto.Substring(1)}";

            }
        }
        public override string ToString()
        {
            return this.nombreLocal;
        }
    }
}

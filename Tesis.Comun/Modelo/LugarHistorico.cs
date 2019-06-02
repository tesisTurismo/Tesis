using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis.Comun.Modelo
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class LugarHistorico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idLugarHistorico { get; set; }
        public string foto { get; set; }
        public string nombreLugarH { get; set; }
        public string descripcionLugarH { get; set; }
        public string calle { get; set; }
        public int numero { get; set; }
        public string telefonoLugarH { get; set; }
        public float latitudLugarH { get; set; }
        public float longitudLugarH { get; set; }
        public int idCategoria { get; set; }
        [JsonIgnore]
        public virtual Categoria categoriafk { get; set; }

        public string fotoApp
        {
            get
            {
                if (string.IsNullOrEmpty(this.foto))
                {
                    return null;
                }

                return $"https://tesisweb.azurewebsites.net/{this.foto.Substring(1)}";

            }
        }
    }

}

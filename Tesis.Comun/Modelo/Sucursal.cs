using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis.Comun.Modelo
{
    using Newtonsoft.Json;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Sucursal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idSucursal { get; set; }
        public string foto { get; set; }
        public string calle { get; set; }
        public int numero { get; set; }
        public string calleIntersección { get; set; }
        public string telefono { get; set; }
        public float latitud { get; set; }
        public float longitud { get; set; }
        public int idLocal { get; set; }
        [JsonIgnore]
        public virtual Local localfk { get; set; }

        public override string ToString()
        {
            return this.calle;
        }

       
    }
}

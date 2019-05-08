using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis.Comun.Modelo
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUsuario { get; set; }
        
        public string nombreU { get; set; }
        public string apellidoU { get; set; }
        public string PaisOrigen { get; set; }
        public string CiudadOrigen { get; set; }
        public string Nacionalidad { get; set; }
        public string email { get; set; }



    }
}

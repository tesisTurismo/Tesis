using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesis.Comun.Modelo
{
    public class Respuesta
    {
        public bool respExitosa { get; set; }
        public string mensaje { get; set; }
        public object resultado { get; set; }
    }
}

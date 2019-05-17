using System;
using System.Collections.Generic;
using System.Text;

namespace Tesis.VistaModelo
{
   public class VistaPrincipal
    {
        public NegociosVModelo Negocios { get; set; }

        public VistaPrincipal()
        {
            this.Negocios = new NegociosVModelo();
        }
    }
}

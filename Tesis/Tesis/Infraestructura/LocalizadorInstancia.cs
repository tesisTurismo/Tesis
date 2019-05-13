using System;
using System.Collections.Generic;
using System.Text;

namespace Tesis.Infraestructura
{
    using VistaModelo;
    public class LocalizadorInstancia
    {
        public VistaPrincipal Main { get; set;}

             public LocalizadorInstancia()
            {
            this.Main = new VistaPrincipal();
            }
    }
}

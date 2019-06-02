using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Tesis.Vistas;
using Xamarin.Forms;

namespace Tesis.VistaModelo
{
   public class VistaPrincipal
    {
        public NegociosVModelo Negocios { get; set; }
        public SucursalVModelo Sucursales { get; set; }
        public DetalleSucursalVModelo DetalleSucursales { get; set; }
        public LugaresHistoricosVModelo LugaresHistoricos { get; set; }

        public CategoriasVModel Categorias { get; set; }

        public VistaPrincipal()
        {
            instancia = this;
        }

        private static VistaPrincipal instancia;

        public static VistaPrincipal GetInstancia()
        {
            if (instancia == null)
            {
                return new VistaPrincipal();
            }
            return instancia;
        }


    }
}

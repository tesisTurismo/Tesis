using System;
using System.Collections.Generic;
using System.Text;
using Tesis.Comun.Modelo;
using Tesis.Servicios;

namespace Tesis.VistaModelo
{
    public class LurgaresHistoricosItemVModelo:LugarHistorico
    {
        private ApiServicio apiservicio;
        public LurgaresHistoricosItemVModelo()
        {
            this.apiservicio = new ApiServicio();
        }


    }
}

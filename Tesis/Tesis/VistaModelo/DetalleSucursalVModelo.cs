using System;
using System.Collections.Generic;
using System.Text;
using Tesis.Comun.Modelo;
using Tesis.Servicios;

namespace Tesis.VistaModelo
{
    public class DetalleSucursalVModelo:BaseVModelo
    {
        private ApiServicio apiServicios;
        private Sucursal sucursal;
        private string calleSucursal;
        private int numeroSucursal;
        private string telefonoSucursal;
        private string calleIntersección;


        public Sucursal Sucursal
        {
            get { return this.sucursal; }
            set { this.SetValue(ref this.sucursal,value); }
        }
        public string CalleSucursal
        {
            get { return this.calleSucursal; }
            set { this.SetValue(ref this.calleSucursal, value); }
        }

        public string TelefonoSucursal
        {
            get { return this.telefonoSucursal; }
            set { this.SetValue(ref this.telefonoSucursal, value); }
        }

        public int NumeroSucursal
        {
            get { return this.numeroSucursal; }
            set { this.SetValue(ref this.numeroSucursal, value); }
        }

        public string CalleIntersección
        {
            get { return this.calleIntersección; }
            set { this.SetValue(ref this.calleIntersección, value); }
        }

        public DetalleSucursalVModelo(Sucursal sucursal)
        {
            this.Sucursal = sucursal;
            this.apiServicios = new ApiServicio();
            this.CalleSucursal = sucursal.calle;
            this.NumeroSucursal = sucursal.numero;
            this.CalleIntersección = sucursal.calleIntersección;
            this.TelefonoSucursal = sucursal.telefono;
        }
    }
}

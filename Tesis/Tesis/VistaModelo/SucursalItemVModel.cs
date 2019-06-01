using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Tesis.Comun.Modelo;
using Tesis.Servicios;
using Tesis.Vistas;
using Xamarin.Forms;

namespace Tesis.VistaModelo
{
    public class SucursalItemVModel:Sucursal
    {
        private ApiServicio apiservicio;
        private Sucursal sucursal { get; set; }
        public SucursalItemVModel()
        {
            this.apiservicio = new ApiServicio();
        }


        public ICommand SucursalesDetalleCommand
        {
            get
            {
                return new RelayCommand(SucursalesDetalle);
            }
        }

        private async void SucursalesDetalle()
        {
            VistaPrincipal.GetInstancia().DetalleSucursales = new DetalleSucursalVModelo(this);
            await Application.Current.MainPage.Navigation.PushAsync(new DetalleSucursalesPage());
        }


    }
}

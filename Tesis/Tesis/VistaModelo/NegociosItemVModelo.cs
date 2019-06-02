
using System;
using System.Collections.Generic;
using System.Text;


namespace Tesis.VistaModelo
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Tesis.Comun.Modelo;
    using Tesis.Servicios;
    using Tesis.Vistas;
    using Xamarin.Forms;
    public class NegociosItemVModelo:Local
    {
        private ApiServicio apiservicio;
        private Local local { get; set; }
       
        public NegociosItemVModelo()
        {
            this.apiservicio = new ApiServicio();
        }
        /*
        public Local Local
        {
            get { return local; }
            set
            {
               
                    ItemSeleccionado();
                
            }
        }

        private async void ItemSeleccionado()
        {
            VistaPrincipal.GetInstancia().Sucursales = new SucursalVModelo(this);
            await Application.Current.MainPage.Navigation.PushAsync(new SucursalesPage());
        }*/
        public ICommand SucursalesCommand
        {
            get
            {
                return new RelayCommand(IrSucursales);
            }
        }

        private async void IrSucursales()
        {
            
                

                VistaPrincipal.GetInstancia().Sucursales = new SucursalVModelo(this);
                await Application.Current.MainPage.Navigation.PushAsync(new SucursalesPage());

           

        }
    }
}

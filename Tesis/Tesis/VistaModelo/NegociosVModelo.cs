using System;
using System.Collections.Generic;

using System.Text;

namespace Tesis.VistaModelo
{
    using Tesis.Comun.Modelo;
    using System.Collections.ObjectModel;
    using Tesis.Servicios;
    using Xamarin.Forms;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public class NegociosVModelo:BaseVModelo
    {
        private ApiServicio apiServicios;

        private ObservableCollection<Local> locales;

        private bool isRefreshing;

        public ObservableCollection<Local> Locales {
            get { return this.locales; }
            set { this.SetValue(ref this.locales, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }

        }

        public NegociosVModelo()
        {
            this.apiServicios = new ApiServicio();
            this.LoadLocales();
        }

        private async void LoadLocales()
        {
            this.IsRefreshing = true;
            var conexion = await this.apiServicios.ValidacionInternet();
            if (!conexion.respExitosa)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor conectarse a internet.", "Aceptar");
                return;
            }
            var url = Application.Current.Resources["UrlAPILocal"].ToString();
            var prefijo = Application.Current.Resources["UrlPrefixLocal"].ToString();
            var controlador = Application.Current.Resources["UrlRestauranteControllerLocal"].ToString();
            var response = await this.apiServicios.mostrarLista<Local>(url, prefijo,controlador);

            if (!response.respExitosa)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.mensaje, "Aceptar");
                return;
            }

            var lista = (List<Local>)response.resultado;
            this.Locales = new ObservableCollection<Local>(lista);
           // this.RefreshList();
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLocales);
            }
        }


    }
}

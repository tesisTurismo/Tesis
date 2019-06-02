using System;
using System.Collections.Generic;
using System.Text;
using Tesis.Comun.Modelo;
using Tesis.Vistas;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Tesis.Servicios;

namespace Tesis.VistaModelo
{
    public class CategoriaItemVModelo : Categoria
    {
        private ApiServicio apiservicio;
        private Categoria cat;
        public CategoriaItemVModelo()
        {
            this.apiservicio = new ApiServicio();
        }
        public ICommand CategoriaSeleccionadaCommand
        {
            get
            {
                return new RelayCommand(CategoriaSeleccionada);
            }
        }

        private async void CategoriaSeleccionada()
        {
            NegociosVModelo neg= new NegociosVModelo(this);
            if (neg.IdCat == 10)
            {
                VistaPrincipal.GetInstancia().LugaresHistoricos = new LugaresHistoricosVModelo();
                await App.Current.MainPage.Navigation.PushAsync(new LugaresHistoricosPage());
            }
            else
            {
                VistaPrincipal.GetInstancia().Negocios = new NegociosVModelo(this);
                await App.Current.MainPage.Navigation.PushAsync(new NegociosPage());
            }
        }

        /*
        public ICommand lugarhistoricoCommand
        {
            get
            {
                return new RelayCommand(lugarhistorico);
            }
        }


        private async void lugarhistorico()
        {
            VistaPrincipal.GetInstancia().LugaresHistoricos = new LugaresHistoricosVModelo();
            await App.Current.MainPage.Navigation.PushAsync(new LugaresHistoricosPage());
        }*/
    }
}

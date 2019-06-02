using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tesis.Comun.Modelo;
using Tesis.Servicios;
using Xamarin.Forms;

namespace Tesis.VistaModelo
{
   public class LugaresHistoricosVModelo:BaseVModelo
    {
        //atributos 
        ApiServicio apiServicio;
        private ObservableCollection<LurgaresHistoricosItemVModelo> lugaresHistoricos;
        private bool isRefreshing;
        private string filtro;
        private static LugaresHistoricosVModelo instancia;

        //propiedades
        public List<LugarHistorico> listaLugares { get; set; }
        public ObservableCollection<LurgaresHistoricosItemVModelo> LugaresHistoricos
        {
            get { return this.lugaresHistoricos; }
            set { this.SetValue(ref this.lugaresHistoricos, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }

        }

        public string Filtro
        {
            get { return this.filtro; }
            set
            {
                this.filtro = value;
               this.RefreshList();
            }
        }

        //Constructor
        public LugaresHistoricosVModelo()
        {
            instancia = this;
            
            this.apiServicio = new ApiServicio();

            this.LoadLugaresHistoricos();
        }

        private async void LoadLugaresHistoricos()
        {
            this.IsRefreshing = true;
            var conexion = await this.apiServicio.ValidacionInternet();
            if (!conexion.respExitosa)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor conectarse a internet.", "Aceptar");
                return;
            }

            var p = await this.LoadLugaresHistoricosFromAPI();
            if (p)
            {
               this.RefreshList();

            }

           
            this.IsRefreshing = false;
        }
        private async Task<bool> LoadLugaresHistoricosFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefijo = Application.Current.Resources["UrlPrefix"].ToString();
            var controlador = Application.Current.Resources["UrlControllerLugaresHistoricos"].ToString();
            var response = await this.apiServicio.mostrarLista<LugarHistorico>(url, prefijo, controlador);

            if (!response.respExitosa)
            {
                return false;

            }

            // var lista = (List<Sucursal>)response.resultado;
            this.listaLugares = (List<LugarHistorico>)response.resultado;
            //this.Sucursales = new ObservableCollection<Sucursal>(lista);
            return true;
        }


        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {
                var listalugares = this.listaLugares.Select(l => new LurgaresHistoricosItemVModelo
                {
                    idLugarHistorico = l.idLugarHistorico,
                    foto = l.foto,
                    nombreLugarH = l.nombreLugarH,
                    descripcionLugarH = l.descripcionLugarH,
                    calle = l.calle,
                    numero = l.numero,
                    telefonoLugarH = l.telefonoLugarH,
                    latitudLugarH = l.latitudLugarH,
                    longitudLugarH = l.longitudLugarH,
                    idCategoria = l.idCategoria,




                });
                this.LugaresHistoricos = new ObservableCollection<LurgaresHistoricosItemVModelo>(
                    listalugares.OrderBy(l => l.nombreLugarH));
            }
            else
            {

                var listalugares = this.listaLugares.Select(l => new LurgaresHistoricosItemVModelo
                {
                    idLugarHistorico = l.idLugarHistorico,
                    foto = l.foto,
                    nombreLugarH = l.nombreLugarH,
                    descripcionLugarH = l.descripcionLugarH,
                    calle = l.calle,
                    numero = l.numero,
                    telefonoLugarH = l.telefonoLugarH,
                    latitudLugarH = l.latitudLugarH,
                    longitudLugarH = l.longitudLugarH,
                    idCategoria = l.idCategoria,



                }).Where(l => l.nombreLugarH.ToLower().Contains(this.filtro.ToLower())).ToList();
                this.LugaresHistoricos = new ObservableCollection<LurgaresHistoricosItemVModelo>(
                    listalugares.OrderBy(l => l.nombreLugarH));



            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLugaresHistoricos);
            }
        }
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }




    }
}

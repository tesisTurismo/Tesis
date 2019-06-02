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
    using Tesis.Vistas;
    using System.Linq;

    public class NegociosVModelo:BaseVModelo
    {
        //atributos
        private ApiServicio apiServicios;

        private ObservableCollection<NegociosItemVModelo> locales;

        private bool isRefreshing;
        private string filtro;
        public Categoria Categoria { get; set; }
        private int idCat;
        //propiedades

        public int IdCat
        {
            get { return this.idCat; }
            set { this.SetValue(ref this.idCat, value); }
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
        public ObservableCollection<NegociosItemVModelo> Locales {
            get { return this.locales; }
            set { this.SetValue(ref this.locales, value); }
        }

        //lista de locales
        public List<Local>MyLocals{get; set;}

        private static NegociosVModelo instancia;
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }

        }

        public NegociosVModelo(Categoria categoria)
        {
            
            instancia = this;
            this.IdCat = categoria.idCategoria;
            this.Categoria = categoria;
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
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefijo = Application.Current.Resources["UrlPrefix"].ToString();
            var controlador = Application.Current.Resources["UrlControllerLocal"].ToString();
            var response = await this.apiServicios.mostrarLista<Local>(url, prefijo,controlador,this.Categoria.idCategoria);

            if (!response.respExitosa)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.mensaje, "Aceptar");
                return;
            }

          // var lista = (List<Local>)response.resultado;
          this.MyLocals= (List<Local>)response.resultado;
            //this.Locales = new ObservableCollection<Local>(lista);
            this.RefreshList();
            this.IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLocales);
            }
        }

        //Metodo para refrescar la lista y filtrar
        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {
                var mylistaNVM = this.MyLocals.Select(p => new NegociosItemVModelo
                {
                    idLocal=p.idLocal,
                    foto = p.foto,
                    nombreLocal=p.nombreLocal,
                    pagWeb=p.pagWeb,
                    descripcion=p.descripcion,
                    idCategoria=p.idCategoria,




                });
                this.Locales = new ObservableCollection<NegociosItemVModelo>(
                    mylistaNVM.OrderBy(p => p.nombreLocal));
            }
            else
            {

                var mylistaNVM = this.MyLocals.Select(p => new NegociosItemVModelo
                {
                    idLocal = p.idLocal,
                    foto = p.foto,
                    nombreLocal = p.nombreLocal,
                    pagWeb = p.pagWeb,
                    descripcion = p.descripcion,
                    idCategoria = p.idCategoria,




                }).Where(p => p.nombreLocal.ToLower().Contains(this.filtro.ToLower())).ToList();
                this.Locales = new ObservableCollection<NegociosItemVModelo>(
                    mylistaNVM.OrderBy(p => p.nombreLocal));



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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Tesis.Comun.Modelo;
using Tesis.Servicios;
using Xamarin.Forms;
using System.Windows.Input;
using System.Linq;
using GalaSoft.MvvmLight.Command;

namespace Tesis.VistaModelo
{
    public class CategoriasVModel : BaseVModelo

    {
        private string filtro;

        private ApiServicio apiServicios;

        private bool isRefreshing;


        private ObservableCollection<CategoriaItemVModelo> categorias;

        public List<Categoria> listaCategoria { get; set; }


        public ObservableCollection<CategoriaItemVModelo> Categorias
        {
            get { return this.categorias; }
            set { this.SetValue(ref this.categorias, value); }
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
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        public CategoriasVModel()
        {
            instancia = this;
            this.apiServicios = new ApiServicio();
            this.LoadCategorias();
        }



        private static CategoriasVModel instancia;



        private async void LoadCategorias()
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
            var controlador = Application.Current.Resources["UrlControllerCategorias"].ToString();
            var response = await this.apiServicios.mostrarLista<Categoria>(url, prefijo, controlador);

            if (!response.respExitosa)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.mensaje, "Aceptar");
                return;
            }

            this.listaCategoria = (List<Categoria>)response.resultado;
            //this.Categorias = new ObservableCollection<Categoria>(lista);
            this.RefreshList();
            this.IsRefreshing = false;
        }

        private void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {
                var listaCategoriaVModel = this.listaCategoria.Select(c => new CategoriaItemVModelo
                {
                    idCategoria = c.idCategoria,
                    nombreCat = c.nombreCat,
                    fotoCategoria = c.fotoCategoria
                });



                this.Categorias = new ObservableCollection<CategoriaItemVModelo>(
                    listaCategoriaVModel.OrderBy(c => c.nombreCat));
            }
            else
            {

                var listaCategoriaVModel = this.listaCategoria.Select(c => new CategoriaItemVModelo
                {


                    idCategoria = c.idCategoria,
                    nombreCat = c.nombreCat,
                    fotoCategoria = c.fotoCategoria


                }).Where(c => c.nombreCat.ToLower().Contains(this.filtro.ToLower())).ToList();
                this.Categorias = new ObservableCollection<CategoriaItemVModelo>(listaCategoriaVModel.OrderBy(c => c.nombreCat));
            }
        }


        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCategorias);
            }
        }

    }
}

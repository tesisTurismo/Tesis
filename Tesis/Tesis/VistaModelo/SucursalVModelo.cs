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
    public class SucursalVModelo:BaseVModelo
    {
        //atrubitos
        private ApiServicio apiServicios;

        private ObservableCollection<SucursalItemVModel> sucursales;
        

        private bool isRefreshing;

        private Local local;
        private ImageSource imageSource;
        private string nombreLocal;
        private string descripcionLocal;
        private string pagWebLocal;
      
        //PROPIEDADES
       
        public ObservableCollection<SucursalItemVModel> Sucursales
        {
            get { return this.sucursales; }
            set { this.SetValue(ref this.sucursales, value); }
        }
        public string NombreLocal
        {
            get { return this.nombreLocal; }
            set { this.SetValue(ref this.nombreLocal, value); }
        }
        public string DescripcionLocal
        {
            get { return this.descripcionLocal; }
            set { this.SetValue(ref this.descripcionLocal, value); }
        }
        public string PagWebLocal
        {
            get { return this.pagWebLocal; }
            set { this.SetValue(ref this.pagWebLocal, value); }
        }
        public Local Local
        {
            get;
            set;
        }
       

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }

        //lista de sucursales
        public List<Sucursal>MySucursals{get; set;}

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }

        }

        //constructor
       
        public SucursalVModelo(Local local)
        {
            instancia = this;
            this.Local = local;
            this.apiServicios = new ApiServicio();
            this.ImageSource = local.fotoApp;
          this.NombreLocal = local.nombreLocal;
            this.DescripcionLocal = local.descripcion;
            this.PagWebLocal = local.pagWeb;
            this.LoadSucursales();
           
        }
        private static SucursalVModelo instancia;
        public static SucursalVModelo GetInstancia()
        {
           
            return instancia;
        }


        //métodos
        //cargar datos..
        private async void LoadSucursales()
        {
            this.IsRefreshing = true;
            var conexion = await this.apiServicios.ValidacionInternet();
            if (!conexion.respExitosa)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor conectarse a internet.", "Aceptar");
                return;
            }

            var p = await this.LoadSucursalsFromAPI();
            if (p)
            {
                this.RefreshList();
               
            }
           
            // this.RefreshList();
            this.IsRefreshing = false;
        }


        private async Task<bool> LoadSucursalsFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefijo = Application.Current.Resources["UrlPrefix"].ToString();
            var controlador = Application.Current.Resources["UrlControllerSucursal"].ToString();
            var response = await this.apiServicios.mostrarLista<Sucursal>(url, prefijo, controlador,this.Local.idLocal);

            if (!response.respExitosa)
            {
                return false;
                
            }

           // var lista = (List<Sucursal>)response.resultado;
            this.MySucursals= (List<Sucursal>)response.resultado;
            //this.Sucursales = new ObservableCollection<Sucursal>(lista);
            return true;
        }


        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadSucursales);
            }
        }

        public void RefreshList()
        {


            var mylistaNVM = this.MySucursals.Select(p => new SucursalItemVModel
            {
                idSucursal = p.idSucursal,
                foto = p.foto,
                calle = p.calle,
                numero = p.numero,
                calleIntersección = p.calleIntersección,
                telefono = p.telefono,
                latitud = p.latitud,
                longitud = p.longitud,
                idLocal = p.idLocal,



            });
                this.Sucursales = new ObservableCollection<SucursalItemVModel>(
                    mylistaNVM.OrderBy(p => p.calle));



            }
        
    }
}

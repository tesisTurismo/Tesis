using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Tesis
{
    using Tesis.VistaModelo;
    using Vistas;
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            var vPrincipal = VistaPrincipal.GetInstancia();
            vPrincipal.Categorias = new CategoriasVModel();
            this.MainPage = new NavigationPage(new CategoriasPage());

            //MainPage = new NavigationPage (new NegociosPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

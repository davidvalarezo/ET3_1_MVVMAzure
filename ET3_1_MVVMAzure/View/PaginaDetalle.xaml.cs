using ET3_1_MVVMAzure.Model;
using ET3_1_MVVMAzure.ViewModel;
using Plugin.TextToSpeech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ET3_1_MVVMAzure.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaDetalle : ContentPage
    {
        Listado Persona;
        ListadoViewModel vm;
        public PaginaDetalle(Listado persona, ListadoViewModel viewM)
        {
            InitializeComponent();
            Persona = persona;
            BindingContext = Persona;
            vm = viewM;
            BotonGuardar.Clicked += BotonGuardar_Clicked;
        }

        public void BotonHablar_Clicked(object sender, EventArgs e)
        {
            CrossTextToSpeech.Current.Speak(this.Persona.Description);
        }

        public async void BotonWebr_Clicked(object sender, EventArgs e)
        {
            if (Persona.Website.StartsWith("http"))
                //Device.OpenUri(new Uri(Persona.Website));
                await Launcher.OpenAsync(new Uri(Persona.Website));
        }

        public async void BotonGuardar_Clicked(object sender, EventArgs e)
        {
            Persona.Title = TituloEditable.Text;
            await vm.UpdateListado(Persona);
            await Navigation.PopModalAsync();
        }
    }
}
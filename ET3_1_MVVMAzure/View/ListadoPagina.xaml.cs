using ET3_1_MVVMAzure.Model;
using ET3_1_MVVMAzure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ET3_1_MVVMAzure.View
{
    [ContentProperty("Source")]

    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null) return null;
            return ImageSource.FromResource("ET3_1_MVVMAZURE.Imagenes." + Source, Assembly.GetExecutingAssembly());
            //throw new NotImplementedException();
        }
    }


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListadoPagina : ContentPage
    {
        ListadoViewModel vm;
        public ListadoPagina()
        {
            InitializeComponent();
            vm = new ListadoViewModel();
            BindingContext = vm;

            //BannerComun.Source = ImageSource.FromResource("ET3_1_MVVMAZURE.Imagenes.BannerXam.jpg");
            BannerComun.HorizontalOptions = LayoutOptions.Center;

        }

        private async void ListadoView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var persona = e.SelectedItem as Listado;
            if (persona == null) return;
            PaginaDetalle pd = new PaginaDetalle(persona, vm);
            await Navigation.PushModalAsync(pd);
            ListadoView.SelectedItem = null;

        }

    }


}
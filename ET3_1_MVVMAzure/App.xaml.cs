using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ET3_1_MVVMAzure
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new View.ListadoPagina();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

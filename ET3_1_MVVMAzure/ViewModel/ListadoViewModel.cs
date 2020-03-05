using ET3_1_MVVMAzure.Model;
using ET3_1_MVVMAzure.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ET3_1_MVVMAzure.ViewModel
{
    public class ListadoViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Listado> ListaPersonal { get; set; }
        public Command RecuperaListado { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool busy;

        public bool IsBusy
        {
            get { return busy; }
            set
            {
                busy = value;
                OnPropertyChanged();
                RecuperaListado.ChangeCanExecute();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        private async Task GetLista()
        {
            if (IsBusy) return;
            Exception error = null;
            try
            {
                IsBusy = true;
                //Peticion
                //using (var cliente = new System.Net.Http.HttpClient())
                //{
                //var jsontxt = await cliente.GetStringAsync("http://kona2.alc.upv.es/Listado_jc.txt");
                //var items = JsonConvert.DeserializeObject<List<Listado>>(jsontxt);
                var service = DependencyService.Get<ServicioAzure>();
                var items = await service.GetListado();
                    ListaPersonal.Clear();
                    foreach (var item in items)
                    {
                        ListaPersonal.Add(item);
                    }
                //}
                    

            }
            catch (Exception ex)
            {
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task UpdateListado (Listado persona)
        {
            var service = DependencyService.Get<Services.ServicioAzure>();
            await service.ActualizaListado(persona);
            await GetLista();
        }



    }
}

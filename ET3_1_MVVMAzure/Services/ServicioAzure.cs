using ET3_1_MVVMAzure.Model;
using ET3_1_MVVMAzure.Services;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ServicioAzure))]

namespace ET3_1_MVVMAzure.Services
{
    class ServicioAzure
    {
        public MobileServiceClient MobileService { get; set; } = null;
        IMobileServiceSyncTable<Listado> table;

        public async Task Initialize()
        {
            if (MobileService?.SyncContext?.IsInitialized ?? false) return;
            MobileService = new MobileServiceClient("https://eucmuxamarin.azurewebsites.net");

            var path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Listado>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            table = MobileService.GetSyncTable<Listado>();
        }

        public async Task<IEnumerable<Listado>> GetListado()
        {
            await Initialize();
            await SyncListado();
            return await table.OrderBy(s => s.Name).ToEnumerableAsync();
        }

        public async Task SyncListado()
        {
            try
            {
                await MobileService.SyncContext.PushAsync();
                await table.PullAsync("Listado", table.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No puedo sincronizar Listado, Trabajando Offline "+ ex.Message.ToString());
            }
        }

        public async Task ActualizaListado(Listado persona)
        {
            await Initialize();
            await table.UpdateAsync(persona);
            await SyncListado();
        }
    }
}

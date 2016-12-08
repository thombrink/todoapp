using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Hoornbeeck.Todo.Services;
using Xamarin.Forms;
using Plugin.Connectivity;
using System.Diagnostics;
using System.IO;

[assembly: Dependency(typeof(AzureService))]
namespace Hoornbeeck.Todo.Services {
    public class AzureService {
        public MobileServiceClient Client { get; set; }
        private IMobileServiceSyncTable<Models.Todo> _todoTable;

        public async Task Initialize() {
            if (Client?.SyncContext?.IsInitialized ?? false) return;

            //Create our client
            Client = new MobileServiceClient("https://hoornbeeck-todo.azurewebsites.net");

            //InitialzeDatabase for path
            var path = "syncstore.db";
            path = Path.Combine(MobileServiceClient.DefaultDatabasePath, path);

            //Setup our local sqlite store and intialize our table
            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Models.Todo>();

            //Initialize SyncContext
            await Client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //Get our sync table that will call out to azure
            _todoTable = Client.GetSyncTable<Models.Todo>();
        }

        public async Task SyncTodo() {
            //pull down all latest changes and then push current todo's up
            try {
                if (!CrossConnectivity.Current.IsConnected)
                    return;

                await _todoTable.PullAsync("allTodos", _todoTable.CreateQuery());

                await Client.SyncContext.PushAsync();
            }
            catch (Exception ex) {
                Debug.WriteLine("Unable to sync todo's, that is alright as we have offline capabilities: " + ex);
            }
        }

        public async Task<IEnumerable<Models.Todo>> GetTodos() {
            //Initialize & Sync
            await Initialize();
            await SyncTodo();

            return await _todoTable.OrderBy(c => c.DateUtc).ToEnumerableAsync();
        }

        public async Task<Models.Todo> AddTodo(string note) {
            //create and insert todo
            var todo = new Models.Todo {
                Note = note,
                DateUtc = DateTime.UtcNow,
                OS = Device.OS.ToString()
            };

            await _todoTable.InsertAsync(todo);

            //Synchronize the todo
            await SyncTodo();

            return todo;
        }

        public async Task DeleteTodo(Models.Todo todo) {
            await _todoTable.DeleteAsync(todo);

            //Synchronize the todo
            await SyncTodo();
        }
    }
}

using Hoornbeeck.Todo.ViewModels;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hoornbeeck.Todo.Views {
    public partial class TodosPage : ContentPage {
        TodosViewModel _vm;

        public TodosPage() {
            InitializeComponent();
            BindingContext = _vm = new TodosViewModel(Navigation);
            ListViewTodos.ItemTapped += (sender, e) => {
                _vm.ViewTodoCommand.Execute(null);
            };

            if (Device.OS != TargetPlatform.iOS && Device.OS != TargetPlatform.Android) {
                ToolbarItems.Add(new ToolbarItem {
                    Text = "Vernieuw",
                    Command = _vm.LoadTodosCommand
                });
            }
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            ListViewTodos.SelectedItem = null;

            CrossConnectivity.Current.ConnectivityChanged += ConnecitvityChanged;
            OfflineStack.IsVisible = !CrossConnectivity.Current.IsConnected;

            if (_vm.Todos.Count == 0) _vm.LoadTodosCommand.Execute(null);
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            CrossConnectivity.Current.ConnectivityChanged -= ConnecitvityChanged;
        }

        void ConnecitvityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e) {
            Device.BeginInvokeOnMainThread(() =>
            {
                OfflineStack.IsVisible = !e.IsConnected;
            });
        }
    }
}

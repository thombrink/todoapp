using Todoo.Shared.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Todoo.Shared {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            // The root page of your application
            MainPage = new NavigationPage(new TodosPage()) {
                BarTextColor = Color.White,
                BarBackgroundColor = Color.FromHex("#1c9d9f")
            };
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}

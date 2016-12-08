using Todoo.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Todoo.Shared.Views {
    public partial class TodoPage : ContentPage {
        TodosViewModel _vm;

        public TodoPage(TodosViewModel vm) {
            InitializeComponent();

            BindingContext = _vm = vm;
        }
    }
}
        

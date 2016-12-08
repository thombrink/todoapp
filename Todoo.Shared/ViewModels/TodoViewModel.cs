using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Hoornbeeck.Todo.ViewModels {
    class TodoViewModel : Base {
        public TodoViewModel(INavigation navigation) : base(navigation) {
        }

        ICommand _popTodoCommand;
        public ICommand ViewTodoCommand =>
            _popTodoCommand ?? (_popTodoCommand = new Command(async () => await ExecutePopTodoCommandAsync()));

        async Task ExecutePopTodoCommandAsync() {
            await Navigation.PopAsync();
        }
    }
}

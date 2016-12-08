using MvvmHelpers;
using Xamarin.Forms;

namespace Hoornbeeck.Todo.ViewModels {
    public abstract class Base : BaseViewModel {
        protected INavigation Navigation;

        public Base(INavigation navigation) {
            Navigation = navigation;
        }
    }
}

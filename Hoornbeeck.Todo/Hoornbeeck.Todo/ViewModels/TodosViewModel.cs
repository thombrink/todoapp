using Hoornbeeck.Todo.Services;
using Hoornbeeck.Todo.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Hoornbeeck.Todo.ViewModels {
    public class TodosViewModel : Base {
        AzureService _azureService;

        public TodosViewModel(INavigation navigation) : base(navigation) {
            //azureService = DependencyService.Get<AzureService>();
            _azureService = new AzureService();
        }

        public ObservableRangeCollection<Models.Todo> Todos { get; } = new ObservableRangeCollection<Models.Todo>();
        public ObservableRangeCollection<Grouping<string, Models.Todo>> TodosGrouped { get; } = new ObservableRangeCollection<Grouping<string, Models.Todo>>();

        string _loadingMessage;
        public string LoadingMessage
        {
            get { return _loadingMessage; }
            set { SetProperty(ref _loadingMessage, value); }
        }

        ICommand loadTodosCommand;
        public ICommand LoadTodosCommand =>
            loadTodosCommand ?? (loadTodosCommand = new Command(async () => await ExecuteLoadTodosCommandAsync()));

        async Task ExecuteLoadTodosCommandAsync() {
            if (IsBusy) return;

            try {
                LoadingMessage = "Loading Todo's...";
                IsBusy = true;

                var todos = await _azureService.GetTodos();
                Todos.ReplaceRange(todos);

                SortTodos();
            }
            catch (Exception ex) {
                Debug.WriteLine("OH NO!" + ex);

                await Application.Current.MainPage.DisplayAlert("Sync Error", "Unable to sync todo's, you may be offline", "OK");
            }
            finally {
                IsBusy = false;
            }
        }

        void SortTodos() {
            var groups = from todo in Todos
                         orderby todo.DateUtc descending
                         group todo by todo.DateDisplay
                into todoGroup
                         select new Grouping<string, Models.Todo>($"{todoGroup.Key} ({todoGroup.Count()})", todoGroup);


            TodosGrouped.ReplaceRange(groups);
        }

        TodoPage _todoPage;
        public TodoPage TodoPage => _todoPage ?? (_todoPage = new TodoPage(this));

        Models.Todo _selectedTodo;
        public Models.Todo SelectedTodo
        {
            get { return _selectedTodo; }
            set { SetProperty(ref _selectedTodo, value); }
        }

        string _newNote;
        public string NewNote
        {
            get { return _newNote; }
            set { SetProperty(ref _newNote, value); }
        }

        bool _atHome;
        public bool AtHome
        {
            get { return _atHome; }
            set { SetProperty(ref _atHome, value); }
        }

        ICommand _addTodoCommand;
        public ICommand AddTodoCommand =>
            _addTodoCommand ?? (_addTodoCommand = new Command(async () => await ExecuteAddTodoCommandAsync()));

        async Task ExecuteAddTodoCommandAsync() {
            if (IsBusy || string.IsNullOrEmpty(NewNote)) return;

            try {
                LoadingMessage = "Adding Todo...";
                IsBusy = true;

                var todo = await _azureService.AddTodo(NewNote);
                Todos.Add(todo);
                SortTodos();
            }
            catch (Exception ex) {
                Debug.WriteLine("OH NO!" + ex);
            }
            finally {
                NewNote = string.Empty;
                LoadingMessage = string.Empty;
                IsBusy = false;
            }
        }

        ICommand _viewTodoCommand;
        public ICommand ViewTodoCommand =>
            _viewTodoCommand ?? (_viewTodoCommand = new Command(async () => await ExecuteNewTodoCommandAsync()));

        async Task ExecuteNewTodoCommandAsync() {
            await Navigation.PushAsync(TodoPage);
        }
    }
}

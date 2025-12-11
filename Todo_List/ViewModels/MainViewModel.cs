using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using Todo_List.Commands;
using Todo_List.Models;
using Todo_List.Services;
using Todo_List.Views;

namespace Todo_List.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ITodoService _todoService;

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (Set(ref _selectedDate, value))
                    _ = LoadTasksAsync();
            }
        }

        public ObservableCollection<TaskItemViewModel> Tasks { get; } = new();

        private TaskItemViewModel? _selectedTask;
        public TaskItemViewModel? SelectedTask
        {
            get => _selectedTask;
            set
            {
                if (Set(ref _selectedTask, value))
                {
                    (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand PrevDayCommand { get; }
        public ICommand NextDayCommand { get; }

        private readonly Timer _dayTimer;


        public MainViewModel(ITodoService todoService)
        {
            _todoService = todoService;
            SelectedDate = DateTime.Today;

            AddCommand = new RelayCommand(async _ => await AddTaskAsync());
            EditCommand = new RelayCommand(async _ => await EditTaskAsync(), _ => SelectedTask != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteTaskAsync(), _ => SelectedTask != null);
            PrevDayCommand = new RelayCommand(_ => SelectedDate = SelectedDate.AddDays(-1));
            NextDayCommand = new RelayCommand(_ => SelectedDate = SelectedDate.AddDays(1));

            // Ładowanie danych po starcie
            _ = LoadTasksAsync();

        }

        public async Task LoadTasksAsync()
        {
            try
            {
                var list = await _todoService.GetByDateAsync(SelectedDate);

                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    Tasks.Clear();
                    foreach (var t in list)
                    {
                        Tasks.Add(new TaskItemViewModel
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            Date = t.Date.Date
                        });
                    }
                });

                (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd ładowania zadań: " + ex.Message);
            }
        }

        private async Task AddTaskAsync()
        {
            var editor = new TaskEditorWindow(new TodoItem { Date = SelectedDate.Date });

            if (editor.ShowDialog() == true)
            {
                var vm = editor.ViewModel.Item;

                if (string.IsNullOrWhiteSpace(vm.Title))
                {
                    MessageBox.Show("Tytuł nie może być pusty.");
                    return;
                }

                var entity = new TodoItem
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    Date = vm.Date.Date
                };

                await _todoService.AddAsync(entity);
                await LoadTasksAsync();
            }
        }

        private async Task EditTaskAsync()
        {
            if (SelectedTask == null)
            {
                MessageBox.Show("Nie wybrano zadania do edycji.");
                return;
            }

            var entity = new TodoItem
            {
                Id = SelectedTask.Id,
                Title = SelectedTask.Title,
                Description = SelectedTask.Description,
                Date = SelectedTask.Date
            };

            var editor = new TaskEditorWindow(entity);

            if (editor.ShowDialog() == true)
            {
                var vm = editor.ViewModel.Item;

                if (string.IsNullOrWhiteSpace(vm.Title))
                {
                    MessageBox.Show("Tytuł nie może być pusty.");
                    return;
                }

                var dbEntity = await _todoService.GetByIdAsync(vm.Id);
                dbEntity.Title = vm.Title;
                dbEntity.Description = vm.Description;
                dbEntity.Date = vm.Date.Date;

                await _todoService.UpdateAsync(dbEntity);
                await LoadTasksAsync();
            }
        }

        private async Task DeleteTaskAsync()
        {
            if (SelectedTask == null)
                return;

            var res = MessageBox.Show("Usunąć zadanie?", "Potwierdzenie", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                await _todoService.DeleteAsync(SelectedTask.Id);
                await LoadTasksAsync();
            }
        }



    }
}

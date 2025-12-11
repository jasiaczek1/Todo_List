using System;
using System.Windows.Input;
using Todo_List.Commands;
using Todo_List.Models;

namespace Todo_List.ViewModels
{
    public class TaskEditorViewModel : ViewModelBase
    {
        public TodoItem Item { get; }

        public ICommand SaveCommand { get; }

        public event EventHandler<bool>? RequestClose;

        public TaskEditorViewModel(TodoItem item)
        {
            Item = item;
            SaveCommand = new RelayCommand(_ => Save());
        }

        private void Save()
        {
            RequestClose?.Invoke(this, true);
        }
    }
}

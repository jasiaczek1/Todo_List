using System.Windows;
using Todo_List.Models;
using Todo_List.ViewModels;

namespace Todo_List.Views
{
    public partial class TaskEditorWindow : Window
    {
        public TaskEditorViewModel ViewModel { get; }

        public TaskEditorWindow(TodoItem item)
        {
            InitializeComponent();
            ViewModel = new TaskEditorViewModel(item);
            DataContext = ViewModel;
            ViewModel.RequestClose += (s, result) => { DialogResult = result; Close(); };
        }
    }
}

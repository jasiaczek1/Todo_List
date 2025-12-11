using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class TaskItemViewModel : INotifyPropertyChanged
{
    private int _id;
    public int Id
    {
        get => _id;
        set { _id = value; OnPropertyChanged(); }
    }

    private string _title;
    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    private string _description;
    public string Description
    {
        get => _description;
        set { _description = value; OnPropertyChanged(); }
    }

    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set { _date = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

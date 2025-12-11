using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Windows;
using Todo_List.Data;
using Todo_List.Services;
using Todo_List.ViewModels;

namespace Todo_List.Views
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _db;
        private readonly TodoService _service;

        public MainWindow()
        {
            InitializeComponent();


            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "todo.db");


            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={dbPath}")
                .Options;

            _db = new AppDbContext(options);
            _service = new TodoService(_db);

            DataContext = new MainViewModel(_service);



        }
    }
}

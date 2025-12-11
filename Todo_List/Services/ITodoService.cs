using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo_List.Models;

namespace Todo_List.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetByDateAsync(DateTime date);
        Task<TodoItem?> GetByIdAsync(int id);
        Task AddAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(int id);
    }
}
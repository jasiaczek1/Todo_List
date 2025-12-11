using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo_List.Data;
using Todo_List.Models;

namespace Todo_List.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetByDateAsync(DateTime date)
        {
            var start = date.Date;
            var end = start.AddDays(1);

            return await _context.TodoItems
                .Where(t => t.Date >= start && t.Date < end)
                .ToListAsync();
        }


        public async Task<TodoItem?> GetByIdAsync(int id)
            => await _context.TodoItems.FindAsync(id);

        public async Task AddAsync(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem item)
        {
            _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return;
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
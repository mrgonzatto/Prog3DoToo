using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.Repositories.Postgres
{
    public class TodoItemRepository : ITodoItemRepository
    {
        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;

        public Task AddItem(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdate(TodoItem item)
        {
            throw new NotImplementedException();
        }

        public Task<List<TodoItem>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task UpdateItem(TodoItem item)
        {
            throw new NotImplementedException();
        }
    }
}

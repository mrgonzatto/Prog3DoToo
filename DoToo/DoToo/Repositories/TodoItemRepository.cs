using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using System.Linq;

namespace DoToo.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private SQLiteAsyncConnection connection;

        private async Task CreateConnection()
        {
            if (connection != null)
                return;

            var documentPath = 
                Environment.GetFolderPath(
                    Environment.SpecialFolder.MyDocuments
                );

            var databasePath =
                Path.Combine(documentPath, "TodoItems.db");

            connection = new SQLiteAsyncConnection( databasePath );
            
            await connection.CreateTableAsync<TodoItem>();
            // Aqui poderíamos adicionar tantas quantas tabelas
            // existissem no modelo de nossa aplicação

            if ( await connection.Table<TodoItem>().CountAsync() == 0 )
            {
                await connection.InsertAsync(
                    new TodoItem() { 
                        Title = "Seja bem-vindo",
                        Due = DateTime.Now
                    }
                );
            }
        }

        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;

        public async Task AddItem(TodoItem item)
        {       
            await CreateConnection();
            await connection.InsertAsync( item );
            OnItemAdded?.Invoke(this, item);
        }

        public async Task AddOrUpdate(TodoItem item)
        {
            if (item.Id == 0)
            {
                await AddItem(item);
            }
            else
            { 
                await UpdateItem(item);
            }
        }

        public async Task<List<TodoItem>> GetItems()
        {
            await CreateConnection();
            return await connection.Table<TodoItem>().ToListAsync();
        }

        public async Task<List<TodoItem>> GetItems(
            DateTime dtInicio, DateTime dtFim
        )
        {
            await CreateConnection();
            return await connection.Table<TodoItem>()
                            .Where(w => w.Due >= dtInicio && w.Due <= dtFim)
                            .ToListAsync();
        }

        public async Task<List<TodoItem>> GetItems(
            DateTime dtInicio, DateTime dtFim, bool concluido
        )
        {
            await CreateConnection();
            return await connection.Table<TodoItem>()
                            .Where(w => w.Due >= dtInicio && w.Due <= dtFim)
                            .Where(w => w.Completed == concluido)
                            .ToListAsync();
        }

        public async Task UpdateItem(TodoItem item)
        {
            await CreateConnection();
            await connection.UpdateAsync( item );
            OnItemUpdated?.Invoke(this, item);
        }
    }
}

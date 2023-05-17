using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;
using TodoListApi.Repositories.IRepositories;

namespace TodoListApi.Repositories
{
    public class TodoTagRepository : ITodoTagRepository
    {
        private TodoDbContext _db;
        private bool disposed = false;

        public TodoTagRepository(TodoDbContext db)
        {
            _db = db;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Add(TodoTag todoTag)
        {
            _db.TodoTags.Add(todoTag);
        }

        public async Task<int> Save()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task DeleteByTodoId(long todoId)
        { 
            var lst = await _db.TodoTags.Where(o => o.TodoId == todoId).ToListAsync();
            if (lst.Count > 0)
            {
                _db.TodoTags.RemoveRange(lst);
                await Save();
            }
        }

        public async Task<bool> Existed(long todoId, long tagId)
        {
            var existed = await _db.TodoTags.Where(o => o.TodoId == todoId && o.TagId == tagId).FirstOrDefaultAsync();
            return existed != null;
        }
    }
}

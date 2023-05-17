using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;
using TodoListApi.Repositories.IRepositories;

namespace TodoListApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private TodoDbContext _db;
        private bool disposed = false;

        public TodoRepository(TodoDbContext db)
        {
            _db = db;
        }

        public async Task<List<Todo>> GetAll()
        {
            return await _db.Todos.ToListAsync();
        }

        public async Task<Todo> GetById(long id)
        {
            return await _db.Todos.Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public void Add(Todo todo)
        {
            _db.Todos.Add(todo);
        }

        public void Update(Todo todo)
        {
            _db.Entry(todo).State = EntityState.Modified;
        }

        public void Delete(Todo todo)
        {
            _db.Todos.Remove(todo);
        }

        public async Task<int> Save()
        {
            return await _db.SaveChangesAsync();
        }

        public IQueryable<Todo> GetAllAsQueryable()
        { 
            return _db.Todos.AsQueryable();
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
    }
}

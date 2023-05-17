using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;
using TodoListApi.Repositories.IRepositories;

namespace TodoListApi.Repositories
{
    public class TagRepository : ITagRepository
    {
        private TodoDbContext _db;
        private bool disposed = false;

        public TagRepository(TodoDbContext db)
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

        public async Task<Tag> GetByName(string name)
        {
            return await _db.Tags.Where(o => o.Name.ToUpper() == name.ToUpper()).FirstOrDefaultAsync();
        }

        public void Add(Tag tag)
        {
            _db.Tags.Add(tag);
        }

        public async Task<int> Save()
        {
            return await _db.SaveChangesAsync();
        }
    }
}

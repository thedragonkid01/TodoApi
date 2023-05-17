using TodoListApi.Models;

namespace TodoListApi.Repositories.IRepositories
{
    public interface ITodoRepository : IDisposable
    {
        Task<List<Todo>> GetAll();
        IQueryable<Todo> GetAllAsQueryable();
        Task<Todo> GetById(long id);
        void Add(Todo todo);
        void Delete(Todo todo);
        void Update(Todo todo);
        Task<int> Save();
    }
}

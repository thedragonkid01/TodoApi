using TodoListApi.Models;

namespace TodoListApi.Repositories.IRepositories
{
    public interface ITodoTagRepository : IDisposable
    {
        void Add(TodoTag todoTag);
        Task DeleteByTodoId(long todoId);
        Task<int> Save();
        Task<bool> Existed(long todoId, long tagId);
    }
}

using TodoListApi.Models;

namespace TodoListApi.Repositories.IRepositories
{
    public interface ITagRepository : IDisposable
    {
        Task<Tag> GetByName(string name);
        void Add(Tag tag);
        Task<int> Save();
    }
}

using TodoListApi.DTOs;
using TodoListApi.DTOs.Requests;
using TodoListApi.DTOs.Responses;

namespace TodoListApi.Services.IServices
{
    public interface ITodoService
    {
        Task<GetAllTodoResponse> GetAll();
        Task<SearchTodoResponse> Search(SearchTodoRequest request);
        Task<CreateTodoResponse> Add(CreateTodoRequest request);
        Task<ResponseBase> UpdateStatus(UpdateTodoStatusRequest request);
        Task<UpdateTodoResponse> Update(UpdateTodoRequest request);
        Task<ResponseBase> Delete(long id);
    }
}

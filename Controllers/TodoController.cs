using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.DTOs.Requests;
using TodoListApi.DTOs.Responses;
using TodoListApi.Services.IServices;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            this._todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllTodoResponse>> GetAll()
        {
            GetAllTodoResponse response = await _todoService.GetAll();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CreateTodoResponse>> CreateTodo(CreateTodoRequest request)
        {
            CreateTodoResponse response = await _todoService.Add(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("updateStatus")]
        public async Task<ActionResult<ResponseBase>> UpdateStatus(UpdateTodoStatusRequest request)
        {
            ResponseBase response = await _todoService.UpdateStatus(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult<UpdateTodoResponse>> Update(UpdateTodoRequest request)
        {
            UpdateTodoResponse response = await _todoService.Update(request);
            return Ok(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ResponseBase>> DeleteTodo(long id)
        {
            ResponseBase response = await _todoService.Delete(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("search")]
        public async Task<ActionResult<SearchTodoResponse>> SearchTodo(SearchTodoRequest request)
        {
            SearchTodoResponse response = await _todoService.Search(request);
            return Ok(response);
        }
    }
}

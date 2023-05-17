namespace TodoListApi.DTOs.Responses
{
    public class GetAllTodoResponse: ResponseBase
    {
        public GetAllTodoData Data { get; set; }

        public GetAllTodoResponse()
        {
        }

        public GetAllTodoResponse(bool success)
        {
            base.Success = success;
        }
    }

    public class GetAllTodoData
    {
        public List<TodoDTO> TodoList { get; set; }
    }
}

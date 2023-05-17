namespace TodoListApi.DTOs.Responses
{
    public class SearchTodoResponse : ResponseBase
    {
        public SearchTodoData Data { get; set; }

        public SearchTodoResponse()
        {
        }

        public SearchTodoResponse(bool success)
        {
            base.Success = success;
        }
    }

    public class SearchTodoData
    {
        public List<TodoDTO> TodoList { get; set; }
    }
}

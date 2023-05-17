namespace TodoListApi.DTOs.Responses
{
    public class CreateTodoResponse: ResponseBase
    {
        public CreateTodoData Data { get; set; }

        public CreateTodoResponse()
        {
        }

        public CreateTodoResponse(bool success)
        {
            base.Success = success;
        }
    }

    public class CreateTodoData
    {
        public TodoDTO Todo { get; set; }
    }
}

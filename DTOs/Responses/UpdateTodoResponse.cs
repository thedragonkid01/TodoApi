namespace TodoListApi.DTOs.Responses
{
    public class UpdateTodoResponse : ResponseBase
    {
        public UpdateTodoData Data { get; set; }

        public UpdateTodoResponse()
        {
        }

        public UpdateTodoResponse(bool success)
        {
            base.Success = success;
        }
    }

    public class UpdateTodoData
    {
        public TodoDTO Todo { get; set; }
    }
}

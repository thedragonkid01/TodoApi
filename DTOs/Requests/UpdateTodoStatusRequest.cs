namespace TodoListApi.DTOs.Requests
{
    public class UpdateTodoStatusRequest
    {
        public long Id { get; set; }
        public int Status { get; set; }
    }
}

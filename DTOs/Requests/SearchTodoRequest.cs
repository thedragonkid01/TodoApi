namespace TodoListApi.DTOs.Requests
{
    public class SearchTodoRequest
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Status { get; set; }
    }
}

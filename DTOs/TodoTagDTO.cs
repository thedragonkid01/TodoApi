namespace TodoListApi.DTOs
{
    public class TodoTagDTO
    {
        public long TodoId { get; set; }
        public TodoDTO Todo { get; set; }
        public long TagId { get; set; }
        public TagDTO Tag { get; set; }
    }
}

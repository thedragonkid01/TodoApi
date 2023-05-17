namespace TodoListApi.Models
{
    public class TodoTag
    {
        public long TodoId { get; set; }
        public Todo Todo { get; set; }
        public long TagId { get; set; }
        public Tag Tag { get; set; }
    }
}

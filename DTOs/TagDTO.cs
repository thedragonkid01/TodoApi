using System.ComponentModel.DataAnnotations;

namespace TodoListApi.DTOs
{
    public class TagDTO
    {
        public long Id { get; set; }
        //[Required]
        public string Name { get; set; } = String.Empty;
    }

    public class TagCreateTodoDTO
    {
        public string Name { get; set; } = String.Empty;
    }
}

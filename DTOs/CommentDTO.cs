using System.ComponentModel.DataAnnotations;

namespace TodoListApi.DTOs
{
    public class CommentDTO
    {
        public long Id { get; set; }
        //[Required]
        public string Title { get; set; } = String.Empty;
    }

    public class CommentCreateTodoDTO
    {
        public string Title { get; set; } = String.Empty;
    }
}

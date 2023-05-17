using System.ComponentModel.DataAnnotations;

namespace TodoListApi.DTOs
{
    public class TodoDTO
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime? DueDate { get; set; }
        public ICollection<TagDTO> Tags { get; set; } = new List<TagDTO>();
        public ICollection<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }

    public class TodoCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public ICollection<TagCreateTodoDTO> Tags { get; set; } = new List<TagCreateTodoDTO>();
        public ICollection<CommentCreateTodoDTO> Comments { get; set; } = new List<CommentCreateTodoDTO>();
    }

    public class TodoUpdateDTO
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public int Status { get; set; }
        public DateTime? DueDate { get; set; }
        public ICollection<TagCreateTodoDTO> Tags { get; set; } = new List<TagCreateTodoDTO>();
    }
}

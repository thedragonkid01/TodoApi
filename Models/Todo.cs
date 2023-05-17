using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApi.Models
{
    public class Todo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public DateTime? DueDate { get; set; }
        public virtual IList<TodoTag> TodoTags { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApi.Models
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Todo Todo { get; set; }
    }
}

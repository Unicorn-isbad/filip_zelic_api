using System.ComponentModel.DataAnnotations;

namespace Ispit.API.Models
{
    public class TodoList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }
}

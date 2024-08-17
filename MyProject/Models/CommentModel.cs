using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Comments")]
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }

        [ForeignKey("AnimalId")]
        public int AnimalId { get; set; }
        public virtual Animal? Animal { get; set; }

        [StringLength(120, MinimumLength = 2)]
        public string? Comment { get; set; }    
    }
}

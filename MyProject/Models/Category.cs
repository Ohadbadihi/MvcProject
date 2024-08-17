using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter Category name of animel")]
        public string? CategoryName { get; set; }


        public virtual ICollection<Animal>? Animals { get; set; }
    }
}

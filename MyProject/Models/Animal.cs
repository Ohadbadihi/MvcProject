using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{

    public class Animal

    {
        [Key]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Please enter the animal's name.")]
        [StringLength(50,MinimumLength = 2,ErrorMessage ="Name must be between 2 - 50 characters.")]      
        public string? Name { get; set; }


        [Required(ErrorMessage = "Please enter the animal's age.")]
        [Range(1,100, ErrorMessage = "Age must be an integer between 0 and 100.")]
        public int Age { get; set; }


        public string? PictureName { get; set; }


        [Required(ErrorMessage = "Please enter the animal's description")]
        [StringLength(630,MinimumLength = 20,ErrorMessage = "Description must be between 20 and 630 characters.")]
        public string? Description { get; set; }


        [Required(ErrorMessage = "Please select a category.")]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }


        public virtual ICollection<CommentModel>? Comments { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PostDisplayModel 
    {
        [Required]
        [StringLength(15, ErrorMessage = "Title is too long.")]
        [MinLength(5, ErrorMessage = "Title is too short.")]
        public string Title { get; set; }
        
        [Required]
        [StringLength(200, ErrorMessage = "Body is too long.")]
        [MinLength(20, ErrorMessage = "Body is too short.")]
        public string Body { get; set; }
        
        [Required]
        public bool Published { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models.Dto.Movie
{
    public class AddMovie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }

        [Required]
        public string Director { get; set; }
        [Required]
        public int Year { get; set; }


        [Display(Name = "Insert an Image")]
        public string MoviePhoto { get; set; }
    }
}

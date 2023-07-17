using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace MovieApp.Models.Domain
{
    public class Movies
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

        public List<Comment> Comments { get; set; }

    }
}

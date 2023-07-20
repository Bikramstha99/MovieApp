using Microsoft.Build.Framework;
using MovieApp.Models.Domain;
using MovieApp.Models.Dto.Comment;
using MovieApp.Models.Dto.Pager;
using MovieApp.Models.Dto.Rating;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models.Dto.Movie
{
    public class UpdateMovie
    {
        [Key]
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Genre { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Director { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int Year { get; set; }


        [Display(Name = "Insert an Image")]
        public string MoviePhoto { get; set; }
        public List<UpdateMovie> Movie { get; set; }
        public List<UpdateComment> Comments { get; set; }
        public List<AddRating>Ratings { get; set; }
        public double AverageRating { get; set; }
        public AddRating? Rat{ get; set; }
        public PagerVM Pager { get; set; }

    }
}

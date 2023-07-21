using Microsoft.AspNetCore.Identity;
using MovieApp.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models.Dto.Rating
{
    public class AddRating
    {
        public int RatingId { get; set; }
        public int Ratings { get; set; }

        [ForeignKey("Movies")]
        public int MovieId { get; set; }
        public Movies? Movies { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public double AverageRating { get; set; }
    }
}

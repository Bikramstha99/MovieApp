using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using MovieApp.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models.Dto.Comment
{
    public class UpdateComment
    {

        [Key]
        public int CommentId { get; set; }
        
        public string UserName { get; set; }

        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }


        [ForeignKey("Movies")]
        public int MovieId { get; set; }
        public Movies Movies { get; set; }

        public string? CommentDesc { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

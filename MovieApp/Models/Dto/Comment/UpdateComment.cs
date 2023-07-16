﻿using Microsoft.AspNetCore.Identity;
using MovieApp.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models.Dto.Comment
{
    public class UpdateComment
    {

        [Key]
        public int CommentId { get; set; }


        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        [ForeignKey("Movies")]
        public int MovieId { get; set; }
        public Movies Movies { get; set; }

        public string? CommentDesc { get; set; }
    }
}

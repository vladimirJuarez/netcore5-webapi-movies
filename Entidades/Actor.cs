using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace back_end.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:200)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Biography { get; set; }
        public string Picture { get; set; }
    }
}
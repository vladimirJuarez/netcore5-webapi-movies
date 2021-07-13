using System;
using System.ComponentModel.DataAnnotations;
using back_end.Validations;
using Microsoft.AspNetCore.Http;

namespace back_end.DTOs
{
    public class ActorCreationDTO
    {
        [Required]
        [StringLength(maximumLength:200)]
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Biography { get; set; }
        public IFormFile Picture { get; set; }
    }
}
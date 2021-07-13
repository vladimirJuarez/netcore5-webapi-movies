using System;
using System.ComponentModel.DataAnnotations;

namespace back_end.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Biography { get; set; }
        public string Picture { get; set; }
    }
}
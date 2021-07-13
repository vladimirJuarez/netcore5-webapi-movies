using System.ComponentModel.DataAnnotations;
using back_end.Validations;

namespace back_end.DTOs
{
    public class GenreCreationDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:50)]
        //validation by attribute
        [FirstCapitalLetter]
        public string Name { get; set; }
    }
}   
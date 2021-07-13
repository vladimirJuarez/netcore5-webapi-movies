using System;
using System.ComponentModel.DataAnnotations;
using back_end.Validations;

namespace back_end.ViewModels
{
    public class GenreCreationViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        //[StringLength(maximumLength:10)]
        [StringLength(maximumLength:50)]
        //validation by attribute
        [FirstCapitalLetter]
        public string Nombre { get; set; }
    }
}
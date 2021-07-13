using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using back_end.Validations;

namespace back_end.Entidades
{
    public class Genre//: IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:50)]
        //validation by attribute
        [FirstCapitalLetter]
        public string Name { get; set; }

        //validation by model, it is executed just when validation via attribute is success
        /*°ublic IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var firstLetter = Name[0].ToString();
                if (firstLetter != firstLetter.ToUpper())
                {
                    yield return new ValidationResult("First letter must be capital(MODEL)",
                        new string[] {nameof(Name)});
                }
            }
        }*/
    }
}
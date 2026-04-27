using System.ComponentModel.DataAnnotations;

namespace DevRequestPortal.ViewModels.Request
{
    public class ScreeningRequest
    {
        [Required]
        public string Category { get; set; } = string.Empty; // dashboard | form | workflow | newapp | migrate | integration | other

        [Required, MinLength(5), MaxLength(5)]
        public List<int> Answers { get; set; } = new();
    }
}
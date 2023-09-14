using System.ComponentModel.DataAnnotations;

namespace LoginProgramacion3.Models.ViewModels
{
    public class CredencialesViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}

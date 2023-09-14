using System.ComponentModel.DataAnnotations;

namespace LoginProgramacion3.Models.ViewModels
{
    public class UsuarioViewModel:CredencialesViewModel
    {

        [Required]
        public string Nombre { get; set; }

        
    }
}

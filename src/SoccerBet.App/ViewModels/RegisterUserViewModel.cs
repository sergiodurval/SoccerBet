using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SoccerBet.App.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        [JsonProperty("password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        [JsonProperty("confirmPassword")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

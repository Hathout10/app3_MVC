using System.ComponentModel.DataAnnotations;

namespace app3.PL.ViewModel.Auth
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password Is Requierd!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Requierd!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirmed Password doesn't match the password")]
        public string ConfirmPassword { get; set; }
    }
}

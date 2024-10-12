using System.ComponentModel.DataAnnotations;

namespace app3.PL.ViewModel.Auth
{
    public class ForgetPasswardViewModel
    {

        [Required(ErrorMessage = "Email Is Requierd!")]
        [EmailAddress(ErrorMessage = "Invalid Email !!")]
        public string Email { get; set; }
    }
}

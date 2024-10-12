using System.ComponentModel.DataAnnotations;

namespace app3.PL.ViewModel.Auth
{
	public class SignInViewModel
	{

		[Required(ErrorMessage = "Email Is Requierd!")]
		[EmailAddress(ErrorMessage = "Invalid Email !!")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Requierd!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

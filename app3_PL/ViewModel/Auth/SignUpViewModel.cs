using System.ComponentModel.DataAnnotations;

namespace app3.PL.ViewModel.Auth
{
	public class SignUpViewModel
	{
        [Required(ErrorMessage ="UserName Is Requierd!")]
        public  string UserName { get; set; }

		[Required(ErrorMessage = "FirstName Is Requierd!")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName Is Requierd!")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Email Is Requierd!")]
		[EmailAddress(ErrorMessage ="Invalid Email !!")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Requierd!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "ConfirmPassword Is Requierd!")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage = "Confirmed Password doesn't match the password")]
		public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }




    }
}

using app3.DaL.models;
using app3.PL.Helper;
using app3.PL.ViewModel.Auth;
using app3_MVC.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace app3.PL.Controllers
{
	public class AccountController : Controller
	{
		public UserManager<ApplicationUser> _userManager { get; }
		public SignInManager<ApplicationUser> _signInManager { get; }

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}




		#region signup

		//signUp

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{
				//signUp
				try
				{
					var user = await _userManager.FindByNameAsync(model.UserName);
					if (user is null)
					{
						user = await _userManager.FindByEmailAsync(model.Email);
						if (user is null)
						{
							user = new ApplicationUser()
							{

								UserName = model.UserName,
								FirstName = model.FirstName,
								LastName = model.LastName,
								Email = model.Email,
								IsAgree = model.IsAgree

							};
							var result = await _userManager.CreateAsync(user, model.Password);

							if (result.Succeeded)
							{
								return Redirect("SignIn");
							}

							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}

						}
						ModelState.AddModelError(string.Empty, "Email is already exists");
						return View();


					}
					ModelState.AddModelError(string.Empty, "UserName is already exists");

				}
				catch (Exception ex)
				{

					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View();
		}
		#endregion


		#region ٍSignIn

		//SignIn

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{

						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Home");

						}
					}

				}
				ModelState.AddModelError(string.Empty, "invalid Login !");
			}
			return View(model);
		}
		#endregion

		#region sign out
		[HttpGet]
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}

		#endregion


		#region Forget Password

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendResetPassword(ForgetPasswardViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user is not null)
				{
					//Create Token

					var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					//Create Reset Password URL

					var url = Url.Action("ResetPassword", "Account", new { Email = model.Email, token = token }, Request.Scheme /*--> الموجود default اللي هو ال launchSettings بتاعك من ال Url بتجيب شكل ال */);

					//Create Email

					var email = new Email()
					{
						To = model.Email,
						subject = "Reset Password",
						body = url
					};


					//Send Email

					EmailSettings.SendEmail(email);

					return RedirectToAction(nameof(CheckyourInbox));

				}
				ModelState.AddModelError(string.Empty, "Invalid Operation , Please Try Agin !!");
			}
			return View(model);
		}
		[HttpGet]
		public IActionResult CheckyourInbox()
		{
			return View();
		}
		#endregion


		#region ResetPassword

		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				if (user != null)
				{
					var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}

				}
			}
			ModelState.AddModelError(string.Empty, "invalid operation , Pls Try Again !");

			return View(model);
		}
		#endregion

		public IActionResult AccessDenied()
		{
			return View();
		}


	}
}

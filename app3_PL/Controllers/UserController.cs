using app3.BLL;
using app3.BLL.interfacees;
using app3.DaL.models;
using app3.PL.Helper;
using app3.PL.ViewModel;
using app3.PL.ViewModel.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app3.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}

		public async Task<IActionResult> Index(string inputSearch)
		
        {
			var users = Enumerable.Empty<UserViewModel>();

			if (string.IsNullOrEmpty(inputSearch))
			{
				 users= await _userManager.Users.Select(u => new UserViewModel()
				{
					Id = u.Id,
					FirstName = u.FirstName,
					LastName = u.LastName,
					Email = u.Email,
					Roles = _userManager.GetRolesAsync(u).Result
				}).ToListAsync();

			}
			else
			{
				users=await _userManager.Users.Where(u => u.Email
				                  .ToLower()
								  .Contains(inputSearch.ToLower()))
					              .Select(u=>new UserViewModel()
								  {
									  Id = u.Id,
									  FirstName = u.FirstName,
									  LastName = u.LastName,
									  Email = u.Email,
									  Roles = _userManager.GetRolesAsync(u).Result
								  }).ToListAsync();
			}



			return View(users);
		}


        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
           
            if (id is null)
            {

                return BadRequest();//400
            }
			  var userfromDb=await _userManager.FindByIdAsync(id);

			if (userfromDb == null) return NotFound();
			var user = new UserViewModel()
			{
				Id = userfromDb.Id,
				FirstName = userfromDb.FirstName,
				LastName = userfromDb.LastName,
				Email = userfromDb.Email,
				Roles = _userManager.GetRolesAsync(userfromDb).Result
			};


            return View(viewName, user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
         
            return await Details(id, "Edit");
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserViewModel model)
        {

            try
            {
                if (id != model.Id) return BadRequest();

              
                    var userfromDb = await _userManager.FindByIdAsync(id);

                    if (userfromDb == null) return NotFound();

                userfromDb.FirstName = model.FirstName;
                userfromDb.LastName = model.LastName;
                userfromDb.Email = model.Email;
                  
           
                var count = await _userManager.UpdateAsync(userfromDb);

                if (count.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception EX)
            {
                ModelState.AddModelError(String.Empty, EX.Message);
            }



            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {

            return await Details(id, "Delete");
        }



        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute] string id, UserViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();


                var userfromDb = await _userManager.FindByIdAsync(id);

                if (userfromDb == null) return NotFound();

                var count = await _userManager.DeleteAsync(userfromDb);

                if (count.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception EX)
            {
                ModelState.AddModelError(String.Empty, EX.Message);
            }

            return RedirectToAction("Delete");

        }



    }
}

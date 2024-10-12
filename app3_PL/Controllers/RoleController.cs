using app3.DaL.models;
using app3.PL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app3.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string inputSearch)
        {
            var users = Enumerable.Empty<RoleviewModel>();

            if (string.IsNullOrEmpty(inputSearch))
            {
                users = await _roleManager.Roles.Select(u => new RoleviewModel()
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToListAsync();

            }
            else
            {
                users = await _roleManager.Roles.Where(u => u.Name
                                  .ToLower()
                                  .Contains(inputSearch.ToLower()))
                                  .Select(u => new RoleviewModel()
                                  {
                                      Id = u.Id,
                                      Name = u.Name
                                  }).ToListAsync();
            }



            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RoleviewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = model.Name,
                };

                await _roleManager.CreateAsync(role);

                return RedirectToAction("Index");


            }

            return View(model);
        }



        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {

            if (id is null)
            {

                return BadRequest();//400
            }
            var userfromDb = await _roleManager.FindByIdAsync(id);

            if (userfromDb == null) return NotFound();
            var user = new RoleviewModel()
            {
                Id = userfromDb.Id,
                Name = userfromDb.Name,

            };


            return View(viewName, user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {

            return await Details(id, "Edit");
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleviewModel model)
        {

            try
            {
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var userfromDb = await _roleManager.FindByIdAsync(id);

                    if (userfromDb == null) return NotFound();
                    userfromDb.Name = model.Name;
                    await _roleManager.UpdateAsync(userfromDb);


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
        public async Task<IActionResult> DeleteConfirmed([FromRoute] string id, RoleviewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var userfromDb = await _roleManager.FindByIdAsync(id);

                    if (userfromDb == null) return NotFound();
                    userfromDb.Name = model.Name;
                    await _roleManager.DeleteAsync(userfromDb);


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
        public async Task<IActionResult> AddOrRemoveUser(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            ViewData["id"] = id;

            var usersinRole = new List<UsersInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();


            foreach (var item in users)
            {
                var userinRole = new UsersInRoleViewModel()
                {
                    UserId = item.Id,
                    UserName = item.UserName,
                };

                if (await _userManager.IsInRoleAsync(item, role.Name))
                {
                    userinRole.IsSelected = true;
                }
                else
                {
                    userinRole.IsSelected = false;
                }

                usersinRole.Add(userinRole);

            }


            return View(usersinRole);

        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string id, List<UsersInRoleViewModel> models)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();


            if (ModelState.IsValid)
            {

                foreach (var item in models)
                {
                    var appuser = await _userManager.FindByIdAsync(item.UserId);
                    if (appuser is not null)
                    {
                        if (item.IsSelected && ! await _userManager.IsInRoleAsync(appuser,role.Name))
                        {
                            await _userManager.AddToRoleAsync(appuser, role.Name);
                        }
                        else if(!item.IsSelected && await _userManager.IsInRoleAsync(appuser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appuser, role.Name);

                        }

                    }


                }


                return RedirectToAction("Edit", new { id = id });
            }

            return View(models);

        }


    }
}

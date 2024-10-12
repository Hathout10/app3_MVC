using app3.BLL;
using app3.BLL.interfacees;
using app3.BLL.interfaces;
using app3.BLL.Repostry;
using app3.DaL.models;
using app3.PL.Helper;
using app3.PL.ViewModel.Departments;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app3.PL.Controllers
{
	[Authorize]
	public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepo departmentRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(
            //IDepartmentRepo repo,
            IUnitOfWork unitOfWork,
            IMapper mapper
            
            )
        {
            //departmentRepo = repo;
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var departments=await unitOfWork.DepartmentRepo.GetAllAsync();
          var department=  _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {

           var department01=  _mapper.Map<Department>(department);
            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepo.AddAsync(department01);
                var count =await unitOfWork.CompleteAsync();


                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }

        public async  Task<IActionResult> Details(int? id ,string viewName="Details")
        {
            if(id is null )
            {
                return BadRequest();//400
            }
           var result=await unitOfWork.DepartmentRepo.GetAsync(id.Value);

            if (result == null)  return NotFound();

          var department=  _mapper.Map<DepartmentViewModel>(result);

            return View(viewName,department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
        //    if(id is null ) return BadRequest();

        //   var department=departmentRepo.Get(id);
        //    if (department == null) return NotFound();

        //    return View(department);
        return await Details(id,"Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]int? id,DepartmentViewModel department)
        {
           var department01= _mapper.Map<Department>(department);
            try
            {
                if (id != department01.Id) return BadRequest();

                if (ModelState.IsValid)
                {

                    unitOfWork.DepartmentRepo.UpdateAsync(department01);
                    var count =await unitOfWork.CompleteAsync();

                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }
             
            }
            catch (Exception EX)
            {
                ModelState.AddModelError(String.Empty, EX.Message);
            }
          

            return View(department);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var department =await unitOfWork.DepartmentRepo.GetAsync(id.Value);
            if (department == null) return NotFound();
         var department01=   _mapper.Map<DepartmentViewModel>(department);

            return View(department01); 
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(DepartmentViewModel department)
        {
            if (department == null) return NotFound();
            if (ModelState.IsValid)
            {

                var department01 = _mapper.Map<Department>(department);
                unitOfWork.DepartmentRepo.DeleteAsync(department01);
                var count =await unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            
            return RedirectToAction("Delete");
        }
    }



}


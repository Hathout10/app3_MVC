using app3.BLL.interfacees;
using app3.BLL.interfaces;
using app3.BLL.Repostry;
using app3.DaL.models;
using app3.PL.Helper;
using app3.PL.ViewModel.Employee;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace app3.PL.Controllers
{
    [Authorize]
	public class EmployeeController : Controller
    {
        //private  readonly IEmployeeRepo _employeeRepo;
        //private readonly IDepartmentRepo _departmentRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepo employeeRepo,
            //IDepartmentRepo departmentRepo,
            IUnitOfWork unitOfWork,
            IMapper mapper

            )
        {
            //_employeeRepo = employeeRepo;
            //_departmentRepo = departmentRepo;
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string inputSearch)
        {
            var employee = Enumerable.Empty<Employee>();

            if (string.IsNullOrEmpty(inputSearch))
            {
                employee =await unitOfWork.EmployeeRepo.GetAllAsync();

            }
            else
            {
                employee =await unitOfWork.EmployeeRepo.GetByNameAsync(inputSearch);
            }


            var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employee);

            //view's > Dictionary : Transfer data from Action To View (one ways)

            //1. ViewData : property from controller Class 
            //ViewData["Data01"] = "Hellow world from ViewData";


            //2. ViewBag  :property inherited from controller class, dynamic
            //ViewBag.Data02 = "Hellow world from ViewBag";



            //3. TempData :property inherited from controller class, dynamic
            //Transfer data from request to anther request

            TempData["Data03"] = "Hellow data from tempdata";


            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var department =await unitOfWork.DepartmentRepo.GetAllAsync();
            ViewData["departments"] = department;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {

            // casting EmployeeViewModel (ViewModel) --> Employee (view)
            //Mapping : 
            //1. manual mapping 

            //Employee employee1 = new Employee()
            //{
            //    Id=employee.Id,
            //    Name=employee.Name,
            //    Address=employee.Address,
            //    Salary=employee.Salary,
            //    Age=employee.Age,
            //    HireDate=employee.HireDate,
            //    IsActive=employee.IsActive,
            //    workfor=employee.workfor,
            //    workforId=employee.workforId,
            //    Email=employee.Email,
            //    PhoneNumber=employee.PhoneNumber

            //};


            //2. Auto mapping 

            employee.ImageName = DocumentSetting.UploadFile(employee.Image, "Images");

            var employee1 = _mapper.Map<Employee>(employee);

            var department =await unitOfWork.DepartmentRepo.GetAllAsync();
            ViewData["departments"] = department;
            if (ModelState.IsValid)
            {
                unitOfWork.EmployeeRepo.AddAsync(employee1);
                var count =await unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    TempData["Message"] = "Employee Created ";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Employee not Created";
                }
            }
            return View(employee);
        }
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            var department =await unitOfWork.DepartmentRepo.GetAllAsync();
            ViewData["departments"] = department;
            if (id is null)
            {

                return BadRequest();//400
            }
            var employee =await unitOfWork.EmployeeRepo.GetAsync(id.Value);

            if (employee == null) return NotFound();

            //Mapping : Employee --> EmployeeViewModel

            //EmployeeViewModel employeeView = new EmployeeViewModel()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Address = employee.Address,
            //    Salary = employee.Salary,
            //    Age = employee.Age,
            //    HireDate = employee.HireDate,
            //    IsActive = employee.IsActive,
            //    workfor = employee.workfor,
            //    workforId = employee.workforId,
            //    Email = employee.Email,
            //    PhoneNumber = employee.PhoneNumber

            //};

            var employeeView = _mapper.Map<EmployeeViewModel>(employee);

            return View(viewName, employeeView);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //    if(id is null ) return BadRequest();

            //   var department=departmentRepo.Get(id);
            //    if (department == null) return NotFound();

            //    return View(department);
            return await Details(id, "Edit");
        }


        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employee)
        {

            var department =await unitOfWork.DepartmentRepo.GetAllAsync();
            ViewData["departments"] = department;
            try
            {
                if (id != employee.Id) return BadRequest();

                //Employee employee1 = new Employee()
                //{
                //    Id = employee.Id,
                //    Name = employee.Name,
                //    Address = employee.Address,
                //    Salary = employee.Salary,
                //    Age = employee.Age,
                //    HireDate = employee.HireDate,
                //    IsActive = employee.IsActive,
                //    workfor = employee.workfor,
                //    workforId = employee.workforId,
                //    Email = employee.Email,
                //    PhoneNumber = employee.PhoneNumber

                //};

                if (employee.ImageName is not null)
                {
                    DocumentSetting.DeleteFile(employee.ImageName, "Images");
                }

                if (ModelState.IsValid)
                {
                    employee.ImageName = DocumentSetting.UploadFile(employee.Image, "Images");


                    var employee1 = _mapper.Map<Employee>(employee);


                    unitOfWork.EmployeeRepo.UpdateAsync(employee1);
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



            return View(employee);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            var department =await unitOfWork.DepartmentRepo.GetAllAsync();
            ViewData["departments"] = department;

            if (id == null) return BadRequest();

            var employee =await unitOfWork.EmployeeRepo.GetAsync(id.Value);
            if (employee == null) return NotFound();

            //EmployeeViewModel employeeView = new EmployeeViewModel()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Address = employee.Address,
            //    Salary = employee.Salary,
            //    Age = employee.Age,
            //    HireDate = employee.HireDate,
            //    IsActive = employee.IsActive,
            //    workfor = employee.workfor,
            //    workforId = employee.workforId,
            //    Email = employee.Email,
            //    PhoneNumber = employee.PhoneNumber

            //};

            var employeeView = _mapper.Map<EmployeeViewModel>(employee);

            return View(employeeView);
        }



        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(EmployeeViewModel employee)
        {

            if (employee == null) return NotFound();

            if (ModelState.IsValid)
            {
                //Employee employee1 = new Employee()
                //{
                //    Id = employee.Id,
                //    Name = employee.Name,
                //    Address = employee.Address,
                //    Salary = employee.Salary,
                //    Age = employee.Age,
                //    HireDate = employee.HireDate,
                //    IsActive = employee.IsActive,
                //    workfor = employee.workfor,
                //    workforId = employee.workforId,
                //    Email = employee.Email,
                //    PhoneNumber = employee.PhoneNumber

                //};

                var employee1 = _mapper.Map<Employee>(employee);



                unitOfWork.EmployeeRepo.DeleteAsync(employee1);
                var count =await unitOfWork.CompleteAsync();

                if (employee1.ImageName is not null)
                    DocumentSetting.DeleteFile(employee1.ImageName, "Images");

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }

            }


            return RedirectToAction("Delete");
        }





    }
}

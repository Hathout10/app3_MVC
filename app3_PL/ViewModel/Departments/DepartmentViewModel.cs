using app3.DaL.models;
using System.ComponentModel.DataAnnotations;

namespace app3.PL.ViewModel.Departments
{
    public class DepartmentViewModel :BaseClass
    {

        [Required(ErrorMessage = "Code is Required!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "name is Required!")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

    }
}

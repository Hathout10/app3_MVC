using app3.DaL.models;
using System.ComponentModel.DataAnnotations;

namespace app3.PL.ViewModel.Employee
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Salary is Required")]

        public double Salary { get; set; }
        [Range(25, 60, ErrorMessage = "Age must be between 25 and 60")]
        public int? Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^\d+\s[A-z]+\s[A-z]+,\s[A-z]+\s\d{5}$",
            ErrorMessage = "Please enter a valid address.")]
        public string Address { get; set; }
        [RegularExpression(@"^(\+20|0020|0)?1[0125]\d{8}$",
            ErrorMessage = "Please enter a valid Egyptian phone number.")]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;

        public int? workforId { get; set; }    
        public Department? workfor { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}

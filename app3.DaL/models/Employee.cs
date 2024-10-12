using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app3.DaL.models
{
    public class Employee :BaseClass
    {
        public string Name { get; set; }

        public double Salary { get; set; }
        public int? Age { get; set; }
      
        public string Email { get; set; }
   
        public string Address { get; set; }

        public string?  ImageName { get; set; }

        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime HireDate { get; set; }= DateTime.Now;

        public int? workforId { get; set; }
        public Department? workfor { get; set; }
    }
}

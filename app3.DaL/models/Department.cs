using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app3.DaL.models
{
    public class Department :BaseClass
    {
        public string Code { get; set; }
        public string  Name { get; set; }
        public DateTime DateOfCreation { get; set; }

       

    }
}

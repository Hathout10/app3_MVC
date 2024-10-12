using app3.BLL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app3.DaL.models;
using app3.DaL.Data.Contexts;
using app3.BLL.interfacees;

namespace app3.BLL.Repostry
{
    public class DepartmentRepo : GenericRepo<Department>, IDepartmentRepo
    {

        public DepartmentRepo(AppDbContext dbContext): base(dbContext)//ask clr create object from AppDbContext
        {
        }


     
    }
}

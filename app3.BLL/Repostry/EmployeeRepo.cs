using app3.BLL.interfacees;
using app3.DaL.Data.Contexts;
using app3.DaL.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app3.BLL.Repostry
{
    public class EmployeeRepo : GenericRepo<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(AppDbContext _context) :base(_context)
        {

        }



        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
           return await context.Employees.Where(e => e.Name.ToLower().Contains(name.ToLower())).Include(e=>e.workfor).ToListAsync();
        }


    }
}

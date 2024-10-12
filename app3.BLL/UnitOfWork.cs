using app3.BLL.interfacees;
using app3.BLL.interfaces;
using app3.BLL.Repostry;
using app3.DaL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app3.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext Context;
        private IDepartmentRepo _departmentRepo;
        private IEmployeeRepo _employeeRepo;

        public UnitOfWork(AppDbContext context)
        {
            _employeeRepo= new EmployeeRepo(context);
            _departmentRepo= new DepartmentRepo(context);
            Context = context;
        }


        public IDepartmentRepo DepartmentRepo => _departmentRepo;
        public IEmployeeRepo EmployeeRepo => _employeeRepo;

        public async Task< int> CompleteAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}

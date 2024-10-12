using app3.BLL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app3.BLL.interfacees
{
    public interface IUnitOfWork
    {
        public IDepartmentRepo DepartmentRepo { get; }
        public IEmployeeRepo EmployeeRepo { get; }

        public Task<int> CompleteAsync();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app3.BLL.interfacees;
using app3.DaL.models;

namespace app3.BLL. interfaces
{
    public interface IDepartmentRepo : IGenericRepo<Department>
    {
       //IEnumerable<Department> GetAll();

       // Department Get(int? id);

       // int Add(Department department);
       // int Update(Department department);
       // int Delete(Department department);

    }
}

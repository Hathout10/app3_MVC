using app3.BLL.Repostry;
using app3.DaL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app3.BLL.interfacees
{
    public interface IEmployeeRepo :  IGenericRepo<Employee>
    {

        Task<IEnumerable<Employee>> GetByNameAsync(string name);


        //IEnumerable<Employee> GetAll();
        //Employee GetEmployee(int id);
        //int Add (Employee entity);
        //int  Update(Employee entity);
        //int Delete (Employee entity );  




    }
}

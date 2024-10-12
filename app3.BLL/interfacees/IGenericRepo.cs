using app3.DaL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app3.BLL.interfacees
{
    public interface IGenericRepo<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int? id);

        void AddAsync(T department);
        void UpdateAsync(T department);
        void DeleteAsync(T department);


    }
}

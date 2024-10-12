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
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseClass 
    {
        protected readonly AppDbContext context;

        public GenericRepo(AppDbContext _context )
        {
            context = _context;
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>) await context.Employees.Include(e => e.workfor).AsNoTracking().ToListAsync();
            }
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task< T> GetAsync(int? id)
        {
           

            var result = await context.Set<T>().FindAsync(id);

            if (typeof(T) == typeof(Employee))
            {
                return await context.Employees.Include(e => e.workfor)
                                         .FirstOrDefaultAsync(e => e.Id == id.Value) as T;
            }

            return result;
        }


        public async void AddAsync(T entity)
        {
           await context.Set<T>().AddAsync(entity);
        }

        public async void DeleteAsync(T entity)
        {
            context .Set<T>().Remove(entity);

        }



        public async void UpdateAsync(T entity)
        {
           context.Set<T>().Update(entity);

        }
    }
}

using app3.DaL.models;
using app3.PL.ViewModel.Employee;
using AutoMapper;

namespace app3.PL.Mapping.Employees
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
        }

    }
}

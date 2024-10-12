using app3.DaL.models;
using app3.PL.ViewModel.Departments;
using AutoMapper;

namespace app3.PL.Mapping.Departments
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentViewModel>().ReverseMap();
        }
    }
}

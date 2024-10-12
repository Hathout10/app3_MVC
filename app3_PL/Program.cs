using app3.BLL.interfacees;
using app3.BLL;
using app3.BLL.interfaces;
using app3.BLL.Repostry;
using app3.DaL.Data.Contexts;
using app3.PL.Mapping.Departments;
using app3.PL.Mapping.Employees;
using app3.PL.serviees;
using Microsoft.EntityFrameworkCore;
using app3.DaL.models;
using Microsoft.AspNetCore.Identity;

namespace app3_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
	
			// Add services to the container.
			builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<AppDbContext>()
         

            builder.Services.AddDbContext<AppDbContext>(option=>
            {
                //option.UseSqlServer(builder.Configuration[" ConnectionStrings :DefaultConnection"]);
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddAutoMapper(typeof(EmployeeProfile));
            builder.Services.AddAutoMapper(typeof(DepartmentProfile));
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
					.AddEntityFrameworkStores<AppDbContext>()
					.AddDefaultTokenProviders();


			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // lifeTime
            //builder.Services.AddScoped();     //per request , unreachable object
            //builder.Services.AddSingleton();  //per app
            //builder.Services.AddTransient();  //per operation

            builder.Services.AddScoped<IscopedServices, ScopedServie>();
            builder.Services.AddSingleton<IsingeltonServies, singeltonServies>();
            builder.Services.AddTransient<ItrenseintServes, TransentServices>();


            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
                config.AccessDeniedPath = "/Account/AccessDenied";


            }); 


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

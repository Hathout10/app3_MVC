using app3.PL.serviees;
using app3_MVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace app3_MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IscopedServices scop01;
        private readonly IscopedServices scop02;
        private readonly IsingeltonServies singel01;
        private readonly IsingeltonServies singel02;
        private readonly ItrenseintServes transint01;
        private readonly ItrenseintServes transint02;

        public HomeController(


            ILogger<HomeController> logger,
            IscopedServices scop01,
            IscopedServices scop02,
            IsingeltonServies singel01,
            IsingeltonServies singel02,
            ItrenseintServes transint01,
            ItrenseintServes transint02


            )
        {
            _logger = logger;
            this.scop01 = scop01;
            this.scop02 = scop02;
            this.singel01 = singel01;
            this.singel02 = singel02;
            this.transint01 = transint01;
            this.transint02 = transint02;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"Scop01 :: {scop01.GetGuid()}\n");
            builder.Append($"Scop02 :: {scop02.GetGuid()}\n\n");

            builder.Append($"transent01 :: {transint01.GetGuid()}\n");
            builder.Append($"transent02 :: {transint02.GetGuid()}\n\n");
            
            builder.Append($"singl01 :: {singel01.GetGuid()}\n");
            builder.Append($"singl02 :: {singel02.GetGuid()}\n\n");

            return builder.ToString();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Demo.CSharpScript.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo.CSharpScript.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Demo.CSharpScript";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "QQ:1124999434";

            return View();
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

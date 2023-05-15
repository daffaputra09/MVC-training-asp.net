using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStatisMVC.Models;

namespace WebStatisMVC.Controllers
{
    public class ArticleController : Controller
    {   
        public IActionResult Article()
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

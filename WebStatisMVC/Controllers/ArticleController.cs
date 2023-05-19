using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebStatisMVC.Models;

namespace WebStatisMVC.Controllers
{
    public class ArticleController : Controller
    {
        private readonly MyDbContext _context;
        public ArticleController (MyDbContext dbcontext)
        {
            _context = dbcontext;
        }
        public IActionResult Index()
        {
            var articles = _context.Articles.ToList();
            return View(articles);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articles = await _context.Articles
                .FirstOrDefaultAsync(m => m.id == id);
            if (articles == null)
            {
                return NotFound();
            }

            return View(articles);
        }
    }
}

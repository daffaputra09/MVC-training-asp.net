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
        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Articles articles, IFormFile featuredImage)
        {
            if (ModelState.IsValid)
            {
                if (featuredImage != null && featuredImage.Length > 0)
                {

                    var fileName = Path.GetFileName(featuredImage.FileName);
                    var fileExtension = Path.GetExtension(featuredImage.FileName);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await featuredImage.CopyToAsync(fileStream);
                    }

                    var article = new Articles
                    {
                        title = articles.title,
                        description = articles.description,
                        excerpt = articles.excerpt,
                        featured_image = fileName,
                        publish_date = DateTime.Now,
                        author = articles.author,
                        time_read = articles.time_read,
                        category = articles.category
                    };
                    _context.Articles.Add(article);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index"); 
                }
            }
            return View(articles);
        }

        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var articles = await _context.Articles.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }

            var article = new Articles
            {
                id = articles.id,
                title = articles.title,
                description = articles.description,
                excerpt = articles.excerpt,
                featured_image = articles.featured_image,
                author = articles.author,
                time_read = articles.time_read,
                category = articles.category
            };

            return View(articles);
        }

        //EDIT
        [HttpPost]
        public async Task<IActionResult> Edit(Articles articles, IFormFile featuredImage)
        {
            if (ModelState.IsValid)
            {
                var article = await _context.Articles.FindAsync(articles.id);
                if (article == null)
                {
                    return NotFound();
                }

                if (featuredImage != null && featuredImage.Length > 0)
                {

                    var fileName = Path.GetFileName(featuredImage.FileName);
                    var fileExtension = Path.GetExtension(featuredImage.FileName);

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await featuredImage.CopyToAsync(fileStream);
                    }

                    article.featured_image = fileName;
                }

                article.title = articles.title;
                article.description = articles.description;
                article.excerpt = articles.excerpt;
                article.author = articles.author;
                article.time_read = articles.time_read;
                article.category = articles.category;

                _context.Entry(article).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); 
            }

            return View(articles);
        }
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var artticle = await _context.Articles
                .FirstOrDefaultAsync(m => m.id == id);
            if (artticle == null)
            {
                return NotFound();
            }

            return View(artticle);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articles == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var artticle = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(artticle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return (_context.Articles?.Any(e => e.id == id)).GetValueOrDefault();
        }
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
    }
}

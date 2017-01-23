using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ANCBE.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ANCBE.Controllers
{
    public class PostsController : Controller
    {
        readonly BlogDataContext _dataContext;
        public PostsController(BlogDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            post.PostedDate = DateTime.Now;
            post.Author = User.Identity.Name;

            _dataContext.Posts.Add(post);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Post", new { post.PostedDate.Year, post.PostedDate.Month, post.Key });
        }

        public IActionResult Post(long id)
        {
            Post post = _dataContext.Posts.SingleOrDefault(x => x.Id == id);

            return View(post);
        }

        [Route("posts/{year:int}/{month:int}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            Post post = _dataContext.Posts.SingleOrDefault(
               x => x.PostedDate.Year == year && x.PostedDate.Month == month
                    && x.Key == key.ToLower());

            return View(post);
        }
    }
}
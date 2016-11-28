using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ANCBE.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ANCBE.Controllers
{
    public class PostsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            post.PostedDate = DateTime.Now;
            post.Author = User.Identity.Name;

            BlogDataContext db = new BlogDataContext();
            db.Posts.Add(post);
            await db.SaveChangesAsync();

            return View();
        }

        public IActionResult Post(long id)
        {
            Post post = new Post();

            post.Title = "Incredible Post";
            post.PostedDate = DateTime.Now;
            post.Author = "Evgeniy Osipov";
            post.Body = "A lot of text. This is realy awesome post... with pictures and sounds. Believe me, this is the coolest post of all time ;)";

            return View(post);
        }
    }
}

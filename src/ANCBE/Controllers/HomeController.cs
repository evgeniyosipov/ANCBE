using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ANCBE.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ANCBE.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var posts = new[] {
                new Post {
                    Title = "Blog Post Title #1",
                    PostedDate = DateTime.Now,
                    Author = "Evgeniy Osipov",
                    Body = "Blog post content #1",
                },
                new Post {
                    Title = "Blog Post Title #2",
                    PostedDate = DateTime.Now,
                    Author = "Evgeniy Osipov",
                    Body = "Blog post content #2",
                },
                new Post {
                    Title = "Blog Post Title #3",
                    PostedDate = DateTime.Now,
                    Author = "Evgeniy Osipov",
                    Body = "Blog post content #3",
                }
            };

            return View(posts);
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult CauseAnError()
        {
            throw new Exception("Error!");
        }
    }
}

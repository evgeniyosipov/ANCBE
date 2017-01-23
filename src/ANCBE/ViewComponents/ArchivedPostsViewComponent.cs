using ANCBE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ANCBE.ViewComponents
{
    [ViewComponent]
    public class ArchivedPostsViewComponent : ViewComponent
    {
        private readonly BlogDataContext _db;

        public ArchivedPostsViewComponent(BlogDataContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var archivedPosts = _db.GetArchivedPosts().ToArray();
            return View(archivedPosts);
        }
    }
}

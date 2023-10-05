using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nug.Models;
using NewLife.Data;

namespace Nug.LiteBlog.Controllers
{
    public class HomeController : BaseController
    {
        [Route("{p:int?}")]
        public IActionResult Index(int p = 1)
        {
            ViewBag.PageUrl = "/" + (p + 1);

            return View(Posts.PageView(0, new PageParameter { PageIndex = p, PageSize = 5 }));
        }

        [Route("Catalog/{id}/{p:int?}")]
        public IActionResult Catalog(string id, int p = 1)
        {
            var catalog = Catalogs.FindByUrl(id);

            if (catalog == null)
            {
                return Prompt();
            }

            ViewBag.Position = id;

            var list = Posts.PageView(catalog.ID, new PageParameter { PageIndex = p, PageSize = 5 });

            // 只有一篇文章时直接跳转到详情页
            if(list.Count == 1)
            {
                return View("Post", list.FirstOrDefault());
            }

            ViewBag.PageUrl = id + "/" + (p + 1);
            return View("Index", Posts.PageView(catalog.ID, new PageParameter { PageIndex = p, PageSize = 5 }));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nug.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nug.LiteBlog.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Prepare();
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Prepare();
            return base.OnActionExecutionAsync(context, next);
        }

        public virtual void Prepare()
        {
            ViewBag.Position = "home";

            ViewBag.Catalogs = Catalogs.FindAll().OrderBy( p => p.ID);

            NewLife.Log.XTrace.Log.Info("**请求IP**:" + HttpContext.Connection.RemoteIpAddress.ToString());
        }

        /// <summary>
        /// 友情提示页（待添加一个实体）
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Prompt()
        {
            return View("Error");
        }
    }
}
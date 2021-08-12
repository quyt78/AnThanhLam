using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnThanhLam.Model.Models;
using AnThanhLam.Service;
using AnThanhLam.Web.Models;

namespace AnThanhLam.Web.Controllers
{
    public class PageController : Controller
    {
        IPageService _pageService;
        IPostService _postService;
        public PageController(IPageService pageService, IPostService postService)
        {
            this._pageService = pageService;
            this._postService = postService;
        }
        // GET: Page
        public ActionResult Index(string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var model = Mapper.Map<Page,PageViewModel>(page);
            return View(model);
        }

        public ActionResult GetAboutPage()
        {
            var post = _postService.GetById(1);
            var result = Mapper.Map<Post, PostViewModel>(post);
            return PartialView(result);
        }
    }
}
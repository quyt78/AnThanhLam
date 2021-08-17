using AnThanhLam.Common;
using AnThanhLam.Model.Models;
using AnThanhLam.Service;
using AnThanhLam.Web.Infrastructure.Core;
using AnThanhLam.Web.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnThanhLam.Web.Controllers
{
    public class PostController : Controller
    {
        IPostService _postService;
        IPostCategoryService _postCategoryService;
        
        public PostController(IPostService postService, IPostCategoryService postCategoryService)
        {
            this._postService = postService;
            this._postCategoryService = postCategoryService;
        }
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PostDetail(int id)
        {
            var postItem = _postService.GetById(id);
            var postVm = Mapper.Map<Post, PostViewModel>(postItem);
            postVm.PostCategory = Mapper.Map<PostCategory, PostCategoryViewModel>(_postCategoryService.GetById(postItem.CategoryID));
            return View(postVm);
        }

        [ChildActionOnly]
        public ActionResult PostHome()
        {
            var item = _postService.GetAll().Where(x => x.CategoryID == 2).ToList().Take(3);
            var result = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(item);
            return PartialView(result);
        }

        [ChildActionOnly]
        public ActionResult RelateNews(int postId)
        {
            var postItem = _postService.GetById(postId);
            var lstItems = _postService.GetAll().Where(x => x.Status && x.ID != postId && x.CategoryID == postItem.CategoryID).OrderBy(x => x.CreatedDate).Take(5);
            var result = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(lstItems);
            return PartialView(result);
        }
        
        public ActionResult PostSidebar()
        {
            return PartialView();
        }

        public ActionResult NewPost()
        {
            return PartialView();
        }
               
        public ActionResult Category(int idCategory,  int page = 1)
        {
            string nameCategory = _postCategoryService.GetById(idCategory).Name;
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var postModel = _postService.GetAllByCategoryPaging(idCategory,page, pageSize, out totalRow);
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Title = nameCategory;
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }
    }
}
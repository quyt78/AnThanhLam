using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AnThanhLam.Common;
using AnThanhLam.Model.Models;
using AnThanhLam.Service;
using AnThanhLam.Web.Infrastructure.Core;
using AnThanhLam.Web.Models;

namespace AnThanhLam.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        IBrandService _brandService;
        ISizeService _sizeService;
        public ProductController(IProductService productService
            , IProductCategoryService productCategoryService
            , IBrandService brandService
            , ISizeService sizeService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
            this._brandService = brandService;
            this._sizeService = sizeService;
        }
        // GET: Product
        public ActionResult Detail(int productId)
        {
            var productModel = _productService.GetById(productId);
            var viewModel = Mapper.Map<Product, ProductViewModel>(productModel);

            var relatedProduct = _productService.GetReatedProducts(productId, 6);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(viewModel.MoreImages);
            ViewBag.MoreImages = listImages;

            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetListTagByProductId(productId));
            return View(viewModel);
        }

        public ActionResult Category(int id, int? page , string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            if (!page.HasValue)
            {
                page = 1;
            }
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page.Value, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page.Value,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }
        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Keyword = keyword;
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }
        public ActionResult ListByTag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByTag(tagId, page, pageSize, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Tag = Mapper.Map<Tag,TagViewModel>(_productService.GetTag(tagId));
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }
        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword);
            return Json(new
            {
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllProductCategory()
        {
            var productCategory = _productCategoryService.GetAll();
            var productViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(productCategory);
            return PartialView(productViewModel);
        }

        public ActionResult GetProductCategoryHome()
        {
            var productCategory = _productCategoryService.GetAll().Take(6);
            var productViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(productCategory);
            return PartialView(productViewModel);
        }

        public ActionResult GetAllBrand()
        {
            var brands = _brandService.GetAll();
            var brandViewModel = Mapper.Map<IEnumerable<Brand>, IEnumerable<BrandViewModel>>(brands);
            return PartialView(brandViewModel);
        }

        public ActionResult HomeProduct()
        {
            var items = _productService.GetLastest(4);
            var result = Mapper.Map<IEnumerable<Product>,IEnumerable< ProductViewModel>> (items);
            return PartialView(result);
        }

        public ActionResult GetAllSize()
        {
            var sizes = _sizeService.GetAll();
            var sizeViewModel = Mapper.Map<IEnumerable<Size>, IEnumerable<SizeViewModel>>(sizes);
            return PartialView(sizeViewModel);
        }

        public ActionResult SearchProduct(int? bandId, int? categoryId, string sizeId, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.SearchProductByBrandCateSizePaging(bandId, categoryId, sizeId, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
           
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            ViewBag.bandId = bandId;
            ViewBag.categoryId = categoryId;
            ViewBag.sizeId = sizeId;

            return View(paginationSet);
        }

        public ActionResult GetAllProduct(int page = 1, int? categoryId = null, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.SearchProductByBrandCateSizePaging(null, categoryId, null, page, pageSize, sort, out totalRow);
            var productView = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            var totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productView,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View(paginationSet);
        }

        [ChildActionOnly]
        public ActionResult ProductCategorySideBar()
        {
            var productCategory = _productCategoryService.GetRecusionSets();
            
            return PartialView(productCategory);
            
        }




    }
}
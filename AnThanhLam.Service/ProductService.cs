﻿using System;
using System.Collections.Generic;
using System.Linq;
using AnThanhLam.Common;
using AnThanhLam.Data.Infrastructure;
using AnThanhLam.Data.Repositories;
using AnThanhLam.Model.Models;

namespace AnThanhLam.Service
{
    public interface IProductService
    {
        Product Add(Product Product);

        void Update(Product Product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetAll(string keyword);
        
        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetHotProduct(int top);

        IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetListProduct(string keyword);

        IEnumerable<Product> GetReatedProducts(int id, int top);

        IEnumerable<string> GetListProductByName(string name);

        IEnumerable<Product> SearchProductByBrandCateSizePaging(int? brandId, int? categoryId, string sizeId, int page, int pageSize, string sort, out int totalRow);

        Product GetById(int id);

        void Save();

        IEnumerable<Tag> GetListTagByProductId(int id);

        Tag GetTag(string tagId);

        void IncreaseView(int id);

        IEnumerable<Product> GetListProductByTag(string tagId, int page, int pagesize, out int totalRow);

        bool SellProduct(int productId, int quantity);
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ITagRepository _tagRepository;
        private IProductTagRepository _productTagRepository;
        private ISizeRepository _sizeRepository;
        private IProductSizeRepository _productSizeRepository;

        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IProductTagRepository productTagRepository,
            ITagRepository _tagRepository , ISizeRepository sizeRepository, IProductSizeRepository productSizeRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._productTagRepository = productTagRepository;
            this._tagRepository = _tagRepository;
            this._sizeRepository = sizeRepository;
            this._productSizeRepository = productSizeRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product Product)
        {
            var product = _productRepository.Add(Product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }

            if(!string.IsNullOrEmpty(Product.Sizes))
            {
                string[] sizes = Product.Sizes.Split(',');
                for (var i = 0; i< sizes.Length; i++)
                {
                    var sizeName = StringHelper.ToUnsignString(sizes[i]);
                    if(_sizeRepository.Count(x=>x.ID == sizeName) == 0)
                    {
                        Size size = new Size();
                        size.ID = sizeName;
                        size.Name = sizes[i];
                        size.Type = CommonConstants.ProductSize;
                        size.Status = true;
                        _sizeRepository.Add(size);
                    }
                   
                    ProductSize productSize = new ProductSize();
                    productSize.ProductID = product.ID;
                    productSize.SizeID = sizeName;
                    _productSizeRepository.Add(productSize);

                }
            }

            return product;
        }

        public Product Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product Product)
        {
            _productRepository.Update(Product);
            if (!string.IsNullOrEmpty(Product.Tags))
            {
                string[] tags = Product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == Product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = Product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }

            }

            if (!string.IsNullOrEmpty(Product.Sizes))
            {
                string[] sizes = Product.Sizes.Split(',');
                for (var i = 0; i < sizes.Length; i++)
                {
                    var sizeID = StringHelper.ToUnsignString(sizes[i]);
                    if (_sizeRepository.Count(x => x.ID == sizeID) == 0)
                    {
                        Size size = new Size();
                        size.ID = sizeID;
                        size.Name = sizes[i];
                        size.Type = CommonConstants.ProductSize;
                        size.Status = true;
                        _sizeRepository.Add(size);
                    }
                    _productSizeRepository.DeleteMulti(x => x.ProductID == Product.ID);
                  

                    ProductSize productSize = new ProductSize();
                    productSize.ProductID = Product.ID;
                    productSize.SizeID = sizeID;
                    _productSizeRepository.Add(productSize);

                }
            }
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);

        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public  IEnumerable<Product> SearchProductByBrandCateSizePaging(int? brandId, int? categoryId, string sizeId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status);
            if( brandId != null)
            {
                query = query.Where(x => x.BrandID == brandId);
            }

            if(categoryId != null)
            {
                query = query.Where(x => x.CategoryID == categoryId);
            }

            if(sizeId !=null)
            {
                query = query.Where(x => x.Sizes.Contains(sizeId));
            }

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            } 

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetListProductByName(string name)
        {
            return _productRepository.GetMulti(x => x.Status && x.Name.Contains(name)).Select(y => y.Name);
        }

        public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = _productRepository.GetMulti(x=>x.Name.Contains(keyword));
            }
            

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                case "discount":
                    query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetReatedProducts(int id, int top)
        {
            var product = _productRepository.GetSingleById(id);
            return _productRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Tag> GetListTagByProductId(int id)
        {
            return _productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(y => y.Tag);
        }

        public void IncreaseView(int id)
        {
            var product = _productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
        }

        public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var model = _productRepository.GetListProductByTag(tagId, page, pageSize, out totalRow);
            return model;
        }

        public Tag GetTag(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x => x.ID == tagId);
        }

        //Selling product
        public bool SellProduct(int productId, int quantity)
        {
            var product = _productRepository.GetSingleById(productId);
            if (product.Quantity < quantity)
                return false;
            product.Quantity -= quantity;
            return true;
        }

        public IEnumerable<Product> GetListProduct(string keyword)
        {
            IEnumerable<Product> query;
            if (!string.IsNullOrEmpty(keyword))
                query = _productRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                query = _productRepository.GetAll();
            return query;
        }
    }
}
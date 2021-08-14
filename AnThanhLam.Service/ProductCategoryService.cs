using System;
using System.Collections.Generic;
using System.Linq;
using AnThanhLam.Data.Infrastructure;
using AnThanhLam.Data.Repositories;
using AnThanhLam.Model.Abstract;
using AnThanhLam.Model.Models;

namespace AnThanhLam.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory ProductCategory);

        void Update(ProductCategory ProductCategory);

        ProductCategory Delete(int id);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetAll(string keyword);

        IEnumerable<ProductCategory> GetAllByParentId(int parentId);
        List<RecusionSet<ProductCategory>> GetRecusionSets();

        ProductCategory GetById(int id);
        

        void Save();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _ProductCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository ProductCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._ProductCategoryRepository = ProductCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory ProductCategory)
        {
            return _ProductCategoryRepository.Add(ProductCategory);
        }

        public ProductCategory Delete(int id)
        {
            return _ProductCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _ProductCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _ProductCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _ProductCategoryRepository.GetAll();

        }

        public IEnumerable<ProductCategory> GetAllByParentId(int parentId)
        {
            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public ProductCategory GetById(int id)
        {
            return _ProductCategoryRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory ProductCategory)
        {
            _ProductCategoryRepository.Update(ProductCategory);
        }

        public List<RecusionSet<ProductCategory>> getListCategory()
        {
            List<RecusionSet<ProductCategory>> lstItem = new List<RecusionSet<ProductCategory>>();

            return lstItem;
        }

        public List<RecusionSet<ProductCategory>> GetRecusionSets()
        {
            List<RecusionSet<ProductCategory>> lst = new List<RecusionSet<ProductCategory>>();
            _ProductCategoryRepository.GetMulti(a => a.Status && a.ParentID == null).All(x=> {
                var a = GetRecusions(x.ID, 1);
                if(a != null)
                {
                    lst.Add(a);
                }
                return true;
            });
            return lst;
        }

        public RecusionSet<ProductCategory> GetRecusions(int id, int level)
        {
            RecusionSet<ProductCategory> recusionSet = new RecusionSet<ProductCategory>();

            var item = _ProductCategoryRepository.GetSingleById(id);
            if(item.Status !=false)
            {
                
                recusionSet.ID = item.ID;
                recusionSet.Name = item.Name;
                recusionSet.level = level;
                recusionSet.Order = item.DisplayOrder;
                recusionSet.Alias = item.Alias;
                recusionSet.items = new List<RecusionSet<ProductCategory>>();

                _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == id).All(a =>
                {
                    var model = GetRecusions(a.ID, (level + 1));
                    if(model !=null)
                    {
                        recusionSet.items.Add(model);
                    }
                    return true;
                });
            }

            return recusionSet;
                       
        }
    }
}
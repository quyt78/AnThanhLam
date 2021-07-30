﻿using AnThanhLam.Data.Infrastructure;
using AnThanhLam.Data.Repositories;
using AnThanhLam.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnThanhLam.Service
{
    public interface IBrandService
    {
        Brand Add(Brand brand);

        void Update(Brand brand);

        Brand Delete(int id);

        IEnumerable<Brand> GetAll();

        IEnumerable<Brand> GetAll(string keyword);      

        Brand GetById(int id);

        void Save();
    }

    public class BrandService : IBrandService
    {
        private IBrandRepository _brandRepository;
        private IUnitOfWork _unitOfWork;

        public BrandService(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
        {
            this._brandRepository = brandRepository;
            this._unitOfWork = unitOfWork;
        }

        public Brand Add(Brand brand)
        {
            return _brandRepository.Add(brand);
        }

        public Brand Delete(int id)
        {
            return _brandRepository.Delete(id);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public IEnumerable<Brand> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _brandRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _brandRepository.GetAll();

        }

        public IEnumerable<Brand> GetAllByParentId(int parentId)
        {
            return _brandRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public Brand GetById(int id)
        {
            return _brandRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Brand brand)
        {
            _brandRepository.Update(brand);
        }
    }
}

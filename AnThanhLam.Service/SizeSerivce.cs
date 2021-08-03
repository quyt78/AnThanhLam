using AnThanhLam.Data.Infrastructure;
using AnThanhLam.Data.Repositories;
using AnThanhLam.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnThanhLam.Service
{
    public interface ISizeService
    {
        Size Add(Size size);

        void Update(Size size);

        Size Delete(Size size);

        IEnumerable<Size> GetAll();

        IEnumerable<Size> GetAll(string keyword);

        Size GetById(string id);

        void Save();
    }

    public class SizeService : ISizeService
    {
        private ISizeRepository _sizeRepository;
        private IUnitOfWork _unitOfWork;

        public SizeService(ISizeRepository sizeRepository, IUnitOfWork unitOfWork)
        {
            this._sizeRepository = sizeRepository;
            this._unitOfWork = unitOfWork;
        }

        public Size Add(Size size)
        {
            return _sizeRepository.Add(size);
        }

        public Size Delete(Size size)
        {
            return _sizeRepository.Delete(size);
        }

        public IEnumerable<Size> GetAll()
        {
            return _sizeRepository.GetAll();
        }

        public IEnumerable<Size> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _sizeRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _sizeRepository.GetAll();
        }

        public Size GetById(string id)
        {
            return _sizeRepository.GetSizeById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Size size)
        {
            _sizeRepository.Update(size);
        }
    }
}

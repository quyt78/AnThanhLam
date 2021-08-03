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
    public interface IPartnerService
    {
        Partner Add(Partner partner);

        void Update(Partner partner);

        Partner Delete(int id);

        IEnumerable<Partner> GetAll();

        IEnumerable<Partner> GetAll(string keyword);

        Partner GetById(int id);

        void Save();
    }

    public class PartnerService : IPartnerService
    {
        private IPartnerRepository _partnerRepository;
        private IUnitOfWork _unitOfWork;

        public PartnerService(IPartnerRepository partnerRepository, IUnitOfWork unitOfWork)
        {
            this._partnerRepository = partnerRepository;
            this._unitOfWork = unitOfWork;
        }

        public Partner Add(Partner partner)
        {
            return _partnerRepository.Add(partner);
        }

        public Partner Delete(int id)
        {
            return _partnerRepository.Delete(id);
        }

        public IEnumerable<Partner> GetAll()
        {
            return _partnerRepository.GetAll();
        }

        public IEnumerable<Partner> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _partnerRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _partnerRepository.GetAll();

        }

        public IEnumerable<Partner> GetAllByParentId(int parentId)
        {
            return _partnerRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public Partner GetById(int id)
        {
            return _partnerRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Partner partner)
        {
            _partnerRepository.Update(partner);
        }
    }
}

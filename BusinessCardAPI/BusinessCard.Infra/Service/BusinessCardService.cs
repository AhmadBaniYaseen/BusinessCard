using BusinessCard.Core.Repository;
using BusinessCard.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard.Infra.Service
{
    public class BusinessCardService : IBusinessCardService
    {
        private readonly IBusinessCardRepository _businessCardRepository;

        public BusinessCardService(IBusinessCardRepository businessCardRepository)
        {
            _businessCardRepository = businessCardRepository;
        }

        public void CreateBusinessCard(Core.Data.BusinessCard businessCard)
        {
            _businessCardRepository.CreateBusinessCard(businessCard);
        }

        public void DeleteBusinessCard(int id)
        {
            _businessCardRepository.DeleteBusinessCard(id);
        }

        public List<Core.Data.BusinessCard> GetAllBusinessCard()
        {
           return  _businessCardRepository.GetAllBusinessCard();
        }

        public Core.Data.BusinessCard GetByBusinessCardId(int id)
        {
         return  _businessCardRepository.GetByBusinessCardId(id);
        }

        public Core.Data.BusinessCard GetFilterBusinessCard(string name, DateOnly DateOfBirth, string Phone, string Gender, string Email)
        {
            return _businessCardRepository.GetFilterBusinessCard(name,DateOfBirth,Phone,Gender,Email);
        }

        public void UpdateBusinessCard(Core.Data.BusinessCard businessCard)
        {
           _businessCardRepository.UpdateBusinessCard(businessCard);
        }
    }
}

using BusinessCard.Core.DTO;
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

        public void CreateBusinessCard(CreateBusinessCardInput input)
        {
            _businessCardRepository.CreateBusinessCard(input);
        }

        public void DeleteBusinessCard(DeleteBusinessCard input)
        {
            _businessCardRepository.DeleteBusinessCard(input);
        }

        public async Task< List<Core.Data.BusinessCard>> GetAllBusinessCard()
        {
           return await _businessCardRepository.GetAllBusinessCard();
        }

        public Core.Data.BusinessCard GetByBusinessCardId(GetBusinessCardById input)
        {
         return  _businessCardRepository.GetByBusinessCardId(input);
        }

        public List<BusinessCard.Core.Data.BusinessCard> GetFilterBusinessCard(Filter input)
        {
            return _businessCardRepository.GetFilterBusinessCard(input);
        }

        public void UpdateBusinessCard(UpdateBusinessCard input)
        {
           _businessCardRepository.UpdateBusinessCard(input);
        }
    }
}
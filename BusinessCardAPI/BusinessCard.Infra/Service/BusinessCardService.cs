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

        public async Task CreateBusinessCard(CreateBusinessCardInput input)
        {
            await _businessCardRepository.CreateBusinessCard(input);
        }

        public async Task DeleteBusinessCard(DeleteBusinessCard input)
        {
            await _businessCardRepository.DeleteBusinessCard(input);
        }

        public async Task<List<Core.Data.BusinessCard>> GetAllBusinessCard()
        {
            return await _businessCardRepository.GetAllBusinessCard();
        }

        public async Task<Core.Data.BusinessCard> GetByBusinessCardId(GetBusinessCardById input)
        {
            return await _businessCardRepository.GetByBusinessCardId(input);
        }

        public async Task<List<BusinessCard.Core.Data.BusinessCard>> GetFilterBusinessCard(Filter input)
        {
            return await _businessCardRepository.GetFilterBusinessCard(input);
        }

        public async Task UpdateBusinessCard(UpdateBusinessCard input)
        {
            await _businessCardRepository.UpdateBusinessCard(input);
        }
    }
}
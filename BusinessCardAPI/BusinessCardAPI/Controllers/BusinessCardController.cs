using BusinessCard.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BusinessCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessCardController : ControllerBase
    {
        private readonly IBusinessCardService _businessCardService;

        public BusinessCardController(IBusinessCardService businessCardService)
        {
            _businessCardService = businessCardService;
        }
        [HttpGet]
        public List<BusinessCard.Core.Data.BusinessCard> GetAllBusinessCard()
        {
            return _businessCardService.GetAllBusinessCard();
        }

        [HttpPost]
        public void CreateBusinessCard(BusinessCard.Core.Data.BusinessCard businessCardService)
        {
            _businessCardService.CreateBusinessCard(businessCardService);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public void DeleteBusinessCard(int id)
        {
            _businessCardService.DeleteBusinessCard(id);
        }

        [HttpPut]
        public void UpdateBusinessCard(BusinessCard.Core.Data.BusinessCard businessCardService)
        {
            _businessCardService.UpdateBusinessCard(businessCardService);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public BusinessCard.Core.Data.BusinessCard GetByBusinessCardId(int id)
        {
            return _businessCardService.GetByBusinessCardId(id);
        }

        [HttpGet]
        [Route("GetFilterBusinessCard/{name}/{DateOfBirth}/{Phone}/{Gender}/{Email}")]
        public BusinessCard.Core.Data.BusinessCard GetFilterBusinessCard(string name, DateOnly DateOfBirth, string Phone, string Gender, string Email)
        {
            return _businessCardService.GetFilterBusinessCard(name,  DateOfBirth,  Phone,  Gender,  Email);
        }

    }
}

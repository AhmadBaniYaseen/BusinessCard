using BusinessCard.Core.Data;
using BusinessCard.Core.DTO;
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

        public  BusinessCardController(IBusinessCardService businessCardService)
        {
            _businessCardService = businessCardService;
        }
        [HttpPost]
        [Route("GetAllBusinessCard")]
        [ProducesResponseType(typeof(List<GetALLBusinessCard>), 200)] //عشان اعرف شكل الداتا كيف راجعه 
        public async Task<IActionResult>  GetAllBusinessCard()
        {
            return Ok (await _businessCardService.GetAllBusinessCard());
        }

        [HttpPost]
        [Route("CreateBusinessCard")]
        public void CreateBusinessCard([FromBody] CreateBusinessCardInput input)
        {
            _businessCardService.CreateBusinessCard(input);
        }

        [HttpPost]
        [Route("Delete")]
        public void DeleteBusinessCard([FromBody] DeleteBusinessCard input)
        {
            _businessCardService.DeleteBusinessCard(input);
        }

        [HttpPost]
        [Route("UpdateBusinessCard")]
        public void UpdateBusinessCard([FromBody] UpdateBusinessCard input)
        {
            _businessCardService.UpdateBusinessCard(input);
        }

        [HttpPost] 
        [Route("GetById")]
        public BusinessCard.Core.Data.BusinessCard GetByBusinessCardId([FromForm] GetBusinessCardById input)
        {
            return _businessCardService.GetByBusinessCardId(input);
        }

        [HttpPost]
        [Route("GetFilterBusinessCard")]
        public List<BusinessCard.Core.Data.BusinessCard> GetFilterBusinessCard([FromForm] Filter input)
        {
            return _businessCardService.GetFilterBusinessCard(input);
        }

    }
}

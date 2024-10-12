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

        public BusinessCardController(IBusinessCardService businessCardService)
        {
            _businessCardService = businessCardService;
        }
        [HttpPost]
        [Route("GetAllBusinessCard")]
        [ProducesResponseType(typeof(List<GetALLBusinessCard>), 200)] //عشان اعرف شكل الداتا كيف راجعه 
        public async Task<IActionResult> GetAllBusinessCard()
        {
            return Ok(await _businessCardService.GetAllBusinessCard());
        }

        [HttpPost]
        [Route("CreateBusinessCard")]
        public async Task CreateBusinessCard([FromBody] CreateBusinessCardInput input)
        {
            await _businessCardService.CreateBusinessCard(input);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task DeleteBusinessCard([FromBody] DeleteBusinessCard input)
        {
            await _businessCardService.DeleteBusinessCard(input);
        }

        [HttpPost]
        [Route("UpdateBusinessCard")]
        public async Task UpdateBusinessCard([FromBody] UpdateBusinessCard input)
        {
            await _businessCardService.UpdateBusinessCard(input);
        }

        [HttpPost]
        [Route("GetById")]
        public async Task<BusinessCard.Core.Data.BusinessCard> GetByBusinessCardId([FromForm] GetBusinessCardById input)
        {
            return await _businessCardService.GetByBusinessCardId(input);
        }

        [HttpPost]
        [Route("GetFilterBusinessCard")]
        public async Task<List<BusinessCard.Core.Data.BusinessCard>> GetFilterBusinessCard([FromForm] Filter input)
        {
            return await _businessCardService.GetFilterBusinessCard(input);
        }

    }
}

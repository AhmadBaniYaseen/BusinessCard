using BusinessCard.Core.DTO;
using BusinessCard.Core.Service;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BusinessCardAPI.Controllers
{
    [Route("api/business-card")]
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
        [ProducesResponseType(typeof(List<GetALLBusinessCard>), 200)] 
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
        [Route("DeleteCard")]
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
        public async Task<List<BusinessCard.Core.Data.BusinessCard>> GetFilterBusinessCard([FromBody] Filter input)
        {
            return await _businessCardService.GetFilterBusinessCard(input);
        }

        [HttpPost]
        [Route("UploadBusinessCardFile")]
        public async Task<IActionResult> UploadBusinessCardFile(IFormFile file)
        {
            return Ok( await _businessCardService.UploadBusinessCardFile(file));

        }

        [HttpPost]
        [Route("exportToCsv")] 
        public async Task<IActionResult> ExportCsv([FromBody] BusinessCard.Core.Data.BusinessCard businessCards)
        {
            var result = await _businessCardService.ExportToCsv(businessCards);
            return result;
        }

        [HttpPost]
        [Route("exportToXml")]
        public async Task<IActionResult> ExportXml([FromBody] BusinessCard.Core.Data.BusinessCard businessCards)
        {
            var result = await _businessCardService.ExportToXml(businessCards);
            return result;
        }


    }
}

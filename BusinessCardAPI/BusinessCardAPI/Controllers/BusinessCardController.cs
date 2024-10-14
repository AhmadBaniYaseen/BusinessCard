using BusinessCard.Core.Data;
using BusinessCard.Core.DTO;
using BusinessCard.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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

        [HttpPost]
        [Route("UploadBusinessCardFile")]
        public async Task<IActionResult> UploadBusinessCardFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not uploaded.");
            }

            string extension = Path.GetExtension(file.FileName);
            List<CreateBusinessCardInput> businessCards = new();

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                if (extension == ".csv")
                {
                    businessCards = await ReadCsvFile(stream);
                }
                else if (extension == ".xml")
                {
                    businessCards = await ReadXmlFile(stream);
                }
                else
                {
                    return BadRequest("Unsupported file type. Only CSV and XML are allowed.");
                }
            }


            foreach (var card in businessCards)
            {
                await _businessCardService.CreateBusinessCard(card);
            }

            return Ok("Business cards created successfully.");
        }


        private async Task<List<CreateBusinessCardInput>> ReadXmlFile(StreamReader reader)
        {
            var businessCards = new List<CreateBusinessCardInput>();


            var xmlContent = await reader.ReadToEndAsync();
            var xDoc = XDocument.Parse(xmlContent);


            foreach (var element in xDoc.Descendants("BusinessCard"))
            {
                var businessCard = new CreateBusinessCardInput(
                    Name: element.Element("Name")?.Value,
                    Gender: element.Element("Gender")?.Value,
                    DateOfBirth: DateTime.Parse(element.Element("DateOfBirth")?.Value ?? DateTime.MinValue.ToString()),
                    Email: element.Element("Email")?.Value,
                    Phone: element.Element("Phone")?.Value,
                    Address: element.Element("Address")?.Value,
                    Photo: element.Element("Photo")?.Value
                );

                businessCards.Add(businessCard);
            }

            return businessCards;
        }


        private async Task<List<CreateBusinessCardInput>> ReadCsvFile(StreamReader reader)
        {
            List<CreateBusinessCardInput> businessCards = new();
            string line;


            await reader.ReadLineAsync();

            while ((line = await reader.ReadLineAsync()) != null)
            {
                var values = line.Split(',');

                var businessCard = new CreateBusinessCardInput(
                    Name: values[0],
                    Gender: values[1],
                    DateOfBirth: DateTime.Parse(values[2]),
                    Email: values[3],
                    Phone: values[4],
                    Address: values[5],
                    Photo: values[6]
                );

                businessCards.Add(businessCard);
            }

            return businessCards;
        }

    }
}

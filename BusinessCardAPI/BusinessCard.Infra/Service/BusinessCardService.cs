using BusinessCard.Core.Data;
using BusinessCard.Core.DTO;
using BusinessCard.Core.Repository;
using BusinessCard.Core.Service;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public async Task<CreateBusinessCardInput> UploadBusinessCardFile(IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
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

                }
            }
            return businessCards[0];
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
            try
            {
                using var csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = "," 
                });

                var businessCards = new List<CreateBusinessCardInput>();

                // Read records asynchronously
                await foreach (var record in csvReader.GetRecordsAsync<CreateBusinessCardInput>())
                {
                    businessCards.Add(record);
                }

                return businessCards;
            }
            catch
            {
                return new List<CreateBusinessCardInput>(); 
            }
        }


        public async Task<FileResult> ExportToCsv(BusinessCard.Core.Data.BusinessCard businessCards)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Name,Gender,DateOfBirth,Email,Phone,Address,Photo");

            csvBuilder.AppendLine($"{businessCards.Name},{businessCards.Gender},{businessCards.DateOfBirth?.ToShortDateString() ?? string.Empty},{businessCards.Email},{businessCards.Phone},{businessCards.Address},{businessCards.Photo}");


            var byteArray = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            var stream = new MemoryStream(byteArray);

            return new FileStreamResult(stream, "text/csv")
            {
                FileDownloadName = "businesscards.csv"
            };
        }
        public async Task<FileResult> ExportToXml(BusinessCard.Core.Data.BusinessCard businessCard)
        {
            var xDoc = new XDocument(new XElement("BusinessCard",
                new XElement("Name", businessCard.Name),
                new XElement("Gender", businessCard.Gender),
                new XElement("DateOfBirth", businessCard.DateOfBirth?.ToShortDateString() ?? string.Empty),
                new XElement("Email", businessCard.Email),
                new XElement("Phone", businessCard.Phone),
                new XElement("Address", businessCard.Address),
                new XElement("Photo", businessCard.Photo)
            ));

            var xmlString = xDoc.ToString();
            var byteArray = Encoding.UTF8.GetBytes(xmlString);
            var stream = new MemoryStream(byteArray);

            return new FileStreamResult(stream, "application/xml")
            {
                FileDownloadName = "businesscard.xml"
            };
        }




    }
}
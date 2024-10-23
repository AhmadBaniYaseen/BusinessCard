using BusinessCard.Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard.Core.Service
{
    public interface IBusinessCardService
    {

        Task<List<BusinessCard.Core.Data.BusinessCard>> GetAllBusinessCard();
        Task CreateBusinessCard(CreateBusinessCardInput input);
        Task DeleteBusinessCard(DeleteBusinessCard input);
        Task UpdateBusinessCard(UpdateBusinessCard input);
        Task<BusinessCard.Core.Data.BusinessCard> GetByBusinessCardId(GetBusinessCardById input);
        Task<List<BusinessCard.Core.Data.BusinessCard>> GetFilterBusinessCard(Filter input);
        Task<CreateBusinessCardInput> UploadBusinessCardFile(IFormFile file);
        Task<FileResult> ExportToCsv(BusinessCard.Core.Data.BusinessCard businessCards);
        Task<FileResult> ExportToXml(BusinessCard.Core.Data.BusinessCard businessCards);
    }
}
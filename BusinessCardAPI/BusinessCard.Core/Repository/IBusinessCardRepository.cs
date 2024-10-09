using BusinessCard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessCard.Core.Repository
{
    public interface IBusinessCardRepository 
    {

        List<BusinessCard.Core.Data.BusinessCard> GetAllBusinessCard();
        void CreateBusinessCard(BusinessCard.Core.Data.BusinessCard businessCard);
        void DeleteBusinessCard(int id);
        public void UpdateBusinessCard(BusinessCard.Core.Data.BusinessCard businessCard);
        BusinessCard.Core.Data.BusinessCard GetByBusinessCardId(int id);
        BusinessCard.Core.Data.BusinessCard GetFilterBusinessCard(string name,DateOnly DateOfBirth,string Phone,string Gender,string Email);


    }
}

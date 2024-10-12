﻿using BusinessCard.Core.Data;
using BusinessCard.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessCard.Core.Repository
{
    public interface IBusinessCardRepository 
    {

       Task <List<BusinessCard.Core.Data.BusinessCard>>  GetAllBusinessCard();
        void CreateBusinessCard(CreateBusinessCardInput input);
        void DeleteBusinessCard(DeleteBusinessCard input);
        public void UpdateBusinessCard(UpdateBusinessCard input);
        BusinessCard.Core.Data.BusinessCard GetByBusinessCardId(GetBusinessCardById input);
        List<BusinessCard.Core.Data.BusinessCard> GetFilterBusinessCard(Filter input);


    }
}

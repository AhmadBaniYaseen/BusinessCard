using BusinessCard.Core.Common;
using BusinessCard.Core.Data;
using BusinessCard.Core.DTO;
using BusinessCard.Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard.Infra.Repository
{
    public class BusinessCardRepository : IBusinessCardRepository
    {
        private readonly IDbContext _dbContext;

        public BusinessCardRepository(IDbContext dBContext)
        {
            this._dbContext = dBContext;
        }
        public async Task< List<BusinessCard.Core.Data.BusinessCard>> GetAllBusinessCard()
        {
            var result = await _dbContext.Connection.QueryAsync<BusinessCard.Core.Data.BusinessCard>("GetAllBusinessCard", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        #region :: CreateBusinessCard
        public void CreateBusinessCard(CreateBusinessCardInput input)
        {
            var p = new DynamicParameters();
            p.Add("Name", input.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Gender", input.Gender, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("DateOfBirth", input.DateOfBirth, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("Email", input.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Phone", input.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Address", input.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Photo", input.Phone, dbType: DbType.String, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute("CREATEBusinessCard", p, commandType: CommandType.StoredProcedure);
        }
        #endregion



        public void DeleteBusinessCard(DeleteBusinessCard input)
        {
            var p = new DynamicParameters();
            p.Add("Id", input.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _dbContext.Connection.Execute("DeleteBusinessCard", p, commandType: CommandType.StoredProcedure);
        }

        public void UpdateBusinessCard(UpdateBusinessCard input)
        {
            var p = new DynamicParameters();
            p.Add("Id", input.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Name", input.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Gender", input.Gender, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("DateOfBirth", input.DateOfBirth, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("Email", input.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Phone", input.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Address", input.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Photo", input.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.Execute("UPDATEBusinessCard", p, commandType: CommandType.StoredProcedure);

        }

        public BusinessCard.Core.Data.BusinessCard GetByBusinessCardId(GetBusinessCardById input)
        {
            var p = new DynamicParameters();
            p.Add("id", input.ID , dbType: DbType.Int32, direction: ParameterDirection.Input);
            IEnumerable<BusinessCard.Core.Data.BusinessCard> result = _dbContext.Connection.Query<BusinessCard.Core.Data.BusinessCard>("GetBusinessCardById", p, commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();

        }

        public List<BusinessCard.Core.Data.BusinessCard> GetFilterBusinessCard(Filter input)
        {
            var p = new DynamicParameters();
            p.Add("Name", input.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("DateOfBirth", input.DateOfBirth, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("Phone", input.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Gender", input.Gender, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Email", input.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            IEnumerable<BusinessCard.Core.Data.BusinessCard> result = _dbContext.Connection.Query<BusinessCard.Core.Data.BusinessCard>("FilterBusinessCard", p, commandType: CommandType.StoredProcedure);
            return result.ToList();

        }


    }

}

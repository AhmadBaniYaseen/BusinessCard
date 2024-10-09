using BusinessCard.Core.Common;
using BusinessCard.Core.Data;
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
        public List<BusinessCard.Core.Data.BusinessCard> GetAllBusinessCard()
        {
            var result = _dbContext.Connection.Query<BusinessCard.Core.Data.BusinessCard>("GetAllBusinessCard", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        #region :: CreateBusinessCard
        public void CreateBusinessCard(BusinessCard.Core.Data.BusinessCard businessCard)
        {
            var p = new DynamicParameters();
            p.Add("Name", businessCard.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Gender", businessCard.Gender, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("DateOfBirth", businessCard.DateOfBirth, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("Email", businessCard.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Phone", businessCard.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Address", businessCard.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Photo", businessCard.Phone, dbType: DbType.String, direction: ParameterDirection.Input);

            _dbContext.Connection.Execute("CREATEBusinessCard", p, commandType: CommandType.StoredProcedure);
        }
        #endregion



        public void DeleteBusinessCard(int id)
        {
            var p = new DynamicParameters();
            p.Add("Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            _dbContext.Connection.Execute("DeleteBusinessCard", p, commandType: CommandType.StoredProcedure);
        }

        public void UpdateBusinessCard(BusinessCard.Core.Data.BusinessCard businessCard)
        {
            var p = new DynamicParameters();
            p.Add("Id", businessCard.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("Name", businessCard.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Gender", businessCard.Gender, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("DateOfBirth", businessCard.DateOfBirth, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("Email", businessCard.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Phone", businessCard.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Address", businessCard.Address, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Photo", businessCard.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            _dbContext.Connection.Execute("UPDATEBusinessCard", p, commandType: CommandType.StoredProcedure);

        }

        public BusinessCard.Core.Data.BusinessCard GetByBusinessCardId(int id)
        {
            var p = new DynamicParameters();
            p.Add("id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            IEnumerable<BusinessCard.Core.Data.BusinessCard> result = _dbContext.Connection.Query<BusinessCard.Core.Data.BusinessCard>("GetBusinessCardById", p, commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();

        }

        public BusinessCard.Core.Data.BusinessCard GetFilterBusinessCard(string name, DateOnly DateOfBirth, string Phone, string Gender, string Email)
        {
            var p = new DynamicParameters();
            p.Add("Name", name, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("DateOfBirth", DateOfBirth, dbType: DbType.Date, direction: ParameterDirection.Input);
            p.Add("Phone", Phone, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Gender", Gender, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("Email", Email, dbType: DbType.String, direction: ParameterDirection.Input);
            IEnumerable<BusinessCard.Core.Data.BusinessCard> result = _dbContext.Connection.Query<BusinessCard.Core.Data.BusinessCard>("User_Package.GetUserById", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();

        }


    }

}

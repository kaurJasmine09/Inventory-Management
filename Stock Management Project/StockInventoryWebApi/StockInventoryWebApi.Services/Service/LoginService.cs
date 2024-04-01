using StockInventoryWebApi.BusinessModel.DataTransferModel;
using StockInventoryWebApi.Entities.GenericRepo;
using StockInventoryWebApi.Services.IService;
using StockInventoryWebApi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockInventoryWebApi.Services.Service
{
    public class LoginService : ILoginService
    {
        #region Private Member Variables
        /// <summary>
        /// Initialise generic data context variable.
        /// </summary>
        private readonly GenericUnitOfWork<ClothingStock_DBContext> _unitOfWork;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the dependencies of services
        /// </summary>
        /// <param name="unitOfWork">unit of work for repository</param>
        public LoginService(GenericUnitOfWork<ClothingStock_DBContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Public methods
        public EmployeeInfoDto AuthenticateUser(int employeeId, int pin)
        {
            EmployeeInfoDto user = null;

            if(employeeId > 0 && pin > 0)
            {
                user = _unitOfWork.Context.SystemUser.Where(x => x.EmployeeId == employeeId && x.Pin == pin)
                    .Select(x => new EmployeeInfoDto
                    {
                      EmployeeId = x.EmployeeId,
                      Email = x.Email,
                      FirstName = x.Fname,
                      LastName = x.Lname,
                      Phone = x.Phone,
                      Pin =x.Pin,
                      Role =x.Role
                    }).FirstOrDefault();
            }

            return user;
        }

        #endregion
    }
}

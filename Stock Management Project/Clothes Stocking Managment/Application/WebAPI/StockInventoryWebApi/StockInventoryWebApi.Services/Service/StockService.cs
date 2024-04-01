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
    public class StockService : IStockService
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
        public StockService(GenericUnitOfWork<ClothingStock_DBContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public TransactionResponse AddStockRecord(UserStockInOutProductDto userStock)
        {
            bool isSuccess = false;
            string transactionId = String.Empty;
            try
            {
                if (userStock != null)
                {
                    //Validate add stock request
                    switch (userStock.Type)
                    {
                        //Stock In: SupplierId must exists in database. CustomerId must be null. Quantity > 0
                        case "IN":
                            var supplier = _unitOfWork.Repository<Supplier>().GetById(userStock.SupplierId);
                            if (supplier == null || userStock.CustomerId != null || userStock.Quantity <= 0)
                                return new TransactionResponse { TransctionId = transactionId, IsSuccess = isSuccess } ;
                            break;
                        //Stock Out: SupplierId must be null. CustomerId must exists in database. Quantity < 0
                        case "OUT":
                            var customer = _unitOfWork.Repository<Customer>().GetById(userStock.CustomerId);
                            if (customer == null || userStock.SupplierId != null || userStock.Quantity >= 0)
                                return new TransactionResponse { TransctionId = transactionId, IsSuccess = isSuccess };
                            break;
                        //Adjustment: Both SupplierId and CustomerId must be null. Quantity != 0
                        case "ADJUST":
                            if (userStock.CustomerId != null || userStock.SupplierId != null || userStock.Quantity == 0)
                                return new TransactionResponse { TransctionId = transactionId, IsSuccess = isSuccess };
                            break;
                        //Refuse add stock if type is incorrect
                        default:
                            return new TransactionResponse { TransctionId = transactionId, IsSuccess = isSuccess };
                    }

                    var transactionIdGen = Guid.NewGuid();
                    UserStockInOutProduct userStockInOutProduct = new UserStockInOutProduct
                    {
                        TransactionId =transactionIdGen.ToString(),
                        Comments = userStock.Comments,
                        Date = userStock.Date.UtcDateTime,
                        EmployeeId = userStock.EmployeeId,
                        Quantity = userStock.Quantity,
                        ProductId = userStock.ProductId,
                        Type = userStock.Type,
                        CustomerId = userStock.CustomerId,
                        SupplierId = userStock.SupplierId
                    };

                    _unitOfWork.Repository<UserStockInOutProduct>().Insert(userStockInOutProduct);
                    var product = _unitOfWork.Repository<Product>().GetById(userStockInOutProduct.ProductId);
                    product.Quantity += userStockInOutProduct.Quantity;
                    _unitOfWork.Repository<Product>().Update(product);
                    _unitOfWork.SaveChanges();
                    isSuccess = true;
                    transactionId = transactionIdGen.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isSuccess = false;
            }

            return new TransactionResponse { TransctionId = transactionId, IsSuccess = isSuccess };
        }

        public bool DeleteStockRecord(int empId, int prodId, int transNum)
        {
            bool isSuccess = false;
            try
            {
                if (empId > 0 && prodId > 0 && transNum > 0)
                {
                    UserStockInOutProduct stock = _unitOfWork.Context.UserStockInOutProduct
                                                .Where(x => x.EmployeeId == empId && x.ProductId == prodId && x.TransactionNumber == transNum)
                                                .FirstOrDefault();
                    if (stock != null)
                    {
                        _unitOfWork.Repository<Web.Models.UserStockInOutProduct>().Delete(stock);
                        _unitOfWork.SaveChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception )
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public List<dynamic> GetAllStocks()
        {
            List<dynamic> UserStockInOutProduct = _unitOfWork.Context.UserStockInOutProduct
                              .Select(x => new
                              {
                                  Comments = x.Comments,
                                  Date = new DateTimeOffset(x.Date, TimeSpan.Zero),
                                  EmployeeName = x.Employee.Fname,
                                  Quantity = x.Quantity,
                                  ProductName = x.Product.ProductName,
                                  TransactionNumber = x.TransactionNumber,
                                  TransactionId = x.TransactionId,
                                  Type = x.Type,
                                  SupplierName = x.Supplier.SupplierName,
                                  CustomerName = x.Customer.CustomerName,
                              }).ToList<dynamic>();

            return UserStockInOutProduct;
        }

        public UserStockInOutProductDto GetStockById(int empId, int prodId, int transNum)
        {
            UserStockInOutProductDto stock = null;
            if (empId > 0 && prodId > 0 && transNum > 0)
            {
                 stock = _unitOfWork.Context.UserStockInOutProduct
                         .Where(x => x.EmployeeId == empId && x.ProductId == prodId && x.TransactionNumber == transNum)
                         .Select(x => new UserStockInOutProductDto
                         {
                             Comments = x.Comments,
                             Date = x.Date,
                             EmployeeId = x.EmployeeId,
                             Quantity = x.Quantity,
                             ProductId = x.ProductId,
                             TransactionNumber = x.TransactionNumber,
                             TransactionId = x.TransactionId,
                             Type = x.Type,
                             CustomerId = x.CustomerId,
                             SupplierId =x.SupplierId
                         }).FirstOrDefault();
            }

            return stock;
        }

        public bool UpdateStockRecord(UserStockInOutProductDto userStock)
        {
            bool isSuccess = false;
            try
            {
                if (userStock != null)
                {
                    UserStockInOutProduct stock = _unitOfWork.Context.UserStockInOutProduct
                                                .Where(x => x.EmployeeId == userStock.EmployeeId && x.ProductId == userStock.ProductId && x.TransactionNumber == userStock.TransactionNumber)
                                                .FirstOrDefault();
        
                    if (stock != null)
                    {
                        if (userStock.Quantity != stock.Quantity)
                            stock.Quantity = userStock.Quantity;
                        if (userStock.Date != stock.Date)
                            stock.Date = userStock.Date.UtcDateTime;
                        if (userStock.TransactionNumber != stock.TransactionNumber)
                            stock.TransactionNumber = userStock.TransactionNumber;
                        if (userStock.Comments != stock.Comments)
                            stock.Comments = userStock.Comments ?? stock.Comments;
                        if (userStock.Type != stock.Type && !String.IsNullOrWhiteSpace(userStock.Type))
                            stock.Type = userStock.Type;

                        _unitOfWork.Repository<UserStockInOutProduct>().Update(stock);
                        _unitOfWork.SaveChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}

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
    public class CustomerService : ICustomerService
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
        public CustomerService(GenericUnitOfWork<ClothingStock_DBContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public bool AddCustomer(CustomerDto customer)
        {
            bool isSuccess = false;
            try
            {
                if (customer != null)
                {
                    Customer cust = new Customer
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = customer.CustomerName,
                        Email = customer.Email,
                        Location = customer.Location,
                        Phone = customer.Phone,
                        //ProductId = customer.ProductId
                    };

                    _unitOfWork.Repository<Customer>().Insert(cust);
                    _unitOfWork.SaveChanges();
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                isSuccess = false;
            }

            return isSuccess;
        }

        public bool DeleteCustomer(int custId)
        {
            bool isSuccess = false;
            try
            {
                if (custId > 0)
                {
                    Web.Models.Customer cust = _unitOfWork.Context.Customer.Where(x => x.CustomerId == custId).FirstOrDefault();
                    if(cust != null)
                    {
                        _unitOfWork.Repository<Web.Models.Customer>().Delete(cust);
                        _unitOfWork.SaveChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        public List<CustomerDto> GetAllCustomers()
        {
            List<CustomerDto> customerDto = _unitOfWork.Context.Customer
                                          .Select(x => new CustomerDto
                                          {
                                              CustomerId = x.CustomerId,
                                              CustomerName = x.CustomerName,
                                              Email = x.Email,
                                              Location = x.Location,
                                              Phone = x.Phone,
                                              //ProductId = x.ProductId
                                          }).ToList();

            return customerDto;
        }

        public CustomerDto GetCustomerById(int custId)
        {
            CustomerDto customerDto = null;
            if (custId > 0)
            {
                customerDto = _unitOfWork.Context.Customer.Where(x => x.CustomerId == custId)
                                          .Select(x => new CustomerDto
                                          {
                                              CustomerId = x.CustomerId,
                                              CustomerName = x.CustomerName,
                                              Email = x.Email,
                                              Location = x.Location,
                                              Phone = x.Phone,
                                              //ProductId = x.ProductId
                                          }).FirstOrDefault();
            }

            return customerDto;
        }

        public bool UpdateCustomer(CustomerDto customer)
        {
            bool isSuccess = false;
            try
            {
                if (customer != null)
                {
                    Customer cust = _unitOfWork.Context.Customer
                                                            .Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
                    if (cust != null)
                    {
                        if (customer.Email != cust.Email)
                            cust.Email = customer.Email ?? cust.Email;
                        if (customer.CustomerId != cust.CustomerId)
                            cust.CustomerId = customer.CustomerId;
                        if (customer.CustomerName != cust.CustomerName)
                            cust.CustomerName = customer.CustomerName ?? cust.CustomerName;
                        if (customer.Phone != cust.Phone)
                            cust.Phone = customer.Phone ?? cust.Phone;
                        if (customer.Location != cust.Location && !String.IsNullOrWhiteSpace(customer.Location))
                            cust.Location = customer.Location;

                        _unitOfWork.Repository<Customer>().Update(cust);
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

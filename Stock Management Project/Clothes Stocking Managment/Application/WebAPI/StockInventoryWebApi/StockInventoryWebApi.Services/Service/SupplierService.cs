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
    public class SupplierService : ISupplierService
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
        public SupplierService(GenericUnitOfWork<ClothingStock_DBContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion
        public bool AddSupplier(SupplierDto supplier)
        {
            bool isSuccess = false;
            try
            {
                if (supplier != null)
                {
                    Supplier sup = new Supplier
                    {
                        SupplierId = supplier.SupplierId,
                        SupplierName = supplier.SupplierName,
                        Email = supplier.Email,
                        Phone = supplier.Phone,
                    };

                    _unitOfWork.Repository<Supplier>().Insert(sup);
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

        public bool DeleteSupplier(int supplierId)
        {
            bool isSuccess = false;
            try
            {
                if (supplierId > 0)
                {
                    Supplier sup = _unitOfWork.Context.Supplier.Where(x => x.SupplierId == supplierId).FirstOrDefault();
                    if (sup != null)
                    {
                        _unitOfWork.Repository<Web.Models.Supplier>().Delete(sup);
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

        public List<SupplierDto> GetAllSupplier()
        {
            List<SupplierDto> supDto = _unitOfWork.Context.Supplier
                                          .Select(x => new SupplierDto
                                          {
                                              SupplierId = x.SupplierId,
                                              SupplierName = x.SupplierName,
                                              Email = x.Email,
                                              Phone = x.Phone,
                                          }).ToList();

            return supDto;
        }

        public SupplierDto GetSupplierById(int supplierId)
        {
            SupplierDto supDto = null;
            if (supplierId > 0)
            {
                supDto = _unitOfWork.Context.Supplier.Where(x => x.SupplierId == supplierId)
                                          .Select(x => new SupplierDto
                                          {
                                              SupplierId = x.SupplierId,
                                              SupplierName = x.SupplierName,
                                              Email = x.Email,
                                              Phone = x.Phone,
                                          }).FirstOrDefault();
            }

            return supDto;
        }

        public bool UpdateSupplier(SupplierDto supplier)
        {
            bool isSuccess = false;
            try
            {
                if (supplier != null)
                {
                    Supplier sup = _unitOfWork.Context.Supplier
                                                            .Where(x => x.SupplierId == supplier.SupplierId).FirstOrDefault();
                    if (sup != null)
                    {
                        if (supplier.Email != sup.Email)
                            sup.Email = supplier.Email ?? sup.Email;
                        if (supplier.SupplierId != sup.SupplierId)
                            sup.SupplierId = supplier.SupplierId;
                        if (supplier.SupplierName != sup.SupplierName)
                            sup.SupplierName = supplier.SupplierName ?? sup.SupplierName;
                        if (supplier.Phone != sup.Phone)
                            sup.Phone = supplier.Phone ?? sup.Phone;

                        _unitOfWork.Repository<Supplier>().Update(sup);
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

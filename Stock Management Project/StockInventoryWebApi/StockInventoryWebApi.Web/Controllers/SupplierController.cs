using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockInventoryWebApi.BusinessModel.DataTransferModel;
using StockInventoryWebApi.BusinessModel.Helper;
using StockInventoryWebApi.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockInventoryWebApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class SupplierController : ControllerBase
    {
        #region Private Member Variables


        private readonly ISupplierService _supplierService;

        #endregion

        #region Constructors


        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService ?? throw new System.ArgumentNullException(nameof(supplierService));
        }

        #endregion

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<SupplierDto> GetAllSupplierDetails()
        {
            ActionResult response;
            List<SupplierDto> prodDetail = _supplierService.GetAllSupplier();
            if (prodDetail == null || !prodDetail.Any())
            {
                response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, $"There is no record in database.")));
            }
            else
            {
                response = Ok(new ResponseMessage(true, prodDetail, new Message(HttpStatusCode.OK)));
            }

            return response;
        }


        [Route("{id}")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<SupplierDto> GetSupplierDetails(int id)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (id <= 0)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "Please enter valid supplier id."));
                response = BadRequest(responseMessage);
            }
            else
            {
                SupplierDto supDetail = _supplierService.GetSupplierById(id);
                if (supDetail == null)
                {
                    response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, $"There is no record in database against supplier id {id}")));
                }
                else
                {
                    response = Ok(new ResponseMessage(true, supDetail, new Message(HttpStatusCode.OK)));
                }
            }

            return response;
        }


        [Route("supplierDetail")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<SupplierDto> AddSupplierDetails([FromBody] SupplierDto supplier)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (!ModelState.IsValid)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "There is invalid entry in supplier record."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _supplierService.AddSupplier(supplier);
                if (!isSuccess)
                {
                    response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, "No record is add in database.")));
                }
                else
                {
                    response = Ok(new ResponseMessage(true, isSuccess, new Message(HttpStatusCode.OK)));
                }
            }

            return response;
        }

        [Route("supplierDetail")]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<SupplierDto> UpdateSupplierDetails([FromBody] SupplierDto product)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (!ModelState.IsValid)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "There is invalid entry in supplier record."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _supplierService.UpdateSupplier(product);
                if (!isSuccess)
                {
                    response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, "No record is updated in database.")));
                }
                else
                {
                    response = Ok(new ResponseMessage(true, isSuccess, new Message(HttpStatusCode.OK, "Updated! Your record has been updated. success")));
                }
            }

            return response;
        }


        [Route("{id}")]
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<SupplierDto> DeleteSupplierDetails(int id)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (id <= 0)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "Please enter valid supplier id."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _supplierService.DeleteSupplier(id);
                if (!isSuccess)
                {
                    response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, "No record is deleted from database")));
                }
                else
                {
                    response = Ok(new ResponseMessage(true, isSuccess, new Message(HttpStatusCode.OK, "Deleted! Your record has been deleted. success")));
                }
            }

            return response;
        }
    }
}

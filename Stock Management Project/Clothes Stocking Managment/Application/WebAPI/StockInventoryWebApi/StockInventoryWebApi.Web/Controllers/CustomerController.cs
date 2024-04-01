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
    public class CustomerController : ControllerBase
    {
        #region Private Member Variables

        
        private readonly ICustomerService _customerService;

        #endregion

        #region Constructors

       
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new System.ArgumentNullException(nameof(customerService));
        }

        #endregion

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<CustomerDto> GetAllCustomerDetails()
        {
            ActionResult response;
            List<CustomerDto> custDetail = _customerService.GetAllCustomers();
            if (custDetail == null || !custDetail.Any())
            {
                response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, $"There is no record in database.")));
            }
            else
            {
                response = Ok(new ResponseMessage(true, custDetail, new Message(HttpStatusCode.OK)));
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
        public ActionResult<CustomerDto> GetCustomerDetails(int id)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (id <= 0)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "Please enter valid customer id."));
                response = BadRequest(responseMessage);
            }
            else
            {
                CustomerDto custDetail = _customerService.GetCustomerById(id);
                if (custDetail == null)
                {
                    response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, $"There is no record in database against customer id {id}")));
                }
                else
                {
                    response = Ok(new ResponseMessage(true, custDetail, new Message(HttpStatusCode.OK)));
                }
            }

            return response;
        }


        [Route("customerDetail")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<CustomerDto> AddCustomerDetails([FromBody] CustomerDto customer)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (!ModelState.IsValid)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "There is invalid entry in customer record."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _customerService.AddCustomer(customer);
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

        [Route("customerDetail")]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<CustomerDto> UpdateCustomerDetails([FromBody] CustomerDto customer)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (!ModelState.IsValid)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "There is invalid entry in customer record."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _customerService.UpdateCustomer(customer);
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
        public ActionResult<CustomerDto> DeleteCustomerDetails(int id)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (id <= 0)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "Please enter valid customer id."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _customerService.DeleteCustomer(id);
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

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
    public class StockController : ControllerBase
    {
        #region Private Member Variables


        private readonly IStockService _stockService;

        #endregion

        #region Constructors


        public StockController(IStockService stockService)
        {
            _stockService = stockService ?? throw new System.ArgumentNullException(nameof(stockService));
        }

        #endregion

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<UserStockInOutProductDto> GetAllStockDetails()
        {
            ActionResult response;
            List<UserStockInOutProductDto> stockDetail = _stockService.GetAllStocks();
            if (stockDetail == null || !stockDetail.Any())
            {
                response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, $"There is no record in database.")));
            }
            else
            {
                response = Ok(new ResponseMessage(true, stockDetail, new Message(HttpStatusCode.OK)));
            }

            return response;
        }


        [Route("{empId}/{prodId}/{transNum}")]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<UserStockInOutProductDto> GetStockDetails(int empId, int prodId, int transNum)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (empId <= 0 || prodId <=0 || transNum <= 0)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "Please enter valid ids."));
                response = BadRequest(responseMessage);
            }
            else
            {
                UserStockInOutProductDto stockDetail = _stockService.GetStockById(empId, prodId, transNum);
                if (stockDetail == null)
                {
                    response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, $"There is no record about stock in database.")));
                }
                else
                {
                    response = Ok(new ResponseMessage(true, stockDetail, new Message(HttpStatusCode.OK)));
                }
            }

            return response;
        }


        [Route("stockDetail")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<UserStockInOutProductDto> AddStockDetails([FromBody] UserStockInOutProductDto stock)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (!ModelState.IsValid)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "There is invalid entry in product record."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _stockService.AddStockRecord(stock);
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

        [Route("stockDetail")]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<UserStockInOutProductDto> UpdateStockDetails([FromBody] UserStockInOutProductDto stock)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (!ModelState.IsValid)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "There is invalid entry about stock."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _stockService.UpdateStockRecord(stock);
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


        [Route("{empId}/{prodId}/{transNum}")]
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<ProductDto> DeleteStockDetails(int empId, int prodId, int transNum)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (empId <= 0 || prodId <= 0 || transNum <= 0)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "Please enter valid ids."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _stockService.DeleteStockRecord(empId, prodId, transNum);
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

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
    public class ProductController : ControllerBase
    {
        #region Private Member Variables


        private readonly IProductService _productService;

        #endregion

        #region Constructors


        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new System.ArgumentNullException(nameof(productService));
        }

        #endregion

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<ProductDto> GetAllProductDetails()
        {
            ActionResult response;
            List<ProductDto> prodDetail = _productService.GetAllProduct();
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
        public ActionResult<ProductDto> GetProductDetails(int id)
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
                ProductDto prodDetail = _productService.GetProductById(id);
                if (prodDetail == null)
                {
                    response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, $"There is no record in database against product id {id}")));
                }
                else
                {
                    response = Ok(new ResponseMessage(true, prodDetail, new Message(HttpStatusCode.OK)));
                }
            }

            return response;
        }


        [Route("productDetail")]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<ProductDto> AddProductDetails([FromBody] ProductDto product)
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
                bool isSuccess = _productService.AddProduct(product);
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

        [Route("productDetail")]
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<ProductDto> UpdateProductDetails([FromBody] ProductDto product)
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
                bool isSuccess = _productService.UpdateProduct(product);
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
        public ActionResult<ProductDto> DeleteProductDetails(int id)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (id <= 0)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "Please enter valid product id."));
                response = BadRequest(responseMessage);
            }
            else
            {
                bool isSuccess = _productService.DeleteProduct(id);
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

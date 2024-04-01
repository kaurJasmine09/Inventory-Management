using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StockInventoryWebApi.BusinessModel.DataTransferModel;
using StockInventoryWebApi.BusinessModel.Helper;
using StockInventoryWebApi.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockInventoryWebApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class LoginController : ControllerBase
    {
        #region Private Member Variables

        /// <summary>
        /// Login service interface variable.
        /// </summary>
        private readonly ILoginService _loginService;

        #endregion
        /// <summary>
        /// Constructor of Login controller.
        /// </summary>
        /// <param name="loginService">Variable of Login service interface type</param>
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        // GET: api/<LoginController>
        [HttpGet, Route("authenticate")]
        [EnableCors("AllowOrigin")]
        [ProducesResponseType(typeof(ResponseMessage), 200)]
        [ProducesResponseType(typeof(ResponseMessage), 400)]
        [ProducesResponseType(typeof(ResponseMessage), 401)]
        [ProducesResponseType(typeof(ResponseMessage), 404)]
        public ActionResult<EmployeeInfoDto> Login(int employeeId, int pin)
        {
            ActionResult response;
            ResponseMessage responseMessage;
            if (employeeId < 0 || pin < 0)
            {
                responseMessage = new ResponseMessage(false, null, new Message(HttpStatusCode.BadRequest, "Please enter valid employee id and pin."));
                response = BadRequest(responseMessage);
            }
            else
            {
                var authUser = _loginService.AuthenticateUser(employeeId, pin);
                if (authUser == null)
                {
                    response = NotFound(new ResponseMessage(false, null, new Message(HttpStatusCode.NotFound, $"There is no record in database against employee id {employeeId}")));
                }
                else
                {
                    response = Ok(new ResponseMessage(true, authUser, new Message(HttpStatusCode.OK)));
                }
            }

            return response;
           
        }
    }
}

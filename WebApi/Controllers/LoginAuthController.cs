using TachTechnologies.DataAccessLayer;
using TachTechnologies.DataAccessLayer.BusinessUtil;
using TachTechnologies.DataAccessLayer.Repository;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LoginAuthController : ApiController
    {
        //   IRegisterUser repository;
        //public RegisterUserController()
        //{
        //    repository = new RegisterUserConcrete();
        //}
        //// GET: RegisterUser/Create
        //public ActionResult Create()
        //{
        //    return View(new RegisterUser());
        //}

        [HttpPost]
        [ActionName("Login")]
        [Route("Login/Auth")]
        public HttpResponseMessage Login([FromBody] User user)
        {
            try
            {
                if (UserRepository.IsValid(user))
                {
                    UserRepository obj = new UserRepository();
                    string URLParams = JsonConvert.SerializeObject(user);
                    user.URLParams = URLParams;
                    ResponseType Response = obj.UpdateUser(user);
                    if (Response.ErrorMsg == "success")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, Response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, Response);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                      new ResponseType() { ErrorMsg = "UserName,PasswordHash and UId are Required!!", AccessToken = "" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                         new ResponseType() { ErrorMsg = "User Object Required", AccessToken = "" });
            }
        }

        [HttpPost]
        [ActionName("AuthToken")]
        [Route("Login/AccessTokenAuth")]
        public HttpResponseMessage AuthToken([FromBody] User user)
        {
            try
            {
                UserRepository obj = new UserRepository();
                TokenResponseType Response = obj.ValidateUserToken(user);
                if (Response.ErrorMsg == "success")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Response);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, Response);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                         new TokenResponseType() { ErrorMsg = "Access Token Required", PasswordHash = "", UserName = "" });
            }
        }
    }
}
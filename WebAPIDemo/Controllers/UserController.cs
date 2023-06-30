using BusinessEntities;
using BusinessServices.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using WebAPIDemo.Filters;

namespace WebAPIDemo.Controllers
{
    [ApiAuthenticationFilter]
    public class UserController : ApiController
    {
        private readonly IUserInterface _userInterface;
        private readonly IEmailInterface _IEmailInterface;
        // Public constructor to initialize product service instance
        public UserController(IUserInterface userInterface, IEmailInterface iEmailInterface)
        {
            _userInterface = userInterface;
            _IEmailInterface = iEmailInterface;
        }
        // GET api/User
        public HttpResponseMessage Get()
        {

            try
            {

                FunctionResponse Resp = _userInterface.GetAllUsers();

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    IEnumerable<UserEntity> TranslationList = (IEnumerable<UserEntity>)Resp.Data[0];
                    return Request.CreateResponse(HttpStatusCode.OK, TranslationList);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        // GET api/User/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(Guid id)
        {
            var user = _userInterface.GetUserById(id);
            if (user != null)
                return Request.CreateResponse(HttpStatusCode.OK, user);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user found for this id");
        }
      
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] UserEntity userEntity)
        {

            try
            {
                //Create and Assign Role to User
                FunctionResponse Resp = _userInterface.CreateUser(userEntity);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    //Send Verification Mail To User
                    Resp = _userInterface.SendVerificationMail(userEntity, Resp, _IEmailInterface);
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                //  Console.WriteLine(ex.ToString());
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }


        }

        [HttpPost]
        [Route("api/User/SubmitBulkUser")]
        public HttpResponseMessage Post(List<UserEntity> userEntity)
        {
            try
            {
                FunctionResponse Resp = _userInterface.CreateBulkUser(userEntity);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else if (Resp.Status == FunctionResponse.StatusType.WARNING)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        // PUT api/User/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userEntity"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(Guid id, UserEntity userEntity)
        {
            try
            {
                FunctionResponse Resp = _userInterface.UpdateUser(id, userEntity, userEntity.IsActive);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        // DELETE api/User/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(Guid id)
        {

            try
            {
                FunctionResponse Resp = _userInterface.DeleteUser(id);

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        /// <summary>
        /// Check User exists 
        /// </summary>
        /// <param name="searchform">UserName and Pwd</param>
        /// <returns></returns>

        [HttpPost]
        [Route("api/user/CheckUser")]
        public HttpResponseMessage GetUser(UserEntity U)
        {
            try
            {
                FunctionResponse Resp = null;

                //string userName = (string)searchform.UserName;
                //string password = (string)searchform.Pwd.Trim(); 
                string userName = U.UserName.Trim();
                string password = U.Pwd.Trim();



                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    Resp = _userInterface.GetUser(userName, password);
                }

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }
        /// <summary>
        /// GetUserByEmailId
        /// </summary>
        /// <param name="SearchForm">{"Email":""}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/User/ForgotPassword")]
        public HttpResponseMessage ForgotPassword(dynamic SearchForm)
        {
            try
            {
                FunctionResponse Resp = null;

                string EmailId = (string)SearchForm.Email == null ? "" : (string)SearchForm.Email;
                if (!string.IsNullOrEmpty(EmailId))
                {
                    Resp = _userInterface.GetUserByEmailId(EmailId);
                }

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    List<UserEntity> User = (List<UserEntity>)Resp.Data[0];
                    Resp = SendMail(SearchForm, User);

                    if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                    }
                    else if (Resp.Status == FunctionResponse.StatusType.ERROR)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent, Resp.Message);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Sent");
                    }


                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }
        private FunctionResponse SendMail(dynamic SearchForm, List<UserEntity> User)
        {

            try
            {
                FunctionResponse Resp;
                string To = User[0].Email.ToString().Trim();
                string Cc = SearchForm.Cc;
                string Subject = "P2P Credential";

                StringBuilder sb = new StringBuilder("Hi ", 500);

                //char.ToUpper(User[0].LastName[0]) + User[0].LastName.Substring(1)
                string hash = _userInterface.SetPasswordExpireUrl(User[0].Id);

                //Encode hash for html url comptible
                hash = WebUtility.UrlEncode(hash);

                //SHA256 algorithm = SHA256.Create();
                //string hash =  Utility.GetHash(User[0].Id.ToString(), algorithm);
                // bool veryfi = Utility.VerifyHash(algorithm, User[0].Id.ToString(), hash);

                sb.Append(char.ToUpper(User[0].FirstName[0]) + User[0].FirstName.Substring(1) + " " + char.ToUpper(User[0].LastName[0]) + User[0].LastName.Substring(1) + ",");

                sb.AppendLine("<p>For Your Practice2Perfection Password click <a href=" + ConfigurationManager.AppSettings["ForgotPwdUrl"].ToString() + hash + ">here </a></p>");


                string Body = sb.ToString();

                Resp = _IEmailInterface.SendEmail(To, Cc, Subject, Body);
                return Resp;

            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/User/Verify")]
        public HttpResponseMessage EmailVerify(Guid Id)
        {
            try
            {
                FunctionResponse Resp = null;

                Guid EmailId = Id;
                if (EmailId != Guid.Empty)
                {
                    // var user = _userInterface.GetUserById(Id);
                    Resp = _userInterface.EmailVerification(EmailId);
                }

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    Resp.Message = "Success";
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);

                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }

        /// <summary>
        /// Check Email Id Is Exist or not
        /// </summary>
        /// <param name="SearchForm">{"Email":""}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/User/IsEmailExist")]
        public HttpResponseMessage IsEmailExist(dynamic SearchForm)
        {
            try
            {
                FunctionResponse Resp = null;
                string EmailId = (string)SearchForm.Email == null ? "" : (string)SearchForm.Email;

                if (!string.IsNullOrEmpty(EmailId))
                {
                    Resp = _userInterface.GetUserByEmailId(EmailId);

                    if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.OK, "Success");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.OK, "Fail");
                    }
                }

                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Fail");
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        // Is Email exist or not function ENDS HERE
        [HttpPost]
        [Route("api/User/ChangePassword")]
        public HttpResponseMessage ChangePassword(dynamic SearchForm)
        {
            try
            {
                Guid id = (Guid)SearchForm.Id;
                string oldpwd = (string)SearchForm.oPwd;
                string newpwd = (string)SearchForm.nPwd;


                FunctionResponse Resp = _userInterface.changepassword(id, oldpwd, newpwd);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpPost]
        [Route("api/User/UpdatePassword")]
        public HttpResponseMessage UpdatePassword(UserEntity user)
        {
            try
            {
                FunctionResponse Resp = _userInterface.Updatepassword(user.Hashcode, user.Pwd);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp.Message);
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/User/GetPassword")]
        public HttpResponseMessage GetPassword(Guid id)
        {
            try
            {
                string pwd = Convert.ToString(_userInterface.getpassword(id));


                if (pwd != null)
                    return Request.CreateResponse(HttpStatusCode.OK, pwd);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No user found for this id");
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/User/GetUserByEmail")]
        public HttpResponseMessage GetUserByEmailId(string Email)
        {
            try
            {
                FunctionResponse Resp = null;

                string EmailId = Email;
                if (!string.IsNullOrEmpty(EmailId))
                {
                    Resp = _userInterface.GetUserByEmailId(EmailId);
                }

                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    List<UserEntity> User = (List<UserEntity>)Resp.Data[0];
                    Resp.Data.Clear();
                    Resp.Data.Add(User[0].Id);
                    Resp.Message = "Success";
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);

                }
                else if (Resp.Status != FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, Resp);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.OK, Resp.Message);
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here
                // Logger.Error(ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Hi You have 500_Internal_Server_Error");
            }
        }
        [HttpPost]
        [Route("api/User/RemoveUser")]
        public HttpResponseMessage RemoveUser(UserEntity user)
        {
            try
            {



                FunctionResponse Resp = _userInterface.RemoveUser(user);
                if (Resp.Status == FunctionResponse.StatusType.SUCCESS)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");
                }
                else if (Resp.Status == FunctionResponse.StatusType.CRITICAL_ERROR)
                {
                    return Request.CreateResponse(HttpStatusCode.NotImplemented, Resp.Message);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No_Record_Found");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }

        }
    }
}

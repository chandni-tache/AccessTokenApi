using TachTechnologies.DataAccessLayer.BusinessUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TachTechnologies.DataAccessLayer.Repository
{
    public class UserRepository : IRegisterUser
    {
        private UserContext userDBContext = new UserContext();
        private BusinessUtils businesUtilobj = new BusinessUtils();
        /// <summary>
        /// Method user for Add new user if required
        /// </summary>
        /// <param name="user"></param>
        public void InsertUser(User user)
        {
            userDBContext.Users.Add(user);
            userDBContext.SaveChanges();
        }

        /// <summary>
        /// Update user with new Access Tokesn 
        /// BusinessUtils.GetUniqueKey(200) For Uniquekey of 200 words 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseType UpdateUser(User user)
        {
            ResponseType responseType = new ResponseType();
            try
            {
                if (ValidateUsername(user))
                {
                    User userToUpdate = userDBContext
                        .Users.SingleOrDefault(x => x.UserName == user.UserName);
                    string AccessToken = BusinessUtils.GetUniqueKey(200);
                    userToUpdate.AccessToken = AccessToken;
                    userToUpdate.PasswordHash = user.PasswordHash;
                    userToUpdate.URLParams = user.URLParams;
                    userDBContext.SaveChanges();
                    responseType.ErrorMsg = "success";
                    responseType.AccessToken = AccessToken;
                }
                else
                {
                    responseType.ErrorMsg = "UserName does not exists!!";
                    responseType.AccessToken = "";
                }
            }
            catch (Exception ex)
            {
                responseType.ErrorMsg = ex.Message;
                responseType.AccessToken = "";
            }

            return responseType;
        }
        /// <summary>
        /// ValidateUserToken: request toke is valide or not 
        /// if toke is valide to update with null value 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TokenResponseType ValidateUserToken(User user)
        {
            TokenResponseType tokenResponseType = new TokenResponseType();

            if (ValidateToken(user))
            {
                User userToUpdate = userDBContext
                    .Users.SingleOrDefault(x => x.AccessToken == user.AccessToken);

                userToUpdate.AccessToken = null; ;
                userDBContext.SaveChanges();
                tokenResponseType.ErrorMsg = "success";
                tokenResponseType.UserName = userToUpdate.UserName;
                tokenResponseType.PasswordHash = userToUpdate.PasswordHash;
            }
            else
            {
                tokenResponseType.ErrorMsg = "AccessToken does not exists!!";
                tokenResponseType.UserName = "";
                tokenResponseType.PasswordHash = "";
            }
            return tokenResponseType;
        }
        /// <summary>
        /// ValidateRegisteredUser: check user is exist or not in database 
        /// </summary>
        /// <param name="registeruser"></param>
        /// <returns></returns>
        public bool ValidateRegisteredUser(User registeruser)
        {
            var usercount = (from Users in userDBContext.Users
                             where Users.UserName == registeruser.UserName &&
                                   Users.PasswordHash == registeruser.PasswordHash
                             select Users).Count();
            if (usercount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///ValidateToken: toke in exist or not 
        /// </summary>
        /// <param name="registeruser"></param>
        /// <returns></returns>
        ///

        public bool ValidateToken(User registeruser)
        {
            var usercount = (from Users in userDBContext.Users
                             where Users.AccessToken == registeruser.AccessToken
                             select Users).Count();
            if (usercount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///ValidateUsername: validate user by UserName
        /// </summary>
        /// <param name="registeruser"></param>
        /// <returns></returns>

        public bool ValidateUsername(User registeruser)
        {
            var usercount = (from Users in userDBContext.Users
                             where Users.UserName == registeruser.UserName
                             select Users).Count();
            if (usercount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsValid(User user)
        {
            if (string.IsNullOrEmpty(user.UId))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(user.PasswordHash))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(user.UserName))
            {
                return false;
            }
            return true;
        }
    }
}
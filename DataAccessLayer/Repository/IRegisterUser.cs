using TachTechnologies.DataAccessLayer.BusinessUtil;

namespace TachTechnologies.DataAccessLayer.Repository
{
    public interface IRegisterUser
    {
        void InsertUser(User registeruser);

        ResponseType UpdateUser(User registeruser);

        bool ValidateRegisteredUser(User registeruser);

        bool ValidateUsername(User registeruser);

        TokenResponseType ValidateUserToken(User registeruser);

        bool ValidateToken(User registeruser);
    }
}
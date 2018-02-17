namespace TachTechnologies.DataAccessLayer.BusinessUtil
{
    public class ResponseType
    {
        public string ErrorMsg { get; set; }
        public string AccessToken { get; set; }
    }

    public class TokenResponseType
    {
        public string ErrorMsg { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
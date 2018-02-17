using System.ComponentModel.DataAnnotations;

namespace TachTechnologies.DataAccessLayer
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "UId required")]
        public string UId { get; set; }

        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "UserName required")]
        public string UserName { get; set; }

        public string AccessToken { get; set; }
        public string URLParams { get; set; }
    }
}
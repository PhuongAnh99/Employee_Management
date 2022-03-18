using System.ComponentModel.DataAnnotations;

namespace EmployeeManage.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        public string UserName
        {
            get;
            set;
        }
        [Required]
        public string Password
        {
            get;
            set;
        }
    }
}

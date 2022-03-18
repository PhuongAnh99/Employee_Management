using EmployeeManage.Models;

namespace EmployeeManage.JWT
{
    public class UserTokens
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
        public TimeSpan Validaty { get; set; }
        public string RefreshToken { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime ExpiredTime { get; set; }
        public DateTime RefreshExpiredTime { get; set; }
    }
}

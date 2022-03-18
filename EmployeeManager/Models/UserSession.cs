namespace EmployeeManage.Models
{
    public class UserSession : BaseModel
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpireTime { get; set; }
        public virtual User User { get; set; }
    }
}

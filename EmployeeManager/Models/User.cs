using System.ComponentModel.DataAnnotations;

namespace EmployeeManage.Models
{
    public class User : BaseModel
    {
        public User()
        {
            UserSessions = new HashSet<UserSession>();
        }
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<UserSession> UserSessions { get; set; }
    }
}

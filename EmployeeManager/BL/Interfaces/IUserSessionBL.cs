using EmployeeManage.Models;

namespace EmployeeManage.BL.Interfaces
{
    public interface IUserSessionBL : IBaseBL<UserSession>
    {
        CompanyContext GetDbContext();
    }
}

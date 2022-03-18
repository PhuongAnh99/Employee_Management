using EmployeeManage.Models;
using EmployeeManage.Models.Requests;
using EmployeeManage.Models.Responses;

namespace EmployeeManage.BL.Interfaces
{
    public interface IUserBL : IBaseBL<User>
    {
        ServiceResponse Login(LoginRequest model);
        ServiceResponse Register(RegisterRequest model);
    }
}

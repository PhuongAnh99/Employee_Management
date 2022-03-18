using EmployeeManage.BL.Interfaces;
using EmployeeManage.JWT;
using EmployeeManage.Models;
using EmployeeManage.Models.Requests;
using EmployeeManage.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EmployeeManage.Models.Enums.Enumerations;

namespace EmployeeManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserBL _userBL;

        public AccountController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        /// <summary>
        /// API đăng ký người dùng mới
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public ServiceResponse Register([FromBody] RegisterRequest registerModel)
        {
            var res = new ServiceResponse();

            try
            {
                res = _userBL.Register(registerModel);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }

        /// <summary>
        /// Get List of UserAccounts
        /// </summary>
        /// <returns>List Of UserAccounts</returns>
        [HttpPost("Login")]
        public ServiceResponse Login([FromBody] LoginRequest loginModel)
        {
            var res = new ServiceResponse();

            try
            {
                res = _userBL.Login(loginModel);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }
    }
}

using EmployeeManage.BL.Interfaces;
using EmployeeManage.JWT;
using EmployeeManage.Models;
using EmployeeManage.Models.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static EmployeeManage.Models.Enums.Enumerations;

namespace EmployeeManage.Authorize
{
    public class MyAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public bool Ignore { get; set; } = false;

        public MyAuthorizeAttribute(bool igNore = false)
        {
            Ignore = igNore;
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Validate Access Token
            var jwtSetting = context.HttpContext.RequestServices.GetService<JwtSettings>();
            var accessToken = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
            var isValidAT = JwtHelpers.ValidateToken(accessToken, jwtSetting);
            if (!isValidAT)
            {
                // Validate Refresh Token
                var refreshToken = context.HttpContext.Request.Headers["RefreshToken"];
                var isValidRT = JwtHelpers.ValidateToken(refreshToken, jwtSetting);

                if (isValidRT)
                {
                    context.Result = new ObjectResult(new ServiceResponse()
                    .OnError(Code.Unauthorize, SubCode.ErrorAuthorize));
                    return;
                }

                // Gen Access Token mới
                var newAccessToken = JwtHelpers.GenJwtToken(new UserTokens()
                {
                    UserName = context.HttpContext.Request.Headers["UserName"],
                    Email = context.HttpContext.Request.Headers["Email"],
                    Id = int.Parse(context.HttpContext.Request.Headers["UserID"]),
                }, "AccessToken", jwtSetting);

                // Cập nhật Access Token mới vào bảng UserSession trong Db
                var userSessionBL = context.HttpContext.RequestServices.GetService<IUserSessionBL>();
                var findBySessionID = userSessionBL.GetById(Guid.Parse(Convert.ToString(context.HttpContext.Request.Headers["SessionID"])));
                findBySessionID.AccessToken = newAccessToken;
                var dbContext = userSessionBL.GetDbContext();
                int effect = dbContext.SaveChanges();
                if (effect < 1)
                {
                    context.Result = new ObjectResult(new ServiceResponse()
                    .OnError(Code.Unauthorize, SubCode.ErrorAuthorize));
                    return;
                }
            }
        }
    }
}

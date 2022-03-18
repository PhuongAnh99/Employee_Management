using EmployeeManage.Authorize;
using EmployeeManage.BL.Interfaces;
using EmployeeManage.Helper;
using EmployeeManage.JWT;
using EmployeeManage.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static EmployeeManage.Models.Enums.Enumerations;

namespace EmployeeManage.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;
        public JWTMiddleware(RequestDelegate next, JwtSettings jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            var authorizeAttributes = context.GetEndpoint().Metadata.OfType<MyAuthorizeAttribute>().Any();
            if (authorizeAttributes)
            {
                try
                {
                    var isContinue = await SetHeader(context);
                    if (!isContinue)
                    {
                        context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                        await context.Response.WriteAsync(Converter.Serialize(new ServiceResponse()
                        .OnError(Code.Unauthorize, SubCode.ErrorAuthorize)), Encoding.UTF8);
                        return;
                    }
                }
                catch
                {
                    context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                    await context.Response.WriteAsync(Converter.Serialize(new ServiceResponse()
                    .OnError(Code.Unauthorize, SubCode.ErrorAuthorize)), Encoding.UTF8);
                    return;
                }
            }

            await _next(context);
        }

        private async Task<bool> SetHeader(HttpContext context)
        {
            if (context.Request == null)
                return false;

            // Thực hiện đọc session id từ cookie
            if (!context.Request.Headers.ContainsKey("UserCookie"))
                return false;

            var parseHeader = Converter.Deserialize<LoginResponse>(Convert.ToString(context.Request.Headers["UserCookie"]));
            var sessionID = parseHeader.SessionID;

            if (string.IsNullOrWhiteSpace(Convert.ToString(sessionID)))
                return false;

            // Thực hiện lấy access token
            var userSessionBL = context.RequestServices.GetService<IUserSessionBL>();
            var findBySessionID = userSessionBL.GetById(Guid.Parse(Convert.ToString(sessionID)));
            if (findBySessionID == null)
                return false;

            var accessToken = findBySessionID.AccessToken;
            context.Request.Headers.Authorization = "Bearer " + accessToken;
            context.Request.Headers["SessionID"] = parseHeader.SessionID;
            context.Request.Headers["UserID"] = parseHeader.UserID;
            context.Request.Headers["UserName"] = parseHeader.UserName;
            context.Request.Headers["Email"] = parseHeader.Email;
            context.Request.Headers["RefreshToken"] = findBySessionID.RefreshToken;

            return true;
        }
    }
}

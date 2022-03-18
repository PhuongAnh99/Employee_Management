using EmployeeManage.BL.Implements;
using EmployeeManage.BL.Interfaces;
using EmployeeManage.JWT;
using EmployeeManage.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManage.Extensions
{
    public static class ConfigServicesExtensions
    {
        public static void AddJWTTokenServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            var bindJwtSettings = new JwtSettings();
            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

            Services.AddSingleton(bindJwtSettings);
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.ValidateLifetime,
                    ClockSkew = TimeSpan.FromDays(1),
                };
            });
        }
        public static void AddInjectionServices(this IServiceCollection Services, ConfigurationManager Configuration)
        {
            Services.AddTransient(typeof(IBaseBL<>), typeof(BaseBL<>));
            Services.AddTransient<IUserBL, UserBL>();
            Services.AddTransient<IDepartmentBL, DepartmentBL>();
            Services.AddTransient<IEmployeeBL, EmployeeBL>();
            Services.AddTransient<IUserSessionBL, UserSessionBL>();
        }
    }
}

using EmployeeManage.BL.Interfaces;
using EmployeeManage.JWT;
using EmployeeManage.Models;
using EmployeeManage.Models.Requests;
using EmployeeManage.Models.Responses;
using static EmployeeManage.Models.Enums.Enumerations;

namespace EmployeeManage.BL.Implements
{
    public class UserBL : BaseBL<User>, IUserBL
    {
        private readonly JwtSettings _jwtSettings;
        public UserBL(CompanyContext dbcontext, JwtSettings jwtSettings) : base(dbcontext)
        {
            _jwtSettings = jwtSettings;
        }

        /// <summary>
        /// Đăng nhập người dùng
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ServiceResponse Login(LoginRequest model)
        {
            var res = new ServiceResponse();

            var user = _dbcontext.User.FirstOrDefault(x => x.UserName == model.UserName);

            // kiểm tra tên đăng nhập và mật khẩu đã đúng chưa
            if (user == null || model.Password != user.Password)
                return res.OnError(Code.Success, SubCode.ErrorInvalid, null, "Tên đăng nhập hoặc Mật khẩu không chính xác!");

            // Gen Access Token
            var userToken = JwtHelpers.GenAccessToken(
                new UserTokens()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Id = user.Id,
                },
                _jwtSettings);

            // Thêm mới bản ghi session đăng nhập vào database
            var userSession = new UserSession()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                LoginDate = DateTime.Now,
                AccessToken = userToken.AccessToken,
                RefreshToken = userToken.RefreshToken,
                RefreshTokenExpireTime = userToken.RefreshExpiredTime
            };
            _dbcontext.UserSession.Add(userSession);
            var effect = _dbcontext.SaveChanges();
            if (effect < 1)
                return res.OnError(Code.Success, SubCode.ErrorInsert, devMessage: "Không thêm mới được bản ghi session");

            return res.OnSuccess(new LoginResponse()
            {
                SessionID = userSession.Id.ToString(),
                UserID = user.Id.ToString(),
                UserName = user.UserName,
                Email = user.Email
            });
        }

        /// <summary>
        /// Đăng ký người dùng mới
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="Exception"></exception>
        public ServiceResponse Register(RegisterRequest model)
        {
            var res = new ServiceResponse();

            // Kiểm tra tên đăng nhập đã tồn tại chưa
            if (_dbcontext.User.Any(x => x.UserName == model.UserName))
                return res.OnError(Code.Success, SubCode.ErrorDuplicate, null, "Tên đăng nhập đã tồn tại!");
            // Kiểm tra Email đã tồn tại chưa
            if (_dbcontext.User.Any(x => x.Email == model.Email))
                return res.OnError(Code.Success, SubCode.ErrorDuplicate, null, "Email đã tồn tại!");

            // save user
            _dbcontext.User.Add(new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = model.Password,
            });
            var effect = _dbcontext.SaveChanges();
            if (effect <= 0)
                return res.OnError(Code.Success, SubCode.ErrorInsert, null, "Đăng ký người dùng thất bại!");

            return res;
        }
    }
}

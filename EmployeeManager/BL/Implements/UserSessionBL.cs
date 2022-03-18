using EmployeeManage.BL.Interfaces;
using EmployeeManage.Models;

namespace EmployeeManage.BL.Implements
{
    public class UserSessionBL : BaseBL<UserSession>, IUserSessionBL
    {
        public UserSessionBL(CompanyContext dbcontext) : base(dbcontext)
        {
        }

        public override UserSession GetById(Guid id)
        {
            return _dbcontext.UserSession.AsEnumerable().FirstOrDefault(p => p.Id == id);
        }

        public CompanyContext GetDbContext()
        {
            return _dbcontext;
        }
    }
}

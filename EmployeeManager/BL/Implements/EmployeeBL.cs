using EmployeeManage.BL.Interfaces;
using EmployeeManage.Models;

namespace EmployeeManage.BL.Implements
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        public EmployeeBL(CompanyContext dbcontext) : base(dbcontext)
        {
        }
    }
}

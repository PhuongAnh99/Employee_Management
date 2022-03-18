using EmployeeManage.BL.Interfaces;
using EmployeeManage.Models;

namespace EmployeeManage.BL.Implements
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        public DepartmentBL(CompanyContext dbcontext) : base(dbcontext)
        {
        }
    }
}

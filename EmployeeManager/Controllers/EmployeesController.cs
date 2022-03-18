using EmployeeManage.Authorize;
using EmployeeManage.BL.Interfaces;
using EmployeeManage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
    [MyAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        private readonly IEmployeeBL _departmentBL;

        public EmployeesController(IEmployeeBL employeeBL)
        {
            BL = employeeBL;
        }
    }
}

using EmployeeManage.Authorize;
using EmployeeManage.BL.Implements;
using EmployeeManage.BL.Interfaces;
using EmployeeManage.Models;
using EmployeeManage.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManage.Controllers
{
    [MyAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        private readonly IDepartmentBL _departmentBL;

        public DepartmentsController(IDepartmentBL departmentBL)
        {
            BL = departmentBL;
        }
    }
}

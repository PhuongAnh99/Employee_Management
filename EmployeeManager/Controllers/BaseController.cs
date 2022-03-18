using EmployeeManage.BL.Interfaces;
using EmployeeManage.Helper;
using EmployeeManage.Models;
using EmployeeManage.Models.Requests;
using EmployeeManage.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using static EmployeeManage.Models.Enums.Enumerations;

namespace EmployeeManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase where T : BaseModel
    {
        protected IBaseBL<T> BL { get; set; }

        /// <summary>
        /// API thêm mới
        /// </summary>
        /// <param name="insertModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ServiceResponse Insert([FromBody] object insertModel)
        {
            var res = new ServiceResponse();

            try
            {
                var parseModel = Converter.Deserialize<T>(insertModel.ToString());
                res = BL.Insert(parseModel);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }

        /// <summary>
        /// API cập nhật
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        [HttpPut]
        public ServiceResponse Update([FromBody] T updateModel)
        {
            var res = new ServiceResponse();

            try
            {
                res = BL.Update(updateModel);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }

        /// <summary>
        /// API xóa
        /// </summary>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        [HttpDelete]
        public ServiceResponse Delete([FromBody] int id)
        {
            var res = new ServiceResponse();

            try
            {
                res = BL.Delete(id);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }

        /// <summary>
        /// API lấy toàn bộ danh sách
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ServiceResponse GetAll()
        {
            var res = new ServiceResponse();

            try
            {
                res = BL.GetAll();
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }

        /// <summary>
        /// API lấy dữ liệu phân trang
        /// </summary>
        /// <returns></returns>
        [HttpGet("Paging")]
        public ServiceResponse GetPaging([FromBody] PagingRequest pagingRequest)
        {
            var res = new ServiceResponse();

            try
            {
                res.Data = BL.GetPaging(pagingRequest);
            }
            catch (Exception ex)
            {
                res.OnError(Code.Failure, SubCode.Exception, ex);
            }

            return res;
        }
    }
}

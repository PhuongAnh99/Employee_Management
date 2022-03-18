using static EmployeeManage.Models.Enums.Enumerations;

namespace EmployeeManage.Models.Responses
{
    public class ServiceResponse
    {
        public bool Success { get; set; } = true;
        public Code Code { get; set; } = Code.Success;
        public SubCode SubCode { get; set; } = SubCode.Success;
        public object Data { get; set; } = null;
        public string Message { get; set; } = null;
        public string DevMessage { get; set; } = null;

        public ServiceResponse()
        {
        }

        /// <summary>
        /// Thực hiện khi thành công dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <param name="devMessage"></param>
        /// <returns></returns>
        /// created by vdthang 18.01.2022
        public ServiceResponse OnSuccess(object data = null, string message = "Thành công", string devMessage = "Thành công")
        {
            this.Success = true;
            this.Code = Code.Success;
            this.Data = data;
            this.Message = message;
            this.DevMessage = devMessage;

            return this;
        }

        /// <summary>
        /// Thực hiện khi lỗi dữ liệu
        /// </summary>
        /// <param name="code"></param>
        /// <param name="subCode"></param>
        /// <param name="message"></param>
        /// <param name="devMessage"></param>
        /// <returns></returns>
        public ServiceResponse OnError(Code code, SubCode subCode, object data = null, string message = "Hệ thống xảy ra lỗi. Vui lòng thử lại sau!", string devMessage = "Lỗi xảy ra")
        {
            this.Success = false;
            this.Code = code;
            this.Data = data;
            this.Message = message;
            this.DevMessage = devMessage;

            return this;
        }
    }
}

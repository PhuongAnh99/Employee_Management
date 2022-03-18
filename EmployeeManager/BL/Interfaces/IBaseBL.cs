using EmployeeManage.Models;
using EmployeeManage.Models.Requests;
using EmployeeManage.Models.Responses;

namespace EmployeeManage.BL.Interfaces
{
    public interface IBaseBL<T> where T : BaseModel
    {
        PagingResponse GetPaging(PagingRequest pagingRequest);
        ServiceResponse GetAll();
        T GetById(int id);
        T GetById(Guid id);
        ServiceResponse Insert(T entity);
        ServiceResponse Update(T entity);
        ServiceResponse Delete(int id);
    }
}

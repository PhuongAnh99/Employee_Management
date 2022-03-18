using EmployeeManage.BL.Interfaces;
using EmployeeManage.Models;
using EmployeeManage.Models.Requests;
using EmployeeManage.Models.Responses;
using Microsoft.EntityFrameworkCore;
using static EmployeeManage.Models.Enums.Enumerations;

namespace EmployeeManage.BL.Implements
{
    public class BaseBL<T> : IBaseBL<T> where T : BaseModel
    {
        protected readonly CompanyContext _dbcontext;
        public BaseBL(CompanyContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        /// <summary>
        /// Hàm thực hiện lấy tất cả dữ liệu
        /// </summary>
        /// <returns></returns>
        public ServiceResponse GetAll()
        {
            var res = new ServiceResponse();
            return res.OnSuccess(_dbcontext.Set<T>().ToList());
        }

        /// <summary>
        /// Hàm lấy dữ liệu phân trang
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <returns></returns>
        public PagingResponse GetPaging(PagingRequest pagingRequest)
        {
            var pagingResponse = new PagingResponse()
            {
                PageData = _dbcontext.Set<T>()
                                    .Skip((pagingRequest.PageIndex - 1) * pagingRequest.PageSize)
                                    .Take(pagingRequest.PageSize).ToList(),
                TotalRecord = _dbcontext.Set<T>().Count(),
                TotalPage = (int)Math.Ceiling(_dbcontext.Set<T>().Count() / (double)pagingRequest.PageSize),
                CurrentPage = pagingRequest.PageIndex
            };
            return pagingResponse;
        }

        /// <summary>
        /// Hàm thực hiện lấy dữ liệu theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual T GetById(int id)
        {
            var entity = _dbcontext.Set<T>().Find(id);
            return entity;
        }

        /// <summary>
        /// Hàm thực hiện lấy dữ liệu theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public virtual T GetById(Guid id)
        {
            var entity = _dbcontext.Set<T>().Find(id);
            return entity;
        }

        /// <summary>
        /// Hàm thực hiện thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ServiceResponse Insert(T entity)
        {
            var res = new ServiceResponse();
            _dbcontext.Set<T>().Add(entity);
            var effect = _dbcontext.SaveChanges();
            if (effect < 0)
            {
                return res.OnError(Code.Success, SubCode.ErrorInsert);
            }
            else
            {
                return res.OnSuccess();
            }
        }

        /// <summary>
        /// Hàm thực hiên cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ServiceResponse Update(T entity)
        {
            var res = new ServiceResponse();
            _dbcontext.Set<T>().Update(entity);
            var effect = _dbcontext.SaveChanges();
            if (effect < 0)
            {
                return res.OnError(Code.Success, SubCode.ErrorInsert);
            }
            else
            {
                return res.OnSuccess();
            }
        }

        /// <summary>
        /// Hàm thực hiện xóa dữ liệu
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse Delete(int id)
        {
            var res = new ServiceResponse();
            var entity = _dbcontext.Set<T>().Find(id);
            _dbcontext.Set<T>().Update(entity);
            var effect = _dbcontext.SaveChanges();
            if (effect < 0)
            {
                return res.OnError(Code.Success, SubCode.ErrorInsert);
            }
            else
            {
                return res.OnSuccess();
            }
        }
    }
}

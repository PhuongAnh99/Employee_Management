namespace EmployeeManage.Models.Responses
{
    public class PagingResponse
    {
        public object PageData { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalRecord;
    }
}

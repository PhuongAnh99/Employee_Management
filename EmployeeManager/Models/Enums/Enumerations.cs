namespace EmployeeManage.Models.Enums
{
    public static class Enumerations
    {
        public enum Code : int
        {
            Success = 200,
            Accepted = 202,
            NoContent = 204,
            BadRequest = 400,
            Unauthorize = 401,
            Forbidden = 403,
            NotFound = 404,
            WrongFormat = 415,
            Failure = 500,
            Unavailable = 503
        }
        public enum SubCode : int
        {
            Success = 0,
            ErrorInsert = 1,
            ErrorUpdate = 50,
            ErrorDelete = 100,
            ErrorDuplicate = 200,
            ErrorAuthorize = 401,
            ErrorForbidden = 403,
            ErrorNotFound = 404,
            ErrorInvalid = 405,
            Exception = 1000
        }
    }
}

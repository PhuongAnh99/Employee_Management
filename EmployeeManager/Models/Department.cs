using System.ComponentModel.DataAnnotations;

namespace EmployeeManage.Models
{
    public class Department : BaseModel
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}

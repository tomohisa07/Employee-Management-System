using System.Runtime.Serialization;

namespace EmsApi.Models
{
    public class Department
    {
        [DataMember(Name = "DepartmentId")]
        public int DepartmentId { get; set; }

        [DataMember(Name = "DepartmentName")]
        public string? DepartmentName { get; set; }
    }
}

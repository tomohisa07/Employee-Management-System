using System.Runtime.Serialization;
using System.Xml.Linq;

namespace EmsApi.Models
{
    public class User
    {
        [DataMember(Name = "Id")]
        public int? Id { get; set; }

        [DataMember(Name = "Email")]
        public string? Email { get; set; }

        [DataMember(Name = "Password")]
        public string? Password { get; set; }
    }
}

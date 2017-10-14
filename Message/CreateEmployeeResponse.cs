using Infrastructure;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class CreateEmployeeResponse : BaseResponse
    {
        [DataMember(Name = "Employee")]
        public EmployeeMessage Employee { get; set; }
    }
}

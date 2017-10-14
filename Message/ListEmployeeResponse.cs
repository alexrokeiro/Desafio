using Infrastructure;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class ListEmployeeResponse : BaseResponse
    {
        [DataMember(Name = "employees")]
        public List<EmployeeMessage> Employees { get; set; }

    }
}

using Infrastructure;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class ListEmployeeResponse : BaseResponse
    {
        [DataMember(Name = "id")]
        public List<EmployeeMessage> Usuarios { get; set; }

    }
}

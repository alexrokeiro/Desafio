using Infrastructure;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class CreateEmployeeResponse : BaseResponse
    {
        [DataMember(Name = "idEmployee")]
        public int IdEmployee { get; set; }
    }
}

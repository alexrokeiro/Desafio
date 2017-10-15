using Infrastructure;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class AddDependentResponse : BaseResponse
    {
        [DataMember(Name = "idDependent")]
        public int IdDependent { get; set; }
    }
}

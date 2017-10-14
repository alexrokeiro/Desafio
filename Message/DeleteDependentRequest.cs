using Infrastructure;
using System.Runtime.Serialization;

namespace Message
{
    [DataContract]
    public class DeleteDependentRequest : BaseRequest
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }
}

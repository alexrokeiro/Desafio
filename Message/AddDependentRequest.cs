using Infrastructure;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class AddDependentRequest : BaseRequest
    {
        [DataMember(Name = "idEmployee")]
        public int IdUsuario { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}

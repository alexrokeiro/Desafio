using Infrastructure;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class ListUserResponse : BaseResponse
    {
        [DataMember(Name = "id")]
        public List<UserMessage> Usuarios { get; set; }

    }
}

using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Message.Response
{
    [DataContract]
    public class ListUserResponse : BaseResponse
    {
        [DataMember(Name = "id")]
        public List<UserMessage> Usuarios { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Message.Response
{
    [DataContract]
    public class ListarUsuarioResponse 
    {
        [DataMember(Name = "id")]
        public List<UsuarioMessage> Usuarios { get; set; }

    }
}

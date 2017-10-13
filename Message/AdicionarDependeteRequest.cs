using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    [DataContract]
    public class AdicionarDependeteRequest
    {
        [DataMember(Name = "idUsuario")]
        public int IdUsuario { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}

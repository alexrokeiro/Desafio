using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    [DataContract]
    public class ListUserRequest : BaseRequest
    {
        [DataMember(Name = "name")]
        [Required(ErrorMessage = "Campo name é obrigatório.")]
        public string name { get; set; }

        [DataMember(Name = "like")]
        [Required(ErrorMessage = "Campo email é obrigatório.")]
        public bool like { get; set; }

    }
}

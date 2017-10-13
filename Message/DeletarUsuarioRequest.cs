using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    public class DeletarUsuarioRequest
    {
        [DataMember(Name = "id")]
        [Required(ErrorMessage = "Campo id é obrigatório.")]
        public int id { get; set; }
    }
}

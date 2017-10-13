using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ValidationMessage
    {
        /// <summary>
        /// Campo com erro de validação. Ex: "Cep".
        /// </summary>
        [DataMember(Name = "Atributo")]
        public string Atributo { get; set; }

        /// <summary>
        /// Mensagem para a validação do campo. Ex: "Campo obrigatório".
        /// </summary>
        [DataMember(Name = "Mensagem")]
        public string Mensagem { get; set; }
    }
}

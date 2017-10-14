using Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    public class DeleteEmployeeRequest : BaseRequest
    {
        [DataMember(Name = "id")]
        [Required(ErrorMessage = "Campo id é obrigatório.")]
        public int id { get; set; }
    }
}

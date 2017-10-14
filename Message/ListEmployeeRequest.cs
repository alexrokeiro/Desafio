using Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class ListEmployeeRequest : BaseRequest
    {
        [DataMember(Name = "name")]
        [Required(ErrorMessage = "Campo name é obrigatório.")]
        public string name { get; set; }

        [DataMember(Name = "like")]
        [Required(ErrorMessage = "Campo email é obrigatório.")]
        public bool like { get; set; }

    }
}

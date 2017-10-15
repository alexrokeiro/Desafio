using Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class AlterEmployeeRequest : BaseRequest
    {
        [DataMember(Name = "id")]
        [Required(ErrorMessage = "Campo id é obrigatório.")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        [Required(ErrorMessage = "Campo name é obrigatório.")]
        public string Name { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "genre")]
        [Required(ErrorMessage = "Campo genre é obrigatório.")]
        [Range(0, 1, ErrorMessage = "Masculino - 0 ou Feminino  - 1")]
        public int Genre { get; set; }

        [DataMember(Name = "birth")]
        public DateTime Birth { get; set; }

        [DataMember(Name = "role")]
        [Required(ErrorMessage = "Campo role é obrigatório.")]
        public int Role { get; set; }
    }
}

using System;
using System.Runtime.Serialization;

namespace Domain.Message.Message
{
    [DataContract]
    public class EmployeeMessage
    {
        [DataMember(Name = "id")]
        public int id { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "genre")]
        public string genre { get; set; }

        [DataMember(Name = "birth")]
        public DateTime Birth { get; set; }

        [DataMember(Name = "role")]
        public string Role { get; set; }

        [DataMember(Name = "QuantidadeDependentes")]
        public int QuantidadeDependentes { get; set; }
    }
}

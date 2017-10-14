﻿using Infrastructure;
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
        public int id { get; set; }

        [DataMember(Name = "name")]
        [Required(ErrorMessage = "Campo name é obrigatório.")]
        public string name { get; set; }

        [DataMember(Name = "email")]
        [Required(ErrorMessage = "Campo email é obrigatório.")]
        public string Email { get; set; }

        [DataMember(Name = "genre")]
        [Required(ErrorMessage = "Campo genre é obrigatório.")]
        public string genre { get; set; }

        [DataMember(Name = "birth")]
        [Required(ErrorMessage = "Campo birth é obrigatório.")]
        public DateTime Birth { get; set; }

        [DataMember(Name = "role")]
        [Required(ErrorMessage = "Campo role é obrigatório.")]
        public int Role { get; set; }
    }
}
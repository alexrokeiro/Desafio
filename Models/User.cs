using System;
using System.Collections.Generic;

namespace Domain.Model.Models
{
    public class User
    {
        
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Genre { get; set; }

        public virtual DateTime Birth { get; set; }

        public virtual Role Role { get; set; }

        public virtual IList<Dependent> Dependents { get; set; } = new List<Dependent>();

        public static User CreateUser(string name, string email, string genre, DateTime birth, Role role)
        {
            return new User()
            {
                Name = name,
                Email = email,
                Genre = genre,
                Birth = birth,
                Role = role
            };
        }

        public virtual void UpdateUser(string name, string email, string genre, DateTime birth)
        {
            this.Name = name;
            this.Email = email;
            this.Genre = genre;
            this.Birth = birth;
        }
    }
}

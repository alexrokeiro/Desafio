using System;
using System.Collections.Generic;

namespace Domain.Model.Models
{
    public class Employee
    {

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual int Genre { get; set; }

        public virtual DateTime Birth { get; set; }

        public virtual Role Role { get; set; }

        public virtual IList<Dependent> Dependents { get; set; } = new List<Dependent>();

        public static Employee CreateEmployee(string name, string email, int genre, DateTime birth, Role role)
        {
            return new Employee()
            {
                Name = name,
                Email = email,
                Genre = genre,
                Birth = birth,
                Role = role
            };
        }

        public virtual void UpdateEmployee(string name, string email, int genre, DateTime birth, Role role)
        {
            this.Name = name;
            this.Email = email;
            this.Genre = genre;
            this.Birth = birth;
            this.Role = role;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        //public virtual User User { get; set; }
    }
}

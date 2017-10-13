using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Dependent
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual int IdUser { get; set; }

    }
}

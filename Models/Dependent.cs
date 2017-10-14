namespace Domain.Model.Models
{
    public class Dependent
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual int IdUser { get; set; }

    }
}

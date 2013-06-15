using System;

namespace BeerApp.Models
{
    public class Beer : IAuditInfo
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Brewer Brewer { get; set; }
        public virtual Style Style { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}

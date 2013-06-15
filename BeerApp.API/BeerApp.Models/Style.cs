using System;

namespace BeerApp.Models
{
    public class Style : IAuditInfo
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Category Category { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

}

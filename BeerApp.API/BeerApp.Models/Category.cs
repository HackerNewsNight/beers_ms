using System;

namespace BeerApp.Models
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Modified { get; set; }
    }
}

using System;
using System.Linq;

namespace BeerApp.Domain
{
    public class Beer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Brewer Brewer { get; set; }
        public virtual Style Style { get; set; }
    }

    public class Entry
    {
        public virtual int Id { get; set; }
        public virtual Beer Beer { get; set; }
        public virtual short Score { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime Date { get; set; }
    }

    public class Style
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Category Category { get; set; }
    }

    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime Modified { get; set; }
    }

    public class Brewer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ContactDetails Contact { get; set; }
    }
    
    public class ContactDetails
    {
        public virtual int Id { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Website { get; set; }
    }

    public interface IBeerDataSource
    {
        IQueryable<Beer> Beers { get; }
        IQueryable<Entry> Entries { get; }
    }
}

using System;

namespace BeerApp.Models
{
    public class Entry
    {
        public virtual int Id { get; set; }
        public virtual Beer Beer { get; set; }
        public virtual short Score { get; set; }
        public virtual string Comment { get; set; }
        public virtual DateTime Date { get; set; }
    }
}

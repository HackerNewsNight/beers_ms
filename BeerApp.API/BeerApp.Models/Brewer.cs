namespace BeerApp.Models
{
    public class Brewer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ContactDetails Contact { get; set; }
    }
}
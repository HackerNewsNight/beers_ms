namespace BeerApp.Models
{
    public class Beer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Brewer Brewer { get; set; }
        public virtual Style Style { get; set; }
    }
}

namespace BeerApp.Models
{
    public class Style
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Category Category { get; set; }
    }

}

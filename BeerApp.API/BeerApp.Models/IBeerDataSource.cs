using System.Linq;

namespace BeerApp.Models
{
    public interface IBeerDataSource
    {
        IQueryable<Beer> Beers { get; }
        IQueryable<Entry> Entries { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Brewer> Brewers { get; }
        IQueryable<Style> Styles { get; }
    }
}
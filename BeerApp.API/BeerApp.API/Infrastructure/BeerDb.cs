using System.Data.Entity;
using System.Linq;
using BeerApp.Domain;

namespace BeerApp.Infrastructure
{
    public class BeerDb : DbContext, IBeerDataSource
    {
        public BeerDb() : base("DefaultConnection")
        {
        }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brewer> Brewers { get; set; }
        public DbSet<Style> Styles { get; set; }

        IQueryable<Beer> IBeerDataSource.Beers
        {
            get { return Beers; }
        }

        IQueryable<Entry> IBeerDataSource.Entries
        {
            get { return Entries; }
        }

        IQueryable<Category> IBeerDataSource.Categories
        {
            get { return Categories;  }
        }

        IQueryable<Brewer> IBeerDataSource.Brewers
        {
            get { return Brewers;  }
        }

        IQueryable<Style> IBeerDataSource.Styles
        {
            get { return Styles;  }
        }
    }
}
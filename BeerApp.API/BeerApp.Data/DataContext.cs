using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerApp.Data.Configuration;
using BeerApp.Models;

namespace BeerApp.Data
{
    public class DataContext : DbContext
    {
        static DataContext()
        {
            Database.SetInitializer(new CustomDatabaseInitialiser());
        }

        public DataContext() : base(ConnectionStringName) { }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Brewer> Breweries { get; set; }

        private void ApplyRules()
        {
            // Approach via @julielerman
            foreach (var entry in this.ChangeTracker.Entries()
                .Where(e =>e.Entity is IAuditInfo && 
                    (e.State == EntityState.Added) || 
                    (e.State == EntityState.Modified)))
            {
                var e = (IAuditInfo) entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    e.CreatedOn = DateTime.Now; 
                }

                e.ModifiedOn = DateTime.Now; 
            }
        }

        public override int SaveChanges()
        {
            this.ApplyRules();
            return base.SaveChanges();
        }

        public static string ConnectionStringName 
        { 
            get
            {
                if(ConfigurationManager.AppSettings["ConnectionStringName"] != null)
                {
                    return ConfigurationManager.AppSettings["ConnectionStringName"]; 
                }
                return "DefaultConnection";
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BeerConfiguration());
            modelBuilder.Configurations.Add(new BrewerConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new ContactDetailsConfiguration());
            modelBuilder.Configurations.Add(new EntryConfiguration());
            modelBuilder.Configurations.Add(new StyleConfiguration());

            //base.OnModelCreating(modelBuilder);
        }
    }
}

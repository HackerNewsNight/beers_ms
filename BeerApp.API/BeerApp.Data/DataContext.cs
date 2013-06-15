using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerApp.Models;

namespace BeerApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base(ConnectionStringName) { }

        public DbSet<Beer> Beers { get; set;  }

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
    }
}

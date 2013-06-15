using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure; 

namespace BeerApp.Data.Configuration
{
    public class CustomDatabaseInitialiser : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            
            
            base.Seed(context);
        }

        public CustomDatabaseInitialiser()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Data.Entity.Migrations;
using BeerApp.Domain;

namespace BeerApp.API.Migrations
{

    public static class Extensions
    {
        public static XElement ColumnValue(this XElement element, string column)
        {
            var firstOrDefault = element.Descendants("field")
                .FirstOrDefault(x =>
                {
                    var attribute = x.Attribute("name");
                    return attribute != null &&
                           attribute.Value == column;
                });
            return firstOrDefault;
        }

        public static List<XElement> TableElements(this XDocument xmlDoc, string tablename)
        {
            var beers = xmlDoc.Descendants("table_data")
                .Where(x =>
                {
                    var attribute = x.Attribute("name");
                    return attribute != null && attribute.Value == tablename;
                }).Descendants("row").ToList();
            return beers;
        }
    }
    internal sealed class Configuration : DbMigrationsConfiguration<BeerApp.Infrastructure.BeerDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BeerApp.Infrastructure.BeerDb context)
        {
            var xmlDoc = XDocument.Load(@"d:\temp\beers_all.xml");

            var categories = xmlDoc.TableElements("categories")
                .Select(x => new Category()
                {
                    Id = (int)x.ColumnValue("id"),
                    Name = (string)x.ColumnValue("cat_name"),
                    Modified = (DateTime)x.ColumnValue("last_mod")
                }).ToArray();
            context.Categories.AddOrUpdate(x => x.Id, categories);
            context.SaveChanges();

            var styles = xmlDoc.TableElements("styles")
                .Select(x => new Style()
                {
                    Id = (int)x.ColumnValue("id"),
                    Name = (string)x.ColumnValue("style_name"),
                    Category = categories.FirstOrDefault(cat => cat.Id == (int)x.ColumnValue("cat_id"))
                }).ToArray();
            context.Styles.AddOrUpdate(x => x.Id, styles);
            context.SaveChanges();

            var breweries = xmlDoc.TableElements("breweries")
                .Select(x => new Brewer()
                {
                    Id = (int)x.ColumnValue("id"),
                    Name = x.ColumnValue("name").Value,
                    Contact = new ContactDetails()
                    {
                        Address1 = x.ColumnValue("address1").Value,
                        Address2 = x.ColumnValue("address2").Value,
                        City = x.ColumnValue("city").Value,
                        Country = x.ColumnValue("country").Value,
                        Phone = x.ColumnValue("phone").Value,
                        State = x.ColumnValue("state").Value,
                        Website = x.ColumnValue("website").Value
                    }
                }).ToArray();
            context.Brewers.AddOrUpdate(x => x.Id, breweries);
            context.SaveChanges();

            var beers = xmlDoc.TableElements("beers")
                .Select(x => new Beer()
                {
                    Id = (int)x.ColumnValue("id"),
                    Name = x.ColumnValue("name").Value,
                    Style = styles.FirstOrDefault(style => style.Id == (int)x.ColumnValue("id")),
                    Brewer = breweries.FirstOrDefault(brewer => brewer.Id == (int)x.ColumnValue("id"))
                }).ToArray();
            context.Beers.AddOrUpdate(x => x.Id, beers);
            context.SaveChanges();
        }
            /* todo: dmanners - call in the data from the csv files. */
            //  This method will be called after migrating to the latest version.

            //var xmlDoc = XDocument.Load(@"d:\temp\beers_all.xml");
            //var q = from data in xmlDoc.Descendants("table_data")
            //        let attribute = data.Attribute("name")
            //        where attribute != null && attribute.Value == "beers"
            //        select data; 
            
            //IEnumerable<XElement> beers =
            //    from c in xmlDoc.Elements("table_data")
            //    where (string)c.Attribute("name") == "beers"
            //    select c;
            
            //var categories = PopulateCategories(context);
            //var styles = PopulateStyles(context, categories);
            //var brewers = PopulateBrewers(context);
            //var beers = OpenDbBeers(context, styles, brewers);
            
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        //private static string[] OpenDbBeers(BeerDb context, IEnumerable<Style> styles, IEnumerable<Brewer> brewers)
        //{
        //    var openDbBeers = File.ReadAllLines(@"d:\temp\beers.csv");
        //    // "id","brewery_id","name","cat_id","style_id","abv","ibu","srm","upc","filepath","descript","last_mod"
        //    var beers = (from line in openDbBeers
        //                 let data = line.Split(',')
        //                 select new
        //                            {
        //                                id = Int32.Parse(data[0]),
        //                                brewery_id = data[1],
        //                                name = data[2],
        //                                cat_id = data[3],
        //                                style_id = data[4],
        //                                abv = data[5],
        //                                ibu = data[6],
        //                                srm = data[7],
        //                                upc = data[8],
        //                                filepath = data[9],
        //                                descript = data[10],
        //                                last_mod = data[11]
        //                            }).Select(x => new Beer()
        //                                               {
        //                                                   Id = x.id,
        //                                                   Name = x.name,
        //                                                   Style = styles.FirstOrDefault(style => style.Id == x.id),
        //                                                   Brewer = brewers.FirstOrDefault(brewer => brewer.Id == x.id)
        //                                               }).ToArray();
        //    context.Beers.AddOrUpdate(x => x.Id, beers);
        //    context.SaveChanges();
        //    return openDbBeers;
        //}

        //private static IEnumerable<Brewer> PopulateBrewers(BeerDb context)
        //{
        //    var openDbBrewer = File.ReadAllLines(@"d:\temp\breweries.csv");
        //    // id	name	address1	address2	city	state	code	country	phone	website	filepath	descript	last_mod
        //    var breweries = (from line in openDbBrewer
        //                     let data = line.Split(',')
        //                     where data.Count()> 8 && !string.IsNullOrEmpty(data[0])
        //                     select new
        //                                {
        //                                    id = Int32.Parse(data[0]),
        //                                    name = data[1],
        //                                    address1 = data[2],
        //                                    address2 = data[3],
        //                                    city = data[4],
        //                                    state = data[5],
        //                                    code = data[6],
        //                                    country = data[7],
        //                                    phone = data[8],
        //                                    website = data[9],
        //                                    filepath = data[10],
        //                                    descript = data[11]
        //                                }).Select(x => new Brewer()
        //                                                   {
        //                                                       Id = x.id,
        //                                                       Name = x.name,
        //                                                       Contact = new ContactDetails()
        //                                                                     {
        //                                                                         Address1 = x.address1,
        //                                                                         Address2 = x.address2,
        //                                                                         City = x.city,
        //                                                                         Country = x.country,
        //                                                                         Phone = x.phone,
        //                                                                         State = x.state,
        //                                                                         Website = x.website
        //                                                                     }
        //                                                   }).ToArray();
        //    context.Brewers.AddOrUpdate(x => x.Id, breweries);
        //    context.SaveChanges(); 
        //    return breweries; 
        //}

        //private static IEnumerable<Style> PopulateStyles(BeerDb context, IEnumerable<Category> categories)
        //{
        //    var openDbStyles = File.ReadAllLines(@"d:\temp\styles.csv");
        //    // id	cat_id	style_name	last_mod
        //    var styles = (from line in openDbStyles
        //                  let data = line.Split(',')
        //                  select new
        //                             {
        //                                 id = Int32.Parse(data[0]),
        //                                 cat_id = Int32.Parse(data[1]),
        //                                 style_name = data[2],
        //                                 last_mod = data[3]
        //                             }).Select(
        //                                 x =>
        //                                 new Style()
        //                                     {
        //                                         Id = x.id,
        //                                         Name = x.style_name,
        //                                         Category = categories.FirstOrDefault(cat => cat.Id == x.cat_id)
        //                                     }).ToArray();
        //    context.Styles.AddOrUpdate(x => x.Id, styles);
        //    context.SaveChanges();
        //    return styles;
        //}

        //private static IEnumerable<Category> PopulateCategories(BeerDb context)
        //{
        //    var openDbCategories = File.ReadAllLines(@"d:\temp\categories.csv");
        //    // id	cat_name	last_mod
        //    var categories = (from line in openDbCategories
        //                      let data = line.Split(',')
        //                      select new
        //                                 {
        //                                     id = Int32.Parse(data[0]),
        //                                     cat_name = data[1],
        //                                     last_mod = DateTime.Parse(data[2])
        //                                 }).Select(x => new Category()
        //                                                    { 
        //                                                        Id = x.id,
        //                                                        Name = x.cat_name,
        //                                                        Modified = x.last_mod
        //                                                    }).ToArray();
        //    context.Categories.AddOrUpdate(x => x.Id, categories);
        //    context.SaveChanges(); 
        //    return categories;
        //}
 
}

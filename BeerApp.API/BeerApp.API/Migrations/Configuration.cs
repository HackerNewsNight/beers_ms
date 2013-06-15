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
                                     Id = (int) x.ColumnValue("id"),
                                     Name = (string) x.ColumnValue("cat_name"),
                                     Modified = (DateTime) x.ColumnValue("last_mod")
                                 }).ToArray();
            context.Categories.AddOrUpdate(x => x.Id, categories);
            context.SaveChanges();

            var styles = xmlDoc.TableElements("styles")
                .Select(x => new Style()
                                 {
                                     Id = (int) x.ColumnValue("id"),
                                     Name = (string) x.ColumnValue("style_name"),
                                     Category =
                                         categories.FirstOrDefault(cat => cat.Id == (int) x.ColumnValue("cat_id"))
                                 }).ToArray();
            context.Styles.AddOrUpdate(x => x.Id, styles);
            context.SaveChanges();

            var breweries = xmlDoc.TableElements("breweries")
                .Select(x => new Brewer()
                                 {
                                     Id = (int) x.ColumnValue("id"),
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
                                     Id = (int) x.ColumnValue("id"),
                                     Name = x.ColumnValue("name").Value,
                                     Style = styles.FirstOrDefault(style => style.Id == (int) x.ColumnValue("id")),
                                     Brewer = breweries.FirstOrDefault(brewer => brewer.Id == (int) x.ColumnValue("id"))
                                 }).ToArray();
            context.Beers.AddOrUpdate(x => x.Id, beers);
            context.SaveChanges();
        }
    }

}

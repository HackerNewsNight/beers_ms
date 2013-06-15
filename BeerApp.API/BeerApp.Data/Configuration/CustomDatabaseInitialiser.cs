﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity;
using System.Reflection;
using System.Xml.Linq;
using BeerApp.Models;

namespace BeerApp.Data.Configuration
{
    public class CustomDatabaseInitialiser : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            const string resourceName = "BeerApp.Data.beers.xml";
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            var xmlDoc = XDocument.Load(stream);

            var categories = xmlDoc.TableElements("categories")
                .Select(x => new Category()
                {
                    Id = (int)x.ColumnValue("id"),
                    Name = (string)x.ColumnValue("cat_name"),
                }).ToArray();
            context.Categories.AddOrUpdate(x => x.Id, categories);
            context.SaveChanges();

            var styles = xmlDoc.TableElements("styles")
                .Select(x => new Style()
                {
                    Id = (int)x.ColumnValue("id"),
                    Name = (string)x.ColumnValue("style_name"),
                    Category =
                        categories.FirstOrDefault(cat => cat.Id == (int)x.ColumnValue("cat_id"))
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
            context.Breweries.AddOrUpdate(x => x.Id, breweries);
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
            
            //base.Seed(context);
        }

        public CustomDatabaseInitialiser()
        {

        }
    }

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
}

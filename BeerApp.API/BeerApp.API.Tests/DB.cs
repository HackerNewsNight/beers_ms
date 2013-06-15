﻿using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerApp.Web.API.Tests
{
    [TestClass]
    public class DB
    {
        [TestMethod]
        public void DB_Load()
        {
            var xmlDoc = XDocument.Load(@"d:\temp\beers_all.xml");
            var beers = xmlDoc.Descendants("table_data")
                .Where(x =>
                {
                    var attribute = x.Attribute("name");
                    return attribute != null && attribute.Value == "beers";
                }).Descendants("row").ToList();

            foreach (var xElement in beers)
            {
                Console.Write(xElement);
            }
        }
    }
}

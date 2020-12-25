using BreweryLibraryClasses.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BreweryTests
{
    [TestFixture]
    class AddressTest
    {

        BitsContext bitsContext;
        Address ad;
        Address ad2;
        List<Address> adresses;
        [SetUp]
        public void Setup()
        {
            bitsContext = new BitsContext();
            bitsContext.Database.ExecuteSqlRaw("call usp_testingResetDataProviders()");
        }
        [Test]
        public void GetAllTest()
        {
            adresses = bitsContext.Address.OrderBy(sa => sa.AddressId).ToList();
            Assert.AreEqual(7, adresses.Count);
            Assert.AreEqual(1, adresses[0].AddressId);
            PrintAll(adresses);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            ad = bitsContext.Address.Find(5);
            Assert.IsNotNull(ad);
            Assert.AreEqual("10022", ad.Zipcode);
            Console.WriteLine(ad);
        }

        [Test]
        public void GetUsingWhere()
        {
            adresses = bitsContext.Address.Where(sa => sa.City.StartsWith("Y")).OrderBy(sa => sa.City).ToList();
            Assert.AreEqual(1, adresses.Count);
            Assert.AreEqual("WA", adresses[0].State);
            PrintAll(adresses);
        }

        [Test]
        public void CreateTest()
        {
            ad = new Address
            {
                AddressId = 9,
                StreetLine1 = "Testing line fake",
                StreetLine2 = "",
                City = "Florence",
                State = "HI",
                Zipcode = "99999",
                Country = "USA"
            };
            bitsContext.Add(ad);
            bitsContext.SaveChanges();
            Assert.IsNotNull(bitsContext.Address.Find(9));
        }

        [Test]
        public void DeleteTest()
        {
            // First create and then delete, so I don't care about Foreing Key constraints
            ad = new Address
            {
                AddressId = 9,
                StreetLine1 = "Testing line fake",
                StreetLine2 = "",
                City = "Florence",
                State = "HI",
                Zipcode = "99999",
                Country = "USA"
            };
            bitsContext.Add(ad);
            bitsContext.SaveChanges();
            //Then delete
            ad = bitsContext.Address.Find(9);
            bitsContext.Address.Remove(ad);
            bitsContext.SaveChanges();
            Assert.IsNull(bitsContext.Address.Find(9));
        }
       

        [Test]
        public void UpdateTest()
        {
            // First create a new one .....
            ad = new Address
            {
                AddressId = 9,
                StreetLine1 = "Testing line fake",
                StreetLine2 = "",
                City = "Florence",
                State = "HI",
                Zipcode = "99999",
                Country = "USA"
            };
            bitsContext.Add(ad);
            bitsContext.SaveChanges();
            Assert.IsNotNull(bitsContext.Address.Find(9));
            // Then update it..

            ad2 = bitsContext.Address.Find(9);
            ad2.AddressId = 9;
            ad2.StreetLine1 = "Update Testing";
            ad2.StreetLine2 = "test2";
            ad2.City = "Gotham";
            ad2.State = "NJ";
            ad2.Zipcode = "66666";
            ad2.Country = "Guatemala";
            bitsContext.Address.Update(ad2);
            ad2 = bitsContext.Address.Find(9);
            Assert.AreEqual("Gotham", ad2.City);
        }





        public void PrintAll(List<Address> addresses)
        {
            foreach (Address s in addresses)
            {
                Console.WriteLine(s);
            }
        }


    }
}

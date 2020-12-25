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
    class AddressTypeTest
    {

        BitsContext bitsContext;
        AddressType at;
        AddressType at2;

        List<AddressType> types;

        [SetUp]
        public void Setup()
        {
          bitsContext = new BitsContext();
           bitsContext.Database.ExecuteSqlRaw("call usp_testingResetDataProviders()");
        }
        [Test]
        public void GetAllTest()
        {
            types = bitsContext.AddressType.OrderBy(sa => sa.Name).ToList();
            Assert.AreEqual(3, types.Count);
            Assert.AreEqual(1, types[0].AddressTypeId);
            PrintAll(types);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            at = bitsContext.AddressType.Find(2);
            Assert.IsNotNull(at);
            Assert.AreEqual("mailing", at.Name);
            Console.WriteLine(at);
        }

        [Test]
        public void GetUsingWhere()
        {
            types = bitsContext.AddressType.Where(at => at.Name.StartsWith("s")).OrderBy(at => at.Name).ToList();
            Assert.AreEqual(1, types.Count);
            Assert.AreEqual(3, types[0].AddressTypeId);
            PrintAll(types);
        }

        [Test]
        public void CreateTest()
        {
            at = new AddressType
            {
                AddressTypeId = 4,
                Name = "Testing"
            };

            bitsContext.Add(at);
            bitsContext.SaveChanges();
            Assert.IsNotNull(bitsContext.AddressType.Find(4));
        }

        [Test]
        public void DeleteTest()
        {
            // FIRST CREATE
            at = new AddressType
            {
                AddressTypeId = 4,
                Name = "Testing"
            };

            bitsContext.Add(at);
            bitsContext.SaveChanges();
            // THEN DESTROY
            at = bitsContext.AddressType.Find(4);
            bitsContext.AddressType.Remove(at);
            bitsContext.SaveChanges();
            Assert.IsNull(bitsContext.AddressType.Find(4));
        }
       

        [Test]
        public void UpdateTest()
        {
            // I prefer to create one and then update it..
            at = new AddressType
            {
                AddressTypeId = 4,
                Name = "Testing"
            };

            bitsContext.Add(at);
            bitsContext.SaveChanges();

            at = bitsContext.AddressType.Find(4);
            at.Name = "Updated";
            bitsContext.AddressType.Update(at);
            at2 = new AddressType();
            at2 = bitsContext.AddressType.Find(4);
            Assert.AreEqual("Updated", at2.Name);
        }





        public void PrintAll(List<AddressType> types)
        {
            foreach (AddressType s in types)
            {
                Console.WriteLine(s);
            }
        }


    }
}

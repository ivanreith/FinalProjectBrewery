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
    class SupplierAddressTest
    {

        BitsContext bitsContext;
        SupplierAddress sa;
        SupplierAddress sa2;
        List<SupplierAddress> adresses;
        [SetUp]
        public void Setup()
        {
            bitsContext = new BitsContext();
            bitsContext.Database.ExecuteSqlRaw("call usp_testingResetDataProviders()");
        }
        [Test]
        public void GetAllTest()
        {
            adresses = bitsContext.SupplierAddress.OrderBy(sa => sa.Address).ToList();
            Assert.AreEqual(12, adresses.Count);
            Assert.AreEqual(1, adresses[0].AddressId);
            PrintAll(adresses);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            sa = bitsContext.SupplierAddress.Find(2,2,2);
            Assert.IsNotNull(sa);
            Assert.AreEqual(2, sa.AddressTypeId);
            Console.WriteLine(sa);
        }

        [Test]
        public void GetUsingWhere()
        {
            adresses = bitsContext.SupplierAddress.Where(sa => sa.AddressId.Equals(4)).OrderBy(sa => sa.AddressId).ToList();
            Assert.AreEqual(1, adresses.Count);
            Assert.AreEqual(2, adresses[0].AddressTypeId);
            PrintAll(adresses);
        }
        [Test]
        public void DeleteTest()
        {
            // CREATE
            sa = new SupplierAddress
            {
                SupplierId = 3,
                AddressId = 3,
                AddressTypeId = 3
            };
            bitsContext.Add(sa);
            bitsContext.SaveChanges();

            //Destroy
            sa = bitsContext.SupplierAddress.Find(3 , 3 , 3);
            bitsContext.SupplierAddress.Remove(sa);
            bitsContext.SaveChanges();
            Assert.IsNull(bitsContext.SupplierAddress.Find(3, 3, 3));
        }
        [Test]
        public void CreateTest()
        {
            sa = new SupplierAddress
            {
                SupplierId = 3,
                AddressId = 3,
                AddressTypeId = 3
            };
            bitsContext.Add(sa);
            bitsContext.SaveChanges();
            Assert.IsNotNull(bitsContext.SupplierAddress.Find(3, 3, 3));
        }

        [Test]
        public void UpdateTest()
        {
            //CREATE
            sa = new SupplierAddress
            {
                SupplierId = 3,
                AddressId = 3,
                AddressTypeId = 3
            };
            bitsContext.Add(sa);
            bitsContext.SaveChanges();
            Assert.IsNotNull(bitsContext.SupplierAddress.Find(3, 3, 3));
            //UPDATE
            sa = bitsContext.SupplierAddress.Find(3, 3, 3);
            sa.AddressTypeId = 3;
            bitsContext.SupplierAddress.Update(sa);          
            sa2 = bitsContext.SupplierAddress.Find(3,3,3);
            Assert.AreEqual(3, sa2.AddressTypeId);
        }



        public void PrintAll(List<SupplierAddress> addresses)
        {
            foreach (SupplierAddress s in addresses)
            {
                Console.WriteLine(s);
            }
        }


    }
}

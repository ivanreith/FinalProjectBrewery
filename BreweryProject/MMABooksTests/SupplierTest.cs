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
    class SupplierTest
    {

        BitsContext bitsContext;
        Supplier sup2;
        Supplier sup;
      
        List<Supplier> suppliers;

        [SetUp]
        public void Setup()
        {
            bitsContext = new BitsContext();
            bitsContext.Database.ExecuteSqlRaw("call usp_testingResetDataProviders()");
        }
        [Test]
        public void GetAllTest()
        {
            suppliers = bitsContext.Supplier.OrderBy(sa => sa.SupplierId).ToList();
            Assert.AreEqual(6, suppliers.Count);
            Assert.AreEqual("BSG Craft Brewing", suppliers[0].Name);
            PrintAll(suppliers);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            sup = bitsContext.Supplier.Find(5);
            Assert.IsNotNull(sup);
            Assert.AreEqual("John I. Haas, Inc.", sup.Name);
            Console.WriteLine(sup);
        }

        [Test]
        public void GetUsingWhere()
        {
            suppliers = bitsContext.Supplier.Where(sa => sa.Name.StartsWith("M")).OrderBy(sa => sa.Name).ToList();
            Assert.AreEqual(1, suppliers.Count);
            Assert.AreEqual(2, suppliers[0].SupplierId);
            PrintAll(suppliers);
        }

     

        [Test]
        public void UpdateTest()
        {
            // First create a new one .....
           sup = new Supplier
            {
                SupplierId = 9,
                Name = "Testing line fake",
                Phone = "96969696969",
                Email = "noemail.noemail.not",
                Website = "www.fakewebforsure.fake",
                ContactFirstName = "Peter",
                ContactLastName = "Parker",
                ContactPhone = "123456789",
                ContactEmail = "noemail.noemail.not",
                Note = "no comments"
            };
            bitsContext.Add(sup);
            bitsContext.SaveChanges();
            Assert.IsNotNull(bitsContext.Supplier.Find(9));
            // Then update it..
            sup2 = bitsContext.Supplier.Find(9);

            sup2.SupplierId = 9;
            sup2.Name = "Testing line fake2";
            sup2.Phone = "222222222";
            sup2.Email = "noemail.noemail.not22";
            sup2.Website = "22www.fakewebforsure.fake";
            sup2.ContactFirstName = "Bruce";
            sup2.ContactLastName = "Wayne";
            sup2.ContactPhone = "123456789";
            sup2.ContactEmail = "noemail.noemail.not";
            sup2.Note = "no comments";
            
            bitsContext.Supplier.Update(sup2);
            sup2 = bitsContext.Supplier.Find(9);
            Assert.AreEqual("Wayne", sup2.ContactLastName);
        }

        [Test]
        public void CreateTest()
        {
             sup = new Supplier
            {
                SupplierId = 9,
                Name = "Testing line fake",
                Phone = "96969696969",
                Email = "noemail.noemail.not",
                Website = "www.fakewebforsure.fake",
                ContactFirstName = "Peter",
                ContactLastName = "Parker",
                ContactPhone = "123456789",
                ContactEmail = "noemail.noemail.not",
                Note = "no comments"
            };
            bitsContext.Add(sup);
            bitsContext.SaveChanges();
            Assert.IsNotNull(bitsContext.Supplier.Find(9));
        }

        [Test]
        public void DeleteTest()
        {
            // same thing xcreate one wo FK constraints 
            sup = new Supplier
            {
                SupplierId = 9,
                Name = "Testing line fake",
                Phone = "96969696969",
                Email = "noemail.noemail.not",
                Website = "www.fakewebforsure.fake",
                ContactFirstName = "Peter",
                ContactLastName = "Parker",
                ContactPhone = "123456789",
                ContactEmail = "noemail.noemail.not",
                Note = "no comments"
            };
            bitsContext.Add(sup);
            bitsContext.SaveChanges();
            // so it can be deleted
            sup = bitsContext.Supplier.Find(9);
            bitsContext.Supplier.Remove(sup);
            bitsContext.SaveChanges();
            Assert.IsNull(bitsContext.Supplier.Find(9));
        }





        public void PrintAll(List<Supplier> addresses)
        {
            foreach (Supplier s in addresses)
            {
                Console.WriteLine(s);
            }
        }


    }
}

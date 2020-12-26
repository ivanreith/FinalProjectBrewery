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
    class InmgredientInventoryAdditionsTest
    {

        BitsContext bitsContext;
        IngredientInventoryAddition ad;
        IngredientInventoryAddition ad2;
        List<IngredientInventoryAddition> aditions;
        [SetUp]
        public void Setup()
        {
            bitsContext = new BitsContext();
            bitsContext.Database.ExecuteSqlRaw("call usp_testingResetDataProviders()");
        }
        [Test]
        public void GetAllTest()
        {
            aditions = bitsContext.IngredientInventoryAddition.OrderBy(sa => sa.IngredientInventoryAdditionId).ToList();
            Assert.AreEqual(36, aditions.Count);
            Assert.AreEqual(1, aditions[0].IngredientInventoryAdditionId);
            PrintAll(aditions);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            ad = bitsContext.IngredientInventoryAddition.Find(5);
            Assert.IsNotNull(ad);
            Assert.AreEqual(914, ad.IngredientId);
            Console.WriteLine(ad);
        }

        [Test]
        public void GetUsingWhere()
        {
            aditions = bitsContext.IngredientInventoryAddition.Where(sa => sa.SupplierId.Equals(2)).OrderBy(sa => sa.Quantity).ToList();
            Assert.AreEqual(12, aditions.Count);
            Assert.AreEqual(2, aditions[0].SupplierId);
            PrintAll(aditions);
        }

        [Test]
        public void CreateTest()
        {
            ad = new IngredientInventoryAddition
            {
                IngredientInventoryAdditionId = 99,
                IngredientId = 2,
                SupplierId = 3,
                Quantity = 66,
                QuantityRemaining = 66,
                UnitCost = 37,
            
            };
            bitsContext.Add(ad);
            bitsContext.SaveChanges();
            Assert.IsNotNull(bitsContext.IngredientInventoryAddition.Find(9));
        }

        [Test]
        public void DeleteTest()
        {
            // First create and then delete, so I don't care about Foreing Key constraints
           /* ad = new IngredientInventoryAddition
            {
                IngredientInventoryAdditionId = 99,
                IngredientId = 2,
                SupplierId = 3,
                Quantity = 66,
                QuantityRemaining = 66,
                UnitCost = 37,
            };
            bitsContext.Add(ad);
            bitsContext.SaveChanges();  */
            //Then delete
            ad = bitsContext.IngredientInventoryAddition.Find(102);
            bitsContext.IngredientInventoryAddition.Remove(ad);
            bitsContext.SaveChanges();
            Assert.IsNull(bitsContext.IngredientInventoryAddition.Find(102));
        }
       

        [Test]
        public void UpdateTest()
        {
            // First create a new one .....
            ad = new IngredientInventoryAddition
            {
                IngredientInventoryAdditionId = 102,
                IngredientId = 2,
                SupplierId = 3,
                Quantity = 66,
                QuantityRemaining = 66,
                UnitCost = 37,
            };
            bitsContext.Add(ad);
            bitsContext.SaveChanges();
            Assert.IsNotNull(bitsContext.IngredientInventoryAddition.Find(102));
            // Then update it..

            ad2 = bitsContext.IngredientInventoryAddition.Find(102);
            ad2.IngredientInventoryAdditionId = 102;
            ad2.IngredientId = 7;
            ad2.SupplierId = 3;
            ad2.Quantity = 77;
            ad2.QuantityRemaining = 77;
            ad2.UnitCost = 77;
            bitsContext.IngredientInventoryAddition.Update(ad2);
            
            ad2 = bitsContext.IngredientInventoryAddition.Find(102);
            Assert.AreEqual(7, ad2.IngredientId);
        }





        public void PrintAll(List<IngredientInventoryAddition> aditions)
        {
            foreach (IngredientInventoryAddition s in aditions)
            {
                Console.WriteLine(s);
            }
        }


    }
}

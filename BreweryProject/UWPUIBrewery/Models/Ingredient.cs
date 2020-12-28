using System;
using System.Collections.Generic;

namespace BreweryLibraryClasses.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            IngredientInventoryAddition = new HashSet<IngredientInventoryAddition>();
     
        }

        public int IngredientId { get; set; }
        public string Name { get; set; }
        public int? Version { get; set; }
        public int IngredientTypeId { get; set; }
        public double OnHandQuantity { get; set; }
        public int UnitTypeId { get; set; }
        public decimal UnitCost { get; set; }
        public double ReorderPoint { get; set; }
        public string Notes { get; set; }

   
        public virtual ICollection<IngredientInventoryAddition> IngredientInventoryAddition { get; set; }
       
    }
}

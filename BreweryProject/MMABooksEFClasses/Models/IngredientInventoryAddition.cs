using System;
using System.Collections.Generic;

namespace BreweryLibraryClasses.Models
{
    public partial class IngredientInventoryAddition
    {
        public int IngredientInventoryAdditionId { get; set; }
        public int IngredientId { get; set; }
        public int SupplierId { get; set; }
        public double Quantity { get; set; }
        public double? QuantityRemaining { get; set; }
        public decimal UnitCost { get; set; }

        public override string ToString()
        {
            return IngredientInventoryAdditionId + ", " + IngredientId + ", " +
                SupplierId + ", " + Quantity + ", " +
                QuantityRemaining + ", " + UnitCost ;
        }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}

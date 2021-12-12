using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp_Karol;
using ToDoApp_Karol.Domain.Common;
using ToDoApp_Karol.Domain.Enums;

namespace ToDoApp_Karol.Domain.Entity
{
    public class GroceriesTasks: Tasks
    {
        public IngredientsEnum IngredientName { get; set; }
        public double Price { get; set; }
        
        public int Amount { get; set; }

        private double TotalPay = Price * Convert.ToDouble(Amount);
        //why cannot we reference properties in private variable definition?
    }
}

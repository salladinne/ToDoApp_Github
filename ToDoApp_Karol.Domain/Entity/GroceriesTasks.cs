﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp_Karol;
using ToDoApp_Karol.Domain.Common;


namespace ToDoApp_Karol.Domain.Entity
{
    public class GroceriesTasks: Tasks
    {
        public double Price { get; set; }
        
        public int Amount { get; set; }
        
        
    }
}

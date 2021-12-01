using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp_Karol.Domain.Common;
using ToDoApp_Karol.Domain.Enums;

namespace ToDoApp_Karol.Domain.Entity
{
    public class CookingTasks: Tasks
    {
        public List<IngredientsEnum> Ingredients { get; }
        //jeśli Ingredients mają się generować na podstawie Recipe, to chyba powinniśmy dać tylko gettera (bez settera)
        public string Recipe { get; set; }
        //można zrobić free text, który szuka po słowach kluczowych z enuma IngredientsEnum i jeśli występują to automatycznie dodaje je do property Ingredients
        //public int CookingTime { get; set; }

    }
}

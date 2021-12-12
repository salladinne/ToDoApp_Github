using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.App.Abstract
{
    public interface IService<T> // <T,Q> dla 2 zmiennnych typów generycznych
    {
        //public List<T> TaskList { get; set; }

        //public List<Q> PeopleList { get; set; }

        public T GetItem(int id);
        public List<T> GetItem(string title);
        public int DeleteItem(int id);
        public int UpdateItem(T item);
        public int AddNewItem(T item);
        public List<T> GetAllItems();
    }
}


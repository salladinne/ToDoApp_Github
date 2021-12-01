using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp_Karol.App.Abstract
{
    public interface IService <T>
    {
        public List<T> TaskList { get; set; }

        public List<T> PeopleList { get; set; }

        public T GetTask(int id);
        public List<T> GetTask(string title);
        public void DeleteTask(int id);
        public void UpdateTask(int id, string title, string description);
        public int AddNewTask(T task);
        public List<T> GetAllTasks();

    }
}

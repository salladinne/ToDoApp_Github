using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp_Karol.Domain.Common;

namespace ToDoApp.App.Abstract
{
    public abstract class TaskService<T, Q> : IService<T> where T : Tasks, IService<Q> where Q: Persons
        //dlaczego musimy to zapisać tak, a nie po prostu public abstract class TaskService<Tasks> : IService<T> ?
    {
        private List<T> TaskList { get; set; }
        private List<Q> PersonList { get; set; }
        private int id = 0;

        public TaskService()
        {
            TaskList = new List<T>();
            PersonList = new List<Q>();
        }
        public T GetItem(int id)
        {
            foreach (var item in TaskList)
            {
                if (item.TaskID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public List<T> GetItem(string title)
        {
            return TaskList.Where(t => t.Title.Contains(title)).ToList();
        }

        public int DeleteItem(int id)
        {
            if (GetItem(id).TaskID == -1)
                //nie powinno byc TaskID != -1?
            {
                var task = GetItem(id);
                TaskList.Remove(task);
                return id;
            }
            return -1;
        }

        public virtual int UpdateItem(T item)
            //dlaczego virtual?
            //dlaczego tu znowu mamy item a nie taskID, newTitle, new Description itp, skoro nie zmienilismy tego w taskmanagerze?
        {
            if (item.TaskID != null && item.Title != null && item.Description != null)
                //nie powinnismy miec tutaj TaskID!= -1 zamiast null?
            {
                var task = GetItem(id);
                task.Title = item.Title;
                task.Description = item.Description;
                return id;
            }
            return -1;
        }

        public int AddNewItem(T task)
        {
            if (task.Title != null && task.Description != null)
            //nie powinnismy miec tutaj TaskID!= -1 zamiast null?
            {
                task.TaskID = id++;
                task.CreatedTime = DateTime.Now;
                TaskList.Add(task);
                return task.TaskID;
            }
            return -1;
        }

        public List<T> GetAllItems()
        {
            return TaskList;
        }

        public List<Q> GetAllPeople()
        {
            return PersonList;
        }

        public void AssignPersonToTask(string name, int id)
        {
            var person = GetPerson(name);
            var task = GetItem(id);
            if (task != null && person != null)
            {
                person.Duties.Add(task);
            }
        }
        public void AssignPersonToTask(string name, string title)
        {
            var person = GetPerson(name);
            var setOfTasks = GetItem(title);
            if (setOfTasks.Count != 0 && person != null)
            {
                person.Duties.Concat(setOfTasks);
            }
        }

        public Q GetPerson(string name)
        {
            foreach (var person in PersonList)
            {
                if (person.Name == name)
                {
                    return person;
                }
            }
            return null;
        }

        public void AddNewPerson(Q person)
        {
            if (person.Name != null && person.Age >= 0)
            {
                person.Duties = new List<Tasks>();
                PersonList.Add(person);
            }
            else
            {
                Console.WriteLine("Podano błędne dane");
                Console.ReadKey();
            }
        }
        public DateTime CheckTaskTime(T task)
        {
            DateTime nowTime = DateTime.Now;
            nowTime.AddMinutes(Convert.ToDouble(task.TaskPerformanceTime));
            return nowTime;
        }
    }
}


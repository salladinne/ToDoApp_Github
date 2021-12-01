using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp_Karol;
using ToDoApp_Karol.App.Abstract;
using ToDoApp_Karol.Domain.Entity;

namespace ToDoApp_Karol.App.Concrete
{
    public class CleaningTaskService : IService<CleaningTasks>
    {
        public List<CleaningTasks> TaskList { get; set; }

        public List<FamilyMember> PeopleList { get; set; }

        private int id = 0;

        public CleaningTaskService()
        {
            CleaningTaskService cleaningTaskService = new CleaningTaskService();
            TaskList = new List<CleaningTasks>();
            PeopleList = new List<FamilyMember>();
        }

        public int AddNewTask(string title, string description, CleaningActivities taskType)
        {
            if (title!= null && description!= null)   
            {
                CleaningTasks task = new CleaningTasks();
                task.Title = title;
                task.Description = description;
                task.TaskId = id++;
                task.CreatedTime = DateTime.Now;
                task.CleaningActivity = taskType;
                task.TaskPerformanceTime = (int)taskType;
                //czemu nie mogę rzutować wartości enuma na inta?
                TaskList.Add(task);
                return task.TaskId;
            }
            return -1;
        }

        public int DeleteTask(int id)
        {
            if (GetTask(id) == -1)
            {
                var task = GetTask(id);
                TaskList.Remove(task);
                return id;
            }
            return -1;
        }

        public int UpdateTask(int id, string title, string description, CleaningActivites taskType, int performanceTime)
        {
            if (id!=null && title!=null && description!=null)
            {
                task = GetTask(id);
                task.Title = title;
                task.Description = description;
                task.CleaningActivity = taskType;
                task.TaskPerformanceTime = performanceTime+(int)taskType;
                return id;
            }
            return -1;
        }

        public List<CleaningTasks> GetAllTasks()
        {
            return TaskList;
        }

        public CleaningTasks GetTask(int id)
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

        public List<CleaningTasks> GetTask(string title)
        {
            return TaskList.Where(t=>t.Title.Contains(title)).ToList();
        }

        public DateTime CheckTime(CleaningTasks cleaningTask)
        {
            DateTime nowTime = DateTime.Now;
            nowTime.AddMinutes(Convert.ToDouble(cleaningTask.TaskPerformanceTime));
            return nowTime;
        }

        public List<FamilyMember> GetAllPeople()
        {
            return PeopleList;
        }

        public void AssignPersonToTask(string name, int id)
        {
            FamilyMember person = GetPerson(name);
            CleaningTasks cleaningTask = GetTask(id);
            if (cleaningTask != null && person != null)
            {
                person.Duties.Add(cleaningTask);
            }
        }

        public void AssignPersonToTask(string name, string title)
        {
            FamilyMember person = GetPerson(name);
            List<CleaningTasks> setOfTasks = GetTask(title);
            if (setOfTasks.Count!=0 && person != null)
            {
                person.Duties.Add(setOfTasks);
            }
        }

        public FamilyMember GetPerson(string name)
        {
            foreach (var person in PeopleList)
            {
                if (person.Name == name)
                {
                    return person;
                }
            }
            return null;
        }

        public void AddNewPerson(string name, int age)
        {
            if (name != null)
            {
                FamilyMember person = new FamilyMember();
                person.Name = name;
                person.Age = age;
                person.Duties = new List<CleaningTasks>();
            }
            else
            {
                Console.WriteLine("Nie podano imienia");
                Console.ReadKey();
                return;
            }
        }
    }
}

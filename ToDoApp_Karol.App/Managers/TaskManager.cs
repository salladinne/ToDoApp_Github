using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp_Karol;
using ToDoApp_Karol.App.Concrete;
using ToDoApp_Karol.Domain.Entity;


namespace ToDoApp_Karol.App.Managers
{
    public class TaskManager
    {
        private readonly CleaningTaskService cleaningTaskService;
        public TaskManager()
        {
            cleaningTaskService= new CleaningTaskService();
        }

        public void AddTask()
        {
            string title, description;
            Console.WriteLine("Podaj tytuł");
            title = Console.ReadLine().ToString();
            Console.WriteLine("Podaj opis");
            description = Console.ReadLine().ToString();
            Console.WriteLine("Wybierz rodzaj sprzątania:");
            //int counter = 0;
            //dlaczego jak tu dam private to w poniższym foreachu już mi nie rozpoznaje?
            foreach (var cleaningType in CleaningActivites)
            {
                Console.WriteLine(cleaningType);
            }
            string chosenType = Console.ReadLine().ToString();


            //taskType = Console.ReadLine().ToString();
            cleaningTaskService.AddNewTask(title, description, chosenType);
            Console.WriteLine("Dodano taska");
            Console.ReadKey(); 
        }

        public void ShowAll()
        {
            List<CleaningTasks> taskList = new List<CleaningTasks>();
            //czy to zainicjalizowanie taskList jest konieczne?
            taskList = cleaningTaskService.GetAllTasks();
            foreach (var task in taskList)
            {
                Console.WriteLine(@"ID to {0}, tytuł: {1}, opis: {2}, typ: {3}, czas procesowania: {4}", task.TaskID, task.Title, task.Description, task.CleaningActivity, task.TaskPerformanceTime) ;
                if (task.IsCompleted)
                {
                    Console.WriteLine("Zadanie zakończone");
                }
                else
                {
                    Console.WriteLine("W trakcie wykonywania");
                }
                Console.WriteLine(@"Dodano {0}/{1}/{2}",task.CreatedTime.Day, task.CreatedTime.Month, task.CreatedTime.Year);
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public void DeleteTask()
        {
            Console.WriteLine("Podaj numer ID taska do usunięcia, lub jego tytuł");
            string deleteVar = Console.ReadLine().ToString();
            if (!int.TryParse(deleteVar, out int id))
            {
                Console.WriteLine("Podano błędne dane");
                Console.ReadKey();
                return;
            }
            cleaningTaskService.DeleteTask(id);
            Console.WriteLine(@"Usunięto taska o numerze {0}", deleteVar);
            Console.ReadKey();
        }

        public void ShowTask()
        {
            Console.WriteLine("Podaj numer ID taska do pokazania, lub jego tytuł");
            var showVar = Console.ReadLine().ToString();
            System.Type typeOfVar = showVar.GetType();
            string typeOfVar_tostring = typeOfVar.ToString();
            
            switch (typeOfVar_tostring)
            {
                case "System.String":
                    {
                        List<CleaningTasks> taskList = new List<CleaningTasks>();
                        //czy to zainicjalizowanie taskList jest konieczne?
                        taskList = cleaningTaskService.GetTask(showVar);
                        foreach (var task in taskList)
                        {
                            Console.WriteLine(@"ID to {0}, tytuł: {1}, opis: {2}, typ: {3}, czas procesowania: {4}", task.TaskID, task.Title, task.Description, task.CleaningActivity, task.TaskPerformanceTime);
                            if (task.IsCompleted)
                                //jak zrobić zawijanie tekstu?
                            {
                                Console.WriteLine("Zadanie zakończone");
                            }
                            else
                            {
                                Console.WriteLine("W trakcie wykonywania");
                            }
                            Console.WriteLine(@"Dodano {0}/{1}/{2}", task.CreatedTime.Day, task.CreatedTime.Month, task.CreatedTime.Year);
                            Console.WriteLine();
                        }
                        break;
                    }
                case "System.Int32":
                    {
                        CleaningTasks cleaningTask = new CleaningTasks();

                        Console.WriteLine(@"ID to {0}, tytuł: {1}, opis: {2}, typ: {3}, czas procesowania: {4}", cleaningTask.TaskID, cleaningTask.Title, cleaningTask.Description, cleaningTask.CleaningActivity, cleaningTask.TaskPerformanceTime);
                        if (cleaningTask.IsCompleted)
                        {
                            Console.WriteLine("Zadanie zakończone");
                        }
                        else
                        {
                            Console.WriteLine("W trakcie wykonywania");
                        }
                        Console.WriteLine(@"Dodano {0}/{1}/{2}", cleaningTask.CreatedTime.Day, cleaningTask.CreatedTime.Month, cleaningTask.CreatedTime.Year);
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
            Console.ReadKey();
        }

        public void UpdateTask()
        {
            Console.WriteLine("Podaj numer ID taska do pokazania, lub jego tytuł");
            string updateVar = Console.ReadLine().ToString();
            if (!int.TryParse(updateVar, out var taskID))
            {
                Console.WriteLine("Błędne dane!");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Podaj nowy tytuł taska");
            string newTitle = Console.ReadLine().ToString();
            Console.WriteLine("Podaj nowy opis taska");
            string newDescription = Console.ReadLine().ToString();
            Console.WriteLine("Podaj nowy typ taska");
            foreach (var cleaningType in CleaningActivites)
            {
                Console.WriteLine(cleaningType);
            }
            //jak tu ograniczyć tylko do wartości enumów?
            string newType = Console.ReadLine().ToString();
            Console.WriteLine("Podaj nowy czas procesowania");
            string newPerformanceTime = Console.ReadLine().ToString();
            cleaningTaskService.UpdateTask(taskID, newTitle, newDescription, newType, Convert.ToInt32(newPerformanceTime));
            //jak zrobic zeby bylo nieobowiazkowe zmienianie niektorych parametrow, np. performanceTime albo newType?
            Console.WriteLine(@"Udało się zaktualizować dane taska o numrze {0}", updateVar);
            Console.ReadKey();
        }

        public void CheckEstimatedCompletionTime()
        {
            Console.WriteLine("Podaj id taska:");
            string taskID = Console.ReadLine().ToString();
            if (!int.TryParse(taskID, out int id))
            {
                Console.WriteLine("Podano błędne dane");
                Console.ReadKey();
                return;
            }
            CleaningTasks cleaningTask = cleaningTaskService.GetTask(id);
            Console.WriteLine(cleaningTaskService.CheckTaskTime(cleaningTask).ToString());
            Console.ReadKey();
        }

        public void ShowPeople()
        {
            List<FamilyMember> peopleList = new List<FamilyMember>();
            peopleList = cleaningTaskService.GetAllPeople();
            foreach (var person in peopleList)
            {
                Console.WriteLine(@"{0}, w wieku lat: {1}", person.Name, person.Age);
                foreach (var duty in person.Duties)
                {
                    if (duty.IsCompleted)
                    {
                        Console.WriteLine($"Wykonał zadanie {duty.TaskID}");
                    }
                    else
                    {
                        Console.WriteLine($"Wykonuje zadanie {duty.TaskID}");
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public void AssignTask()
        {
            Console.WriteLine("Podaj imię osoby");
            string person = Console.ReadLine().ToString();
            Console.WriteLine("Podaj id lub tytuł taska");
            var taskRef = Console.ReadLine().ToString();
            cleaningTaskService.AssignPersonToTask(person, taskRef);
        }

        public void AddPerson()
        {
            string name, age;
            Console.WriteLine("Jak ma na imię nowa osoba?");
            name = Console.ReadLine().ToString();
            Console.WriteLine("Ile ma lat?");
            age = Console.ReadLine().ToString();
            if (!int.TryParse(age, out int resultAge))
            {
                Console.WriteLine("Podane błędny wiek, proszę podać liczbę");
                Console.ReadKey();
                return;
            }
            cleaningTaskService.AddNewPerson(name, resultAge);
            Console.WriteLine("Dodano osobę");
            Console.ReadKey();
        }

    }
}

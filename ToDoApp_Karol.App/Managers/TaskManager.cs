using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.App.Abstract;
using ToDoApp.App.Concrete;
using ToDoApp_Karol;
using ToDoApp_Karol.Domain.Common;
using ToDoApp_Karol.Domain.Entity;
using ToDoApp_Karol.Domain.Enums;

namespace ToDoApp.App.Managers
{
    public class TaskManager
    {
        private readonly TaskService taskService;
        public TaskManager()
        {
            taskService = new TaskService();
            //czy teraz tu musimy w nawiasach podać Tasks, Persons?:
            //taskService = new TaskService(Tasks, Persons)

        }

        public void AddTask()
        {
            string title, description;
            Console.WriteLine("Podaj tytuł");
            title = Console.ReadLine().ToString();
            Console.WriteLine("Podaj opis");
            description = Console.ReadLine().ToString();
            Console.WriteLine("Podaj typ zadania: sprzątanie, zakupy, gotowanie");
            var taskType = Console.ReadLine().ToString();
            switch (taskType)
            {
                case "sprzątanie":
                    {                        
                        double area;
                        CleaningActivities cleaningActivity = new(); 
                        Console.WriteLine("Podaj pole pracy");
                        if (!double.TryParse(Console.ReadLine().ToString(), out area))
                        {
                            Console.WriteLine("Podano błędne dane.");
                            return;
                        }
                        Console.WriteLine("Wybierz rodzaj sprzątania:");
                        int counter = 1;

                        foreach (var item in Enum.GetNames<CleaningActivities>())
                        {
                            Console.WriteLine("Czy wybierasz {0}? Y/N", item.ToString());
                            var typeAnswer = Console.ReadLine().ToString();
                            if (typeAnswer == "Y")
                            {
                                cleaningActivity = (CleaningActivities)Enum.Parse(typeof(CleaningActivities), item);
                                break;
                            }
                            else if (typeAnswer=="N")
                            {
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Podano błędne dane");
                                return;
                            }
                        }
                        //if (!int.TryParse(Console.ReadLine().ToString(), out counter))
                        //{
                        //    Console.WriteLine("Podano błędny typ.");
                        //    return;
                        //}
                        //else
                        //{
                        //    if (counter > Enum.GetValues(typeof(CleaningActivities)).Length)
                        //    {
                        //        Console.WriteLine("Podano liczbę spoza zakresu");
                        //        return;
                        //    }

                            //CleaningActivities cleaningActivities = new CleaningActivities();
                            //dlaczego tutaj daliśmy ten assign a nie poniżej?
                            //dlaczego nie podajemy tu nru counteru wziętego z inputu od użytkownika
                            //dlaczego w wysokopoziomowym task managerze robimy szczegolowe Add Task dla klasy CleaningTasks a nie generyczne Add Task?
                            //
                        CleaningTasks cleaningTask = new CleaningTasks() { Title = title, Description = description, CleaningActivity = cleaningActivity, IsCompleted = false, Area = area, TaskType = "Cleaning" };


                        //taskType = Console.ReadLine().ToString();
                        taskService.AddNewItem(cleaningTask);
                        break;
                    }
                case "zakupy":
                    {
                        double price =0;
                        int amount =0;
                        //groceries code here
                        GroceriesTasks groceriesTask = new GroceriesTasks() { Title = title, Description = description, IsCompleted = false, Price = price, Amount = amount, TaskType = "Groceries" };
                        taskService.AddNewItem(groceriesTask);
                        break;
                    }
                case "gotowanie":
                    {
                        string recipe = "";
                        //cooking code here
                        CookingTasks cookingTask = new CookingTasks() { Title = title, Description = description, IsCompleted = false, Recipe = recipe, TaskType = "Cooking" };
                        //jak tu dodac item Ingredients, ktory jest ustawiany w setterze klasy CookingTasks
                        taskService.AddNewItem(cookingTask);
                        break;
                    }

            }
            
            Console.WriteLine("Dodano taska");
            Console.ReadKey();
        }

        public void ShowAll()
        {
            List<Tasks> taskList = new List<Tasks>();
            //czy to zainicjalizowanie taskList jest konieczne?
            taskList = taskService.GetAllItems();
            System.Type typeOfVar;
            string typeOfVar_tostring;

            foreach (var task in taskList)
            {
                typeOfVar = task.GetType();
                typeOfVar_tostring = typeOfVar.ToString();
                Console.WriteLine(@"ID to {0}, tytuł: {1}, opis: {2}, typ: {3}, czas procesowania: {4}", task.TaskID, task.Title, task.Description, task.TaskPerformanceTime, task.TaskType);
                switch (typeOfVar_tostring)
                {
                    case "CleaningTasks":
                        //jak sprawdzic do ktorej klasy nalezy, jakiego jest typu
                        {
                            Console.WriteLine($"Rodzaj sprzatania to {task.CleaningActivity}");
                            //teraz jak tu ustawić typ taska żeby dziedziczył po ogólnym tasku ale tylko w przypadku jeśli TaskType=Cleaning
                            break;
                        }
                    case "GroceriesTasks":
                        {
                            Console.WriteLine($"Lista zakupów obejmuje {task.Amount} {task.IngredientName} po cenie {task.Price} za sztukę, o łącznym koszcie {task.Price * task.Amount}.");
                            break;
                        }
                    case "CookingTasks":
                        {
                            Console.WriteLine($"Przepis na danie {task.DishName} wygląda następująco: {task.Recipe} - wykonuje się go z następujących składników: {String.Join(, task.Ingredients)}");
                            break;
                        }
                }



                if (task.IsCompleted)
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
            taskService.DeleteItem(id);
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
                        List<Tasks> taskList = new List<Tasks>()
                            //zmieniłem CleaningTasks na Tasks;
                        //czy to zainicjalizowanie taskList jest konieczne?
                        taskList = taskService.GetItem(showVar);
                        foreach (var task in taskList)
                        {
                            System.Type typeOfTask = task.GetType();
                            string typeOfTask_tostring = typeOfTask.ToString();
                            Console.WriteLine(@"ID to {0}, tytuł: {1}, opis: {2}, typ: {3}, czas procesowania: {4}", task.TaskID, task.Title, task.Description, task.TaskPerformanceTime, task.TaskType);
                            switch (typeOfTask_tostring)
                            {
                                case "CleaningTasks":
                                    //jak sprawdzic do ktorej klasy nalezy, jakiego jest typu
                                    {
                                        Console.WriteLine($"Rodzaj sprzatania to {task.CleaningActivity}");
                                        //teraz jak tu ustawić typ taska żeby dziedziczył po ogólnym tasku ale tylko w przypadku jeśli TaskType=Cleaning
                                        break;
                                    }
                                case "GroceriesTasks":
                                    {
                                        Console.WriteLine($"Lista zakupów obejmuje {task.Amount} {task.IngredientName} po cenie {task.Price} za sztukę, o łącznym koszcie {task.Price * task.Amount}.");
                                        break;
                                    }
                                case "CookingTasks":
                                    {
                                        Console.WriteLine($"Przepis na danie {task.DishName} wygląda następująco: {task.Recipe} - wykonuje się go z następujących składników: {String.Join(, task.Ingredients)}");
                                        break;
                                    }
                            }
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
                        task = new Tasks();

                        System.Type typeOfTask = task.GetType();
                        string typeOfTask_tostring = typeOfTask.ToString();
                        Console.WriteLine(@"ID to {0}, tytuł: {1}, opis: {2}, typ: {3}, czas procesowania: {4}", task.TaskID, task.Title, task.Description, task.TaskPerformanceTime, task.TaskType);
                        switch (typeOfTask_tostring)
                        {
                            case "CleaningTasks":
                                //jak sprawdzic do ktorej klasy nalezy, jakiego jest typu
                                {
                                    Console.WriteLine($"Rodzaj sprzatania to {task.CleaningActivity}");
                                    //teraz jak tu ustawić typ taska żeby dziedziczył po ogólnym tasku ale tylko w przypadku jeśli TaskType=Cleaning
                                    break;
                                }
                            case "GroceriesTasks":
                                {
                                    Console.WriteLine($"Lista zakupów obejmuje {task.Amount} {task.IngredientName} po cenie {task.Price} za sztukę, o łącznym koszcie {task.Price * task.Amount}.");
                                    break;
                                }
                            case "CookingTasks":
                                {
                                    Console.WriteLine($"Przepis na danie {task.DishName} wygląda następująco: {task.Recipe} - wykonuje się go z następujących składników: {String.Join(, task.Ingredients)}");
                                    break;
                                }
                        }
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
            //nie wiem jak tu zmienić tak żeby UpdateItem przyjmowal tylko parametr T item, przeciez musi znalezc najpierw id taska a potem przyjac nowe parametry (nowy tytuł, 
            //nowy opis itp do zmiany. wyjasnijmy to na lekcji
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
            string newType = Console.ReadLine().ToString();
            Console.WriteLine("Podaj nowy czas procesowania");
            string newPerformanceTime = Console.ReadLine().ToString();
            taskService.UpdateItem(taskID, newTitle, newDescription, newType, Convert.ToInt32(newPerformanceTime));
            //jak zrobic zeby bylo nieobowiazkowe zmienianie niektorych parametrow, np. performanceTime albo newType?
            Console.WriteLine(@"Udało się zaktualizować dane taska o numerze {0}", updateVar);
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
            CleaningTasks cleaningTask = taskService.GetItem(id);
            Console.WriteLine(taskService.CheckTaskTime(cleaningTask).ToString());
            Console.ReadKey();
        }

        public void ShowPeople()
        {
            List<FamilyMember> peopleList = new List<FamilyMember>();
            //zastanawiam się czy w ogóle abstrakcyjna klasa Family Member jest potzebna, chyba chciałbym ją usunąć i zrobić Person normalną klasą
            peopleList = taskService.GetAllPeople();
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
            if (!int.TryParse(taskRef, out int resultTaskRef))
            {
                Console.WriteLine("Podane błędny wiek, proszę podać liczbę");
                Console.ReadKey();
                return;
            }
            taskService.AssignPersonToTask(person, resultTaskRef);
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
            var person = new FamilyMember { Name = name, Age = resultAge };
            taskService.AddNewPerson(person);
            Console.WriteLine("Dodano osobę");
            Console.ReadKey();
        }

    }
}


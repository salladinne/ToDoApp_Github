// See https://aka.ms/new-console-template for more information
using System;
using ToDoApp.App.Managers;

namespace ToDoApp_Karol
{
    public class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new();

            while (true) 
            {   
                Console.Clear();                    
                Console.WriteLine("Jaką operację mamy wykonać?");
                Console.WriteLine("1. Dodaj taska");
                Console.WriteLine("2. Usuń taska");
                Console.WriteLine("3. Pokaż taska");
                Console.WriteLine("4. Pokaż wszystkie taski");
                Console.WriteLine("5. Zaktualizuj taska");
                Console.WriteLine("6. Sprawdź kiedy skończy się task");
                Console.WriteLine("7. Wyświetl listę osób z przypisanymi zadaniami");
                Console.WriteLine("8. Przypisz zadanie do osoby");
                Console.WriteLine("9. Dodaj nową osobę");
                Console.WriteLine("9. Zamknij program");

                int.TryParse(Console.ReadLine().ToString(), out int question);
                switch (question)
                {
                    case 1:
                    {
                        taskManager.AddTask();
                        break;
                    }
                    case 2:
                    {
                        taskManager.DeleteTask();
                        break;
                    }
                    case 3:
                    {
                        taskManager.ShowTask();
                        break;
                    }
                    case 4:
                    {
                        taskManager.ShowAll();
                        break;
                    }
                    case 5:
                    {
                        taskManager.UpdateTask();
                        break;
                    }
                    case 6:
                    {
                        taskManager.CheckEstimatedCompletionTime();
                        break;
                    }
                    case 7:
                    {
                        taskManager.ShowPeople();
                        break;
                    }
                    case 8:
                    {
                        taskManager.AssignTask();
                        break;
                    }
                    case 9:
                    {
                        taskManager.AddPerson();
                        break;
                    }
                    case 10:
                    {
                        Environment.Exit(0);
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Błędne dane, spróbuj jeszcze raz");
                        Console.ReadKey();
                        break;
                    }
                }
            }
        }
    }
}

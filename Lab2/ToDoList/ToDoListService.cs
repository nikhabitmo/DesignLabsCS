using Lab2.Console;
using Lab2.Task;

namespace Lab2.ToDoList;

public class ToDoListService
{
    private ToDoList _toDoList;
    private readonly IConsoleInterface _consoleInterface;

    public ToDoListService(ToDoList? toDoList, IConsoleInterface? consoleInterface)
    {
        _toDoList = toDoList ?? new ToDoList(new List<TaskItem>());
        _consoleInterface = consoleInterface ?? new ConsoleInterface();
    }

    public void Start()
    {
        while (true)
        {
            _consoleInterface.ShowMenu();
            var choice = System.Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    SearchTasks();
                    break;
                case "3":
                    DisplayLastTasks();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    System.Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public void AddTask()
    {
        var newTask = _consoleInterface.AddTask();
        _toDoList.Tasks.Add(newTask);
        System.Console.WriteLine("Task added successfully.\n");
    }
    
    public void AddTasks(List<TaskItem> tasks)
    {
        foreach (var task in tasks)
        {
            _toDoList.Tasks.Add(task);
        }
    }

    public void SearchTasks()
    {
        _consoleInterface.SearchTasks(_toDoList.Tasks);
    }

    public void DisplayLastTasks()
    {
        _consoleInterface.DisplayLastTasks(_toDoList.Tasks);
    }
}
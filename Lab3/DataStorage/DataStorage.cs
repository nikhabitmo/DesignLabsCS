using System.Data.SQLite;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization.Json;
using System.Xml;
using Lab3.Task;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;
using Dapper;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace Lab3.DataStorage;

public class DataStorage
{
    private readonly string _projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
    private readonly string _jsonFilePath;
    private readonly string _xmlFilePath;
    private readonly string _sqliteDbPath;

    public DataStorage()
    {
        _jsonFilePath = Path.Combine(_projectDirectory, "tasks.json");
        _xmlFilePath = Path.Combine(_projectDirectory, "tasks.xml");
        _sqliteDbPath = Path.Combine(_projectDirectory, "tasks.db");
        EnsureFilesAndDatabaseExist();
    }


    public void EnsureFilesAndDatabaseExist()
    {
        if (!File.Exists(_jsonFilePath))
        {
            File.Create(_jsonFilePath).Close();
        }

        System.Console.WriteLine(_jsonFilePath);
        System.Console.WriteLine(_xmlFilePath);

        if (!File.Exists(_xmlFilePath))
        {
            File.Create(_xmlFilePath).Close();
        }

        if (!File.Exists(_sqliteDbPath))
        {
            Batteries.Init();
            using (var connection = new SQLiteConnection($"Data Source={_sqliteDbPath};Version=3;"))
            {
                connection.Open();
                connection.Execute(
                    "CREATE TABLE IF NOT EXISTS Tasks (Title TEXT, Description TEXT, Deadline TEXT, Tags TEXT)");
            }
        }
    }

    public void SaveToJSON(List<TaskItem> tasks)
    {
        string json = JsonConvert.SerializeObject(tasks, (Newtonsoft.Json.Formatting)Formatting.Indented);
        File.WriteAllText(_jsonFilePath, json);
    }

    public List<TaskItem> LoadFromJSON()
    {
        if (File.Exists(_jsonFilePath))
        {
            string json = File.ReadAllText(_jsonFilePath);
            return JsonConvert.DeserializeObject<List<TaskItem>>(json);
        }

        return new List<TaskItem>();
    }

    public void SaveToXML(List<TaskItem> tasks)
    {
        using (var writer = XmlWriter.Create(_xmlFilePath))
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<TaskItem>));
            ser.WriteObject(writer, tasks);
        }
    }

    public List<TaskItem> LoadFromXML()
    {
        if (File.Exists(_xmlFilePath))
        {
            using (var reader = XmlReader.Create(_xmlFilePath))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<TaskItem>));
                return (List<TaskItem>)ser.ReadObject(reader);
            }
        }

        return new List<TaskItem>();
    }
    
    public void SaveToSQLite(List<TaskItem> tasks)
    {
        using (var connection = new SQLiteConnection($"Data Source={_sqliteDbPath};Version=3;"))
        {
            connection.Open();
            connection.Execute("DELETE FROM Tasks"); // Очищаем таблицу перед сохранением новых данных

            foreach (var task in tasks)
            {
                connection.Execute(
                    "INSERT INTO Tasks (Title, Description, Deadline, Tags) VALUES (@Title, @Description, @Deadline, @Tags)",
                    new
                    {
                        Title = task.Title,
                        Description = task.Description,
                        Deadline = task.Deadline.ToString("yyyy-MM-dd HH:mm:ss"),
                        Tags = string.Join(",", task.Tags)
                    });
            }
        }
    }
    
    public List<TaskItem> LoadFromSQLite()
    {
        using (var connection = new SQLiteConnection($"Data Source={_sqliteDbPath};Version=3;"))
        {
            connection.Open();
            var tasks = connection.Query("SELECT * FROM Tasks").AsList();

            return tasks.Select(task =>
            {
                var tags = ((string)task.Tags).Split(',').ToList();
                return new TaskItem
                {
                    Title = task.Title,
                    Description = task.Description,
                    Deadline = DateTime.ParseExact((string)task.Deadline, "yyyy-MM-dd HH:mm:ss", null),
                    Tags = tags
                };
            }).ToList();
        }
    }
}
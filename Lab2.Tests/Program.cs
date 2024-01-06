using Lab2.Console;
using Lab2.ToDoList;
using NUnit.Framework;

namespace Lab2.Tests;

[TestFixture]
public class ToDoListTests
{
    private StringWriter _stringWriter;
    private StringReader _stringReader;

    [SetUp]
    public void Setup()
    {
        _stringWriter = new StringWriter();
        _stringReader = new StringReader("1\nsome task\nsome text\n10.11.2012\ntagA\ntagB\n\n2\ntagC\n3\n4\n");
        System.Console.SetOut(_stringWriter);
        System.Console.SetIn(_stringReader);
    }

    [TearDown]
    public void TearDown()
    {
        _stringWriter.Dispose();
        _stringReader.Dispose();
    }

    [Test]
    public void TestToDoListFlow()
    {
        // Arrange
        var consoleInterface = new ConsoleInterface();
        var toDoListService = new ToDoListService(null, consoleInterface);

        // Act
        toDoListService.Start();

        // Assert
        var expectedOutput = "Task added successfully.\nNo such tasks\nActual tasks:\n1. Title: some task\nDescription: some text\nDeadline: 10.11.2012\nTags: tagA, tagB\n";
        Assert.Equals(expectedOutput, _stringWriter.ToString());
    }
}

public static class Program
{
    public static void Main()
    { 
        var toDoListTests = new ToDoListTests();
        toDoListTests.Setup();
        toDoListTests.TestToDoListFlow();
        toDoListTests.TearDown();
    }
}
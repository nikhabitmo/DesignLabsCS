using System;
using System.Collections.Generic;
using System.IO;
using Lab2.Console;
using Lab2.Task;
using Xunit;

namespace Lab2.Tests
{
    public class ConsoleInterfaceTests
    {
        // [Fact]
        // public void ShowMenu_ShouldPrintMenu()
        // {
        //     // Arrange
        //     var output = new StringWriter();
        //     var consoleInterface = new ConsoleInterface();
        //     System.Console.SetOut(output);
        //
        //     // Act
        //     consoleInterface.ShowMenu();
        //
        //     // Assert
        //     var expectedOutput = "Menu:\n1. Add task\n2. Search task\n3. Last tasks\n4. Exit\n> ";
        //     Assert.Equal(expectedOutput, output.ToString());
        // }

        [Fact]
        public void AddTask_ShouldReturnTaskItem()
        {
            // Arrange
            var consoleInterface = new ConsoleInterface();
            // Act
            var task = consoleInterface.AddTask();
            var input = new StringReader("Test Title\nTest Description\n01.01.2023\nTag1\nTag2\n");
            System.Console.SetIn(input);

            // Assert
            Assert.Equal("Test Title", task.Title);
            Assert.Equal("Test Description", task.Description);
            Assert.Equal("01.01.2023", task.Deadline);
            Assert.Equal(new List<string> { "Tag1", "Tag2" }, task.Tags);
        }

        // [Fact]
        // public void SearchTasks_ShouldPrintMatchingTasks()
        // {
        //     // Arrange
        //     var output = new StringWriter();
        //     var consoleInterface = new ConsoleInterface();
        //     System.Console.SetOut(output);
        //
        //     var tasks = new List<TaskItem>
        //     {
        //         new TaskItem("Title1", "Description1", "01.01.2023", new List<string> { "Tag1", "Tag2" }),
        //         new TaskItem("Title2", "Description2", "02.02.2023", new List<string> { "Tag2", "Tag3" }),
        //     };
        //
        //     // Act
        //     consoleInterface.SearchTasks(tasks);
        //
        //     // Assert
        //     var expectedOutput = "Search tasks by tag: Matching tasks:\n" +
        //                          "1. Title: Title1\nDescription: Description1\nDeadline: 01.01.2023\nTags: Tag1, Tag2\n" +
        //                          "2. Title: Title2\nDescription: Description2\nDeadline: 02.02.2023\nTags: Tag2, Tag3\n";
        //     Assert.Equal(expectedOutput, output.ToString());
        // }

    }
}

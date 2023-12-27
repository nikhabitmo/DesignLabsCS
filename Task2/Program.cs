﻿namespace Task2;

static class Program
{
    static void Main()
    {
        Console.WriteLine("Введите количество строк матрицы (M):");
        var m = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество столбцов матрицы (N):");
        var n = int.Parse(Console.ReadLine());

        var k = 1;

        var matrix = new int[m, n];

        var random = new Random();
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                matrix[i, j] = random.Next(1, 101);
            }
        }

        Console.WriteLine("Исходная матрица:");
        
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
        
        RotateMatrix(matrix, k);
        Console.WriteLine("_______________________________________");
        Console.WriteLine("Матрица после преобразования:");
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

    static void RotateMatrix(int[,] matrix, int k)
    {
        var M = matrix.GetLength(0);
        var N = matrix.GetLength(1);

        for (var layer = 0; layer < M / 2; layer++)
        {
            var first = layer;
            var last = M - 1 - layer;

            for (var i = 0; i < k; i++)
            {
                var temp = matrix[first, first + i];
                for (var j = first; j < last; j++)
                {
                    matrix[j, first + i] = matrix[j + 1, first + i];
                }
                for (var j = first; j < last; j++)
                {
                    matrix[last, j] = matrix[last, j + 1];
                }
                for (var j = last; j > first; j--)
                {
                    matrix[j, last] = matrix[j - 1, last];
                }
                for (var j = last; j > first; j--)
                {
                    matrix[first, j] = matrix[first, j - 1];
                }
                matrix[first, first + i + 1] = temp;
            }
        }
    }
}
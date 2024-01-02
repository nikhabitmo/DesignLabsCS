using System;

class Program
{
    static void Main()
    {
        Console.Write("Количество строк матрицы: ");
        var rows = int.Parse(Console.ReadLine());

        Console.Write("Количество столбцов матрицы: ");
        var cols = int.Parse(Console.ReadLine());

        var matrix = GenerateRandomMatrix(rows, cols);

        Console.WriteLine("Сгенерированная матрица:");
        PrintMatrix(matrix);
        
        var product = CalculateProductOfNonNegativeRows(matrix);
        Console.WriteLine($"Произведение элементов в строках без отрицательных элементов: {product}");
        
        var maxSum = CalculateMaxSumOfDiagonals(matrix);
        Console.WriteLine($"Максимум среди сумм элементов диагоналей: {maxSum}");

        Console.ReadLine();
    }

    static int[,] GenerateRandomMatrix(int rows, int cols)
    {
        // Создаем генератор случайных чисел
        var random = new Random();

        var matrix = new int[rows, cols];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(-10, 11);
            }
        }

        return matrix;
    }

    static void PrintMatrix(int[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i, j],3} ");
            }
            Console.WriteLine();
        }
    }

    static int CalculateProductOfNonNegativeRows(int[,] matrix)
    {
        var product = 1;
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            var hasNegative = false;
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] < 0)
                {
                    hasNegative = true;
                    break;
                }
            }
            if (!hasNegative)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    product *= matrix[i, j];
                }
            }
        }
        return product;
    }

    static int CalculateMaxSumOfDiagonals(int[,] matrix)
    {
        var maxSum = int.MinValue;
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            var sum = 0;
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                if (i + j < matrix.GetLength(0))
                {
                    sum += matrix[i + j, j];
                }
            }
            if (sum > maxSum)
            {
                maxSum = sum;
            }
        }
        return maxSum;
    }
}

using MathNet.Numerics;

namespace Lab_2_Calculations;

public static class MatrixGenerator
{
    static Random rnd = new Random();

    public static double[,] Generate(int rows, int cols)
    {
        double[,] matrix = new double[rows, cols];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                matrix[i, j] = GenerateNaturalNum();

        return matrix;
    }

    public static double[] Generate(int rows)
    {
        double[] vector = new double[rows];

        for (int i = 0; i < rows; i++)
        {
            vector[i] = GenerateNaturalNum();
        }

        return vector;
    }

    public static double[] GenerateB(int n)
    {
        var b = new double[n];

        for (int i = 1; i < n + 1; i++)
        {
            if (n.IsEven())
                b[i - 1] = 14 / (i * i * i);
            else
                b[i - 1] = 1 / (i + 14);
        }

        return b;
    }

    public static double[,] GenerateC2(int n)
    {
        var C = new double[n, n];

        for (int i = 1; i < n + 1; i++)
        {
            for (int j = 1; j < n + 1; j++)
            {
                C[i - 1, j - 1] = 14.0 / (i * (j * j * j * j));
            }
        }

        return C;
    }

    private static int GenerateNaturalNum()
    {
        return rnd.Next(0, 1000);
    }
}

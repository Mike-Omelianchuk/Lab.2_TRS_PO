using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2_Calculations;

public static class MatrixOperations
{
    public static int[,] ConvertVector(int[] vector)
    {
        int n = vector.GetLength(0);
        int[,] matrix = new int[1, n];

        for (int i = 0; i < n; i++)
            matrix[0, i] = vector[i];
        
        return matrix;
    }

    public static void M()
    {
        Matrix<int> matrix = Matrix<int>.Build.Random(3, 4);
    }

    public static int[,] Multiply(int[,] m1, int[,] m2)
    {
        int rA = m1.GetLength(0);
        int cA = m1.GetLength(1);
        int rB = m2.GetLength(0);
        int cB = m2.GetLength(1);

        int temp = 0;
        int[,] c = new int[rA, cB];
        for (int i = 0; i < rA; i++)
        {
            for (int j = 0; j < cB; j++)
            {
                temp = 0;
                for (int k = 0; k < cA; k++)
                {
                    temp += m1[i, k] * m2[k, j];
                }
                c[i, j] = temp;
            }
        }

        return c;
    }
}

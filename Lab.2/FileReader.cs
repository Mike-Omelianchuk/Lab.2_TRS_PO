using MathNet.Numerics.LinearAlgebra.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab._2;

public static class FileReader
{
    public static double[,] ReadMatrix(string filePath)
    {
        double[,]? matrix = null;
        
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string input = sr.ReadToEnd();
                string[] lines = input.Split('\n');

                int n = lines.Length;
                matrix = new double[n, n];
                for (int col = 0; col < n; col++)
                {
                    string[] strNumbers = lines[col].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < n; i++)
                    {
                        if (!double.TryParse(strNumbers[i], out double num))
                        {
                            throw new OperationCanceledException(strNumbers[i] + " was not a number.");
                        }
                        matrix[col, i] = num;
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file \"{filePath}\": {ex.Message}");
        }

        if (matrix == null)
        {
            matrix = new double[,] { };
        }

        return matrix;
    }

    public static double[] ReadVector(int n, string filePath)
    {
        string[] line = File.ReadAllLines(filePath);

        double[] vector = new double[n];

        string[] strNumbers = line[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < n; i++)
        {
            if (!double.TryParse(strNumbers[i], out double num))
            {
                throw new OperationCanceledException(strNumbers[i] + " was not a number.");
            }
            vector[i] = num;
        }

        return vector;
    }
}

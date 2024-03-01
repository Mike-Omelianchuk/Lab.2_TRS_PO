using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Lab_2_UI;

internal class UI
{
    static StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 160000);

    ~UI() { sw.Close(); }

    static private void RenderFrame()
    {
        Console.Clear();
        sw.Flush();
    }

    static public int GetMatrixDimensions()
    {
        sw.WriteLine("Enter the Dimensions of squere matrix A, A1, A2, B2:");
        RenderFrame();

        string input = Console.ReadLine();

        if (int.TryParse(input, out int n) && n > 0)
        {
            return n;
        }
        sw.WriteLine("the input is not suitable, try enter a natural number\n(Press enter to continue)");
        sw.Flush();

        while (Console.ReadKey().Key != ConsoleKey.Enter)
        {
            sw.WriteLine("the input is not suitable, try enter a natural number\n(Press enter to continue)");
            RenderFrame();
        }

        return GetMatrixDimensions();
    }

    static public bool Menu1(bool option)
    {
        RenderMenu1(option);


        while (true)
        {
            ConsoleKeyInfo input = Console.ReadKey();


            if (input.Key == ConsoleKey.DownArrow)
            {
                option = false;
                RenderMenu1(option);
            }
            else if (input.Key == ConsoleKey.UpArrow)
            {
                option = true;
                RenderMenu1(option);
            }
            else if (input.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                return option;
            }
        }
    }

    static private void RenderMenu1(bool option)
    {
        if (option)
        {
            sw.Write("Choose task:\n"
                + "> Generate\n"
                + "  Enter manually\n");
            RenderFrame();
        }
        else
        {
            sw.Write("Choose task:\n"
                + "  Generate\n"
                + "> Enter manually\n");
            RenderFrame();
        }
    }

    static public void AddMatrixToScreen(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(0); j++)
                sw.Write(matrix[i, j] + "\t");

            sw.Write('\n');
        }
        sw.Write('\n');
        sw.Flush();
    }

    static public void AddMatrixToScreen(Matrix<double> matrix)
    {
        foreach (Vector<double> v in matrix.EnumerateColumns())
        {
            foreach (double el in v)
            {
                sw.Write(string.Format("{0:0.000}", el) + "\t");
            }
            sw.Write('\n');
        }

        sw.Write('\n');
        sw.Flush();
    }

    static public void AddMatrixToScreen(Vector<double> vector)
    {
        foreach (var el in vector)
            sw.Write(string.Format("{0:0.000}", el) + "\t");

        sw.Write("\n");
        sw.Flush();
    }
}
using MathNet.Numerics.LinearAlgebra;
using Lab_2_Calculations;
using Lab_2_UI;
using Lab._2;
using System.Diagnostics;


int n = UI.GetMatrixDimensions();

bool menuOption1 = UI.Menu1(true);

double[,] Aa = new double[n, n];
double[,] A1a = new double[n, n];
double[,] A2a = new double[n, n];
double[,] B2a = new double[n, n];

double[,] C2a = new double[n, n];

double[] ba = new double[n];
double[] c1a = new double[n];

Thread t1;
Thread t2;
Thread t3;
Thread t4;

var watch = Stopwatch.StartNew();
if (menuOption1)
{
    t1 = new Thread(() => { Aa = MatrixGenerator.Generate(n, n); });
    t1.Start();
    t2 = new Thread(() => { A1a = MatrixGenerator.Generate(n, n); });
    t2.Start();
    t3 = new Thread(() => { A2a = MatrixGenerator.Generate(n, n); });
    t3.Start();
    t4 = new Thread(() => { B2a = MatrixGenerator.Generate(n, n); });
    t4.Start();

    new Thread(() => { c1a = MatrixGenerator.Generate(n); });
}
else
{
    t1 = new Thread(() => { Aa = FileReader.ReadMatrix("C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\A.txt"); });
    t1.Start();
    t2 = new Thread(() => { A1a = FileReader.ReadMatrix("C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\A1.txt"); });
    t2.Start();
    t3 = new Thread(() => { A2a = FileReader.ReadMatrix("C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\A2.txt"); });
    t3.Start();
    t4 = new Thread(() => { B2a = FileReader.ReadMatrix("C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\B2.txt"); });
    t4.Start();

    c1a = FileReader.ReadVector(n, "C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\c1.txt");
}

#region FIRST OUTPUT
C2a = MatrixGenerator.GenerateC2(n);
ba = MatrixGenerator.GenerateB(n);

Console.WriteLine("A");
t1.Join();
Matrix<double> A = Matrix<double>.Build.DenseOfArray(Aa);
UI.AddMatrixToScreen(A);
Console.WriteLine("A1");
t2.Join();
Matrix<double> A1 = Matrix<double>.Build.DenseOfArray(A1a);
UI.AddMatrixToScreen(A1);
Console.WriteLine("A2");
t3.Join();
Matrix<double> A2 = Matrix<double>.Build.DenseOfArray(A2a);
UI.AddMatrixToScreen(A2);
Console.WriteLine("B2");
t4.Join();
Matrix<double> B2 = Matrix<double>.Build.DenseOfArray(B2a);
UI.AddMatrixToScreen(B2);
Console.WriteLine("C2");
Matrix<double> C2 = Matrix<double>.Build.DenseOfArray(C2a);
UI.AddMatrixToScreen(C2);

Console.WriteLine("b1");
Vector<double> b = Vector<double>.Build.DenseOfArray(ba);
UI.AddMatrixToScreen(b);
Console.WriteLine("c1");
Vector<double> c1 = Vector<double>.Build.DenseOfArray(c1a);
UI.AddMatrixToScreen(c1);
#endregion
Console.WriteLine("Optimized");
Console.WriteLine("=================== Calculations ===================");

#region THREAD LAYER 1
// !! 14 * b1
Vector<double> b1_14 = Vector<double>.Build.Dense(n);
Thread b14_t = new Thread(() =>
{
    b1_14 = 14 * b;
    Console.WriteLine("b1 * 14");
}); b14_t.Start();

// !! y1 = A * b1
Vector<double> y1 = Vector<double>.Build.Dense(n);
Thread y1_t = new Thread(() =>
{
    y1 = 14 * b;
    Console.WriteLine("y1 = A * b1");
}); y1_t.Start();

// !! A2 * C2 !!
Matrix<double> A2C2 = Matrix<double>.Build.Dense(n, n);
Thread A2C2_t = new Thread(() =>
{
    A2C2 = A2 * C2;
    Console.WriteLine("A2 * C2");
}); A2C2_t.Start();

// !! 14 * c1
Vector<double> c1_14 = Vector<double>.Build.Dense(n);
Thread c1_14_t = new Thread(() =>
{
    c1_14 = 14 * c1;
    Console.WriteLine("c1 * 14");
}); c1_14_t.Start();
#endregion

#region THREAD LAYER 2
// !! 14b1 + 14c1
b14_t.Join();
c1_14_t.Join();
Vector<double> b1_14__c1_14 = Vector<double>.Build.Dense(n);
Thread b1_14__c1_14_t = new Thread(() =>
{
    b1_14__c1_14 = b1_14 + c1_14;
    Console.WriteLine("14*b1 + 14*c1");
}); b1_14__c1_14_t.Start();

// !! Y3 = A2*C2 - B2
A2C2_t.Join();
Matrix<double> Y3 = Matrix<double>.Build.Dense(n, n);
Thread Y3_t = new Thread(() =>
{
    Y3 = A2C2 - B2;
    Console.WriteLine("Y3 = A2*C2 - B2");
}); Y3_t.Start();

// !! y2 = A1(14b1 + 14c1)
b1_14__c1_14_t.Join();
Matrix<double> y2 = Matrix<double>.Build.Dense(n, 1);
Thread y2_t = new Thread(() =>
{
    y2 = A1 * b1_14__c1_14.ToColumnMatrix();
    Console.WriteLine($"y2");
}); y2_t.Start();

// !! y1'
y1_t.Join();
Matrix<double> y1_dash = Matrix<double>.Build.Dense(y1.Count, 1);
Thread y1_dash_t = new Thread(() =>
{
    y1_dash = y1.ToRowMatrix();
    Console.WriteLine($"y1' ({y1_dash.RowCount} {y1_dash.ColumnCount})");
}); y1_dash_t.Start();
#endregion

#region THREAD LAYER 3
// !! y2y1'
y2_t.Join(1);
y1_dash_t.Join();
Matrix<double> y2_y1_dash = Matrix<double>.Build.Dense(n, n);
Thread y2_y1_dash_t = new Thread(() =>
{
    y2_y1_dash = y2 * y1_dash;
    Console.WriteLine($"y2y1' ({y2_y1_dash.RowCount} {y2_y1_dash.ColumnCount})");
}); y2_y1_dash_t.Start();

// !! y2'
Matrix<double> y2_dash = Matrix<double>.Build.Dense(1, n);
Thread y2_dash_t = new Thread(() =>
{
    y2_dash = y2.Transpose();
    Console.WriteLine("y2'");
}); y2_dash_t.Start();

// !! Y3^3
Y3_t.Join();
Matrix<double> Y3_3 = Matrix<double>.Build.Dense(n, n);
Thread Y3_3_t = new Thread(() =>
{
    Y3_3 = Y3 * Y3 * Y3;
    Console.WriteLine("Y3^3");
}); Y3_3_t.Start();

// !! Y3^3_y1!!
Matrix<double> Y3_3_y1 = Matrix<double>.Build.Dense(n, 1);
Thread Y3_3_y1_t = new Thread(() =>
{
    Y3_3_y1 = Y3_3 * y1.ToColumnMatrix();
    Console.WriteLine($"Y3^3*y1 ({Y3_3_y1.RowCount} {Y3_3_y1.ColumnCount})");
}); Y3_3_y1_t.Start();

// !! y2'*Y3
y2_dash_t.Join();
Matrix<double> y2_dash_Y3 = Matrix<double>.Build.Dense(1, n);
Thread y2_dash_Y3_t = new Thread(() =>
{
    y2_dash_Y3 = y2_dash * Y3;
    Console.WriteLine($"y2'*Y3 ({y2_dash_Y3.RowCount} {y2_dash_Y3.ColumnCount})");
}); y2_dash_Y3_t.Start();
#endregion

#region THREAD LAYER 4
// !! y2'*Y3*y1
y2_dash_Y3_t.Join();
Matrix<double> y2_dash_Y3_y1 = Matrix<double>.Build.Dense(1, 1);
Thread y2_dash_Y3_y1_t = new Thread(() =>
{
    y2_dash_Y3_y1 = y2_dash_Y3 * y1.ToColumnMatrix();
    Console.WriteLine($"y2'*Y3*y1 ({y2_dash_Y3_y1.RowCount} {y2_dash_Y3_y1.ColumnCount})");
}); y2_dash_Y3_y1_t.Start();

// !! Y3^3_y1_y1'
Y3_3_y1_t.Join();
Matrix<double> Y3_3_y1_y1_dash = Matrix<double>.Build.Dense(n, n);
Thread Y3_3_y1_y1_dash_t = new Thread(() =>
{
    Y3_3_y1_y1_dash = Y3_3_y1 * y1_dash;
    Console.WriteLine($"Y3^3*y1*y1' ({Y3_3_y1_y1_dash.RowCount} {Y3_3_y1_y1_dash.ColumnCount})");
}); Y3_3_y1_y1_dash_t.Start();

// !! y2'*Y3*y1*Y3
y2_dash_Y3_y1_t.Join();
Matrix<double> y2_dash_Y3_y1_Y3 = Matrix<double>.Build.Dense(n, n);
Thread y2_dash_Y3_y1_Y3_t = new Thread(() =>
{
    y2_dash_Y3_y1_Y3 = y2_dash_Y3_y1[0,0] * Y3;
    Console.WriteLine($"y2'*Y3*y1*Y3 ({y2_dash_Y3_y1_Y3.RowCount} {y2_dash_Y3_y1_Y3.ColumnCount})");
}); y2_dash_Y3_y1_Y3_t.Start();
#endregion

#region FIND SOLUTION
y2_y1_dash_t.Join();
Y3_3_y1_y1_dash_t.Join();
y2_dash_Y3_y1_Y3_t.Join();
// !! x
Matrix<double> x = Matrix<double>.Build.Dense(n, n);
Thread x_t = new Thread(() =>
{
    x = Y3_3_y1_y1_dash * y2_y1_dash * y2_dash_Y3_y1_Y3;
    Console.WriteLine("\nx:");
    UI.AddMatrixToScreen(x);
    watch.Stop();
    Console.WriteLine("\n" + watch.ElapsedMilliseconds + "ms");
});
x_t.Start();
#endregion

Console.WriteLine("====================== Output ======================");

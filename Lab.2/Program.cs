using MathNet.Numerics.LinearAlgebra;
using Lab_2_Calculations;
using Lab_2_UI;
using Lab._2;

int n = UI.GetMatrixDimensions();

bool menuOption1 = UI.Menu1(true);

double[,] Aa = new double[n, n];
double[,] A1a = new double[n, n];
double[,] A2a = new double[n, n];
double[,] B2a = new double[n, n];

double[,] Ca = new double[n, n];

double[] ba = new double[n];
double[] c1a = new double[n];

Thread t1;
Thread t2;
Thread t3;
Thread t4;

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
    
    //Aa = MatrixGenerator.Generate(n, n);
    //A1a = MatrixGenerator.Generate(n, n);
    //A2a = MatrixGenerator.Generate(n, n);
    //B2a = MatrixGenerator.Generate(n, n);


    new Thread(() => { c1a = MatrixGenerator.Generate(n); });
    //c1a = MatrixGenerator.Generate(n);
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

    //Aa = FileReader.ReadMatrix("C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\A.txt");
    //A1a = FileReader.ReadMatrix("C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\A1.txt");
    //A2a = FileReader.ReadMatrix("C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\A2.txt");
    //B2a = FileReader.ReadMatrix("C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\B2.txt");

    c1a = FileReader.ReadVector(n, "C:\\Users\\Mike\\Desktop\\workshop\\CS\\TRS_PO\\Lab.2\\Lab.2\\c1.txt");
}

Ca = MatrixGenerator.GenerateC(n);
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

Console.WriteLine("C");
Matrix<double> C = Matrix<double>.Build.DenseOfArray(Ca);
UI.AddMatrixToScreen(C);

Console.WriteLine("b");
Vector<double> b = Vector<double>.Build.DenseOfArray(ba);
UI.AddMatrixToScreen(b);
Console.WriteLine("c1");
Vector<double> c1 = Vector<double>.Build.DenseOfArray(c1a);
UI.AddMatrixToScreen(c1);

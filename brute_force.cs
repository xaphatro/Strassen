using System;
using System.Diagnostics;

public class Matrix
{
    public static void Main()
    {
        int n;
        float t;
        Stopwatch watch = Stopwatch.StartNew();

        n = 16;
        calculate(n);
        t = (float)watch.ElapsedMilliseconds/1000;
        Console.WriteLine(t);
        watch.Restart();
        
        n = 32;
        calculate(n);
        t = (float)watch.ElapsedMilliseconds/1000;
        Console.WriteLine(t);
        watch.Restart();

        n = 64;
        calculate(n);
        t = (float)watch.ElapsedMilliseconds/1000;
        Console.WriteLine(t);
        watch.Restart();

        n = 128;
        calculate(n);
        t = (float)watch.ElapsedMilliseconds/1000;
        Console.WriteLine(t);
        watch.Restart();

        n = 256;
        calculate(n);
        t = (float)watch.ElapsedMilliseconds/1000;
        Console.WriteLine(t);
        watch.Restart();

        n = 512;
        calculate(n);
        t = (float)watch.ElapsedMilliseconds/1000;
        Console.WriteLine(t);
        watch.Restart();

        n = 1024;
        calculate(n);
        t = (float)watch.ElapsedMilliseconds/1000;
        Console.WriteLine(t);
        watch.Restart();

        n = 2048;
        calculate(n);
        t = (float)watch.ElapsedMilliseconds/1000;
        Console.WriteLine(t);
        watch.Restart();

        n = 4096;
        calculate(n);
        t = (float)watch.ElapsedMilliseconds/1000;
        Console.WriteLine(t);
        watch.Restart();
    }

    public static void calculate(int n)
    {   
        Random rng = new Random();
        float[,] m1 = new float[n,n];
        float[,] m2 = new float[n,n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                m1[i,j] = (float)rng.NextDouble();
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                m2[i,j] = (float)rng.NextDouble();
            }
        }
        
        float[,] m3 = matrixMulNaive(m1, m2, n);
    }
    
    static float[,] matrixMulNaive(float[,] a, float[,] b, int n)
    {
        float[,] c = new float[n,n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                c[i, j] = 0;
                for (int k = 0; k < n; k++)
                {
                    c[i, j] += a[i, k]*b[k, j];
                }
            }
        }
        return c;
    }
}
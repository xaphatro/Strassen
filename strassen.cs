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
        
        float[,] m3 = strassen(m1, m2, n);
    }
    
    static float[,] strassen(float[,] a, float[,] b, int n)
    {
        if (n == 2)
        {
            var m1 = (a[0, 0] + a[1, 1]) * (b[0, 0] + b[1, 1]);
            var m2 = (a[1, 0] + a[1, 1]) * b[0, 0];
            var m3 = a[0, 0] * (b[0, 1] - b[1, 1]);
            var m4 = a[1, 1] * (b[1, 0] - b[0, 0]);
            var m5 = (a[0, 0] + a[0, 1]) * b[1, 1];
            var m6 = (a[1, 0] - a[0, 0]) * (b[0, 0] + b[0, 1]);
            var m7 = (a[0, 1] - a[1, 1]) * (b[1, 0] + b[1, 1]);
            a[0, 0] = m1 + m4 - m5 + m7;
            a[0, 1] = m3 + m5;
            a[1, 0] = m2 + m4;
            a[1, 1] = m1 - m2 + m3 + m6;
            return a;
        }
        else
        {
            float[,] a11 = matrixDivide(a, n, 11);
            float[,] a12 = matrixDivide(a, n, 12);
            float[,] a21 = matrixDivide(a, n, 21);
            float[,] a22 = matrixDivide(a, n, 22);

            float[,] b11 = matrixDivide(b, n, 11);
            float[,] b12 = matrixDivide(b, n, 12);
            float[,] b21 = matrixDivide(b, n, 21);
            float[,] b22 = matrixDivide(b, n, 22);

            float[,] p1 = strassen(a11, matrixDiff(b12, b22, n/2), n/2);
            float[,] p2 = strassen(matrixSum(a11, a12, n/2), b22, n/2);
            float[,] p3 = strassen(matrixSum(a21, a22, n/2), b11, n/2);
            float[,] p4 = strassen(a22, matrixDiff(b21, b11, n/2), n/2);
            float[,] p5 = strassen(matrixSum(a11, a22, n/2), matrixSum(b11, b22, n/2), n/2);
            float[,] p6 = strassen(matrixDiff(a12, a22, n/2), matrixSum(b21, b22, n/2), n/2);
            float[,] p7 = strassen(matrixDiff(a11, a21, n/2), matrixSum(b11, b12, n/2), n/2);

            float[,] c11 = matrixDiff(matrixSum(p5, p4, n/2),matrixDiff(p2, p6, n/2), n/2);
            float[,] c12 = matrixSum(p1, p2, n/2);
            float[,] c21 = matrixSum(p3, p4, n/2);
            float[,] c22 = matrixDiff(matrixSum(p1, p5, n/2), matrixSum(p3, p7, n/2), n/2);

            for (int i = 0; i < n/2; i++)
            {
                for (int j = 0; j < n/2; j++)
                {
                    a[i,j] = c11[i,j];
                    a[i,j+n/2] = c12[i,j];
                    a[i+n/2,j] = c21[i,j];
                    a[i+n/2,j+n/2] = c22[i,j];
                }
            }
            return a;
        }
    }

    static float[,] matrixSum(float[,] a, float[,] b, int n)
    {
        float[,] c = new float[n,n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                c[i,j] = a[i,j] + b[i,j];
        return c;
    }


    static float[,] matrixDiff(float[,] a, float[,] b, int n)
    {
        float[,] c = new float[n,n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                c[i,j] = a[i,j] - b[i,j];
        return c;
    }

    static float[,] matrixCombine(float[,] a11, float[,] a12, float[,] a21, float[,] a22, int n)
    {
        float[,] a = new float[n,n];
        for (int i = 0; i < n/2; i++)
        {
            for (int j = 0; j < n/2; j++)
            {
                a[i,j] = a11[i,j];
                a[i,j+n/2] = a12[i,j];
                a[i+n/2,j] = a21[i,j];
                a[i+n/2,j+n/2] = a22[i,j];
            }
        }
        return a;
    }

    static float[,] matrixDivide(float[,] a, int n, int region)
    {
        float[,] c = new float[n/2,n/2];
        if (region == 11)
        {
            for (int i = 0, x = 0; x < n/2; i++, x++)
            {
                for (int j = 0, y = 0; y < n/2; j++, y++)
                {
                    c[i,j] = a[x,y];
                }
            }
        }
        else if (region == 12)
        {
            for (int i = 0, x = 0; x < n/2; i++, x++)
            {
                for (int j = 0, y = n/2; y < n; j++, y++)
                {
                    c[i,j] = a[x,y];
                }
            }
        }
        else if (region == 21)
        {
            for (int i = 0, x = n/2; x < n; i++, x++)
            {
                for (int j = 0, y = 0; y < n/2; j++, y++)
                {
                    c[i,j] = a[x,y];
                }
            }
        }
        else if (region == 22)
        {
            for (int i = 0, x = n/2; x < n; i++, x++)
            {
                for (int j = 0, y = n/2; y < n; j++, y++)
                {
                    c[i,j] = a[x,y];
                }
            }
        }
        return c;
    }
}
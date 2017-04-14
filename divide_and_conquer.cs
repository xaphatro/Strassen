using System;
using System.Threading.Tasks;
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
        
        float[,] m3 = divideNConquer(m1, m2, n);
    }
    
    static float[,] divideNConquer(float[,] a, float[,] b, int n)
    {
        if (n == 2)
        {
            var a00 = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0];
            var a01 = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1];
            var a10 = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0];
            var a11 = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1];
            a[0, 0] = a00;
            a[0, 1] = a01;
            a[1, 0] = a10;
            a[1, 1] = a11;
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

            float[,] c11 = matrixSum(divideNConquer(a11, b11, n/2), divideNConquer(a12, b21, n/2), n/2);
            float[,] c12 = matrixSum(divideNConquer(a11, b12, n/2), divideNConquer(a12, b22, n/2), n/2);
            float[,] c21 = matrixSum(divideNConquer(a21, b11, n/2), divideNConquer(a22, b21, n/2), n/2);
            float[,] c22 = matrixSum(divideNConquer(a21, b12, n/2), divideNConquer(a22, b22, n/2), n/2);

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
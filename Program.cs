using System;
using System.Threading;

class Program
{
    private static readonly object lockA = new object();
    private static readonly object lockB = new object();

    static void Main()
    {
        Thread thread1 = new Thread(() =>
        {
            lock (lockA)
            {
                Console.WriteLine("Thread 1 locked A");

                Thread.Sleep(100);

                Console.WriteLine("Thread 1 waiting for B");

                lock (lockB)
                {
                    Console.WriteLine("Thread 1 locked B");
                }
            }
        });

        Thread thread2 = new Thread(() =>
        {
            // IMPORTANT:
            // Thread 2 also takes A before B.
            lock (lockA)
            {
                Console.WriteLine("Thread 2 locked A");

                Thread.Sleep(100);

                Console.WriteLine("Thread 2 waiting for B");

                lock (lockB)
                {
                    Console.WriteLine("Thread 2 locked B");
                }
            }
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Program finished.");
    }
}
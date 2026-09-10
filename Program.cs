using System;
using System.Threading;

class Program
{
    // Two shared lock objects.
    private static readonly object lockA = new object();
    private static readonly object lockB = new object();

    static void Main()
    {
        Thread thread1 = new Thread(() =>
        {
            lock (lockA)
            {
                Console.WriteLine("Thread 1 locked A");

                // Give Thread 2 time to lock B.
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
            lock (lockB)
            {
                Console.WriteLine("Thread 2 locked B");

                // Give Thread 1 time to lock A.
                Thread.Sleep(100);

                Console.WriteLine("Thread 2 waiting for A");

                lock (lockA)
                {
                    Console.WriteLine("Thread 2 locked A");
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
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

class Program
{
    private static readonly ConcurrentBag<int> GlobalList = new ConcurrentBag<int>();
    private static readonly int MaxItems = 1000000;

    static void Main(string[] args)
    {
        // Create the threads
        var oddThread = new Thread(GenerateOddNumbers);
        var primeThread = new Thread(GeneratePrimeNumbers);
        var evenThread = new Thread(GenerateEvenNumbers);

        // Start the threads
        oddThread.Start();
        primeThread.Start();

        // Wait for the global list to reach 250,000 items
        while (GlobalList.Count < 250000)
        {
            Thread.Sleep(100);
        }

        // Start the even thread
        evenThread.Start();

        // Wait for all threads to finish
        oddThread.Join();
        primeThread.Join();
        evenThread.Join();

        // Sort the global list
        GlobalList.Sort();

        // Count the odd and even numbers in the global list
        int oddCount = GlobalList.Count(n => n % 2 != 0);
        int evenCount = GlobalList.Count - oddCount;

        // Display the results
        Console.WriteLine($"Total items: {GlobalList.Count}");
        Console.WriteLine($"Odd numbers: {oddCount}");
        Console.WriteLine($"Even numbers: {evenCount}");

        // Serialize the global list to binary and XML files
        using (var binaryStream = new FileStream("list.bin", FileMode.Create))
        using (var xmlStream = new FileStream("list.xml", FileMode.Create))
        {
            new BinaryFormatter().Serialize(binaryStream, GlobalList);
            new DataContractSerializer(typeof(List<int>)).Serialize(xmlStream, GlobalList.ToList());
        }
    }

    private static void GenerateOddNumbers()
    {
        while (GlobalList.Count < MaxItems)
        {
            int randomNumber = new Random().Next();
            if (randomNumber % 2 != 0)
            {
                GlobalList.Add(randomNumber);
            }
        }
    }

    private static void GeneratePrimeNumbers()
    {
        int currentPrime = 2;
        while (GlobalList.Count < MaxItems)
        {
            if (IsPrime(currentPrime))
            {
                GlobalList.Add(-currentPrime);
            }

            currentPrime++;
        }
    }

    private static void GenerateEvenNumbers()
    {
        while (GlobalList.Count < MaxItems)
        {
            int randomNumber = new Random().Next();
            if (randomNumber % 2 == 0)
            {
                GlobalList.Add(randomNumber);
            }
        }
    }

    private static bool IsPrime(int number)
    {
        if (number <= 1)
        {
            return false;
        }

        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }
}

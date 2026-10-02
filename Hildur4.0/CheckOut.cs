using System;

public class CheckOut
{
    public void Start()
    {
        Console.Clear();

        Console.WriteLine("==============================");
        Console.WriteLine("         UTCHECKNING");
        Console.WriteLine("==============================");
        Console.WriteLine();

        Console.Write("Vad heter du? ");
        string name = Console.ReadLine();

        Console.Write("Vilket år dog du? ");
        string deathYear = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine("==============================");
        Console.WriteLine();

        Console.WriteLine($"Tack {name}.");
        Console.WriteLine($"Dödsår: {deathYear}");

        Console.WriteLine();
        Console.WriteLine("Tack för din vistelse på hotell Hjortonet.Din vistelse är nu avslutad och det är dags att checka ut och checka in på ditt nästa liv. Lycka till med din reinkarnation!

        Console.WriteLine();
        Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }
}
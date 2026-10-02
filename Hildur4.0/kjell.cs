using System;
using System.Threading;

public class Kjell
{
    private Random random = new Random();

    public string Name { get; set; }

    // Räknar hur många menyval spelaren har gjort
    private int menuChoices = 0;


    // Konstruktor
    public Kjell()
    {
        Name = "Kjell";
    }


    // Anropas varje gång spelaren gör ett menyval
    public void MenuChoice()
    {
        menuChoices++;

        // Kjell dyker upp efter 3 menyval
        if (menuChoices == 3)
        {
            Appear();
            Sabotage();

            // Börja räkna om efter Kjells sabotage
            menuChoices = 0;
        }
    }


    // Kjell dyker upp
    private void Appear()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

        Console.WriteLine();
        Console.WriteLine("      /\\_/\\\\");
        Console.WriteLine("     ( o.o )");
        Console.WriteLine("      > ^ <");
        Console.WriteLine();
        Console.WriteLine("       KJELL!");
        Console.WriteLine();

        Console.ResetColor();

        Thread.Sleep(1500);
    }


    // Väljer ett slumpmässigt sabotage
    private void Sabotage()
    {
        int sabotage = random.Next(1, 6);

        switch (sabotage)
        {
            case 1:
                CloseMenu();
                break;

           
            case 2:
                StealKey();
                break;


            case 3:
                TurnOffLights();
                break;

            case 4:
                AnnoyPlayer();
                break;

            case 5:
                DoNothing();
                break;
        }
    }


    // Sabotage 1 - Kjell kraschar menyn
    private void CloseMenu()
    {
        Console.WriteLine("Kjell tittar på menyn...");
        Thread.Sleep(800);

        Console.WriteLine("Kjell trycker på en knapp...");
        Thread.Sleep(800);

        Console.WriteLine();
        Console.WriteLine("💥 KJELL HAR KRASCHAT HELA PROGRAMMET! 💥");

        Thread.Sleep(1500);

        Console.Clear();

        Console.WriteLine("================================");
        Console.WriteLine("          KJELL WINS");
        Console.WriteLine("================================");
        Console.WriteLine();

        Console.WriteLine("Kjell har bestämt att du inte är välkommen.");
        Console.WriteLine();
        Console.WriteLine("Starta om programmet för att försöka igen.");

        Console.WriteLine();
        Console.WriteLine("Tryck på valfri tangent för att avsluta.");

        Console.ReadKey();

        Environment.Exit(0);
    }


    // Sabotage 2 - Kjell försöker stjäla nyckeln



    private void StealKey()
    {
        Console.WriteLine("Kjell hittar din nyckel...");
        Thread.Sleep(800);

        Console.WriteLine();
        Console.WriteLine("Kjell snor nyckeln!");

        Thread.Sleep(1000);

        Console.WriteLine();
        Console.WriteLine("Kjell springer iväg med den...");

        Thread.Sleep(1000);

        Console.Clear();

        Console.WriteLine("================================");
        Console.WriteLine("       KJELL HAR DIN NYCKEL!");
        Console.WriteLine("================================");
        Console.WriteLine();

        Console.WriteLine("[1] Försök ta tillbaka nyckeln");
        Console.WriteLine("[2] Låt Kjell behålla den");
        Console.WriteLine();

        Console.Write("Vad gör du? ");

        string choice = Console.ReadLine();

        if (choice == "1")
        {
            Console.WriteLine();
            Console.WriteLine("Du försöker ta tillbaka nyckeln...");

            Thread.Sleep(1000);

            Console.WriteLine("Kjell stirrar på dig.");
            Thread.Sleep(1000);

            Console.WriteLine("Du stirrar tillbaka.");
            Thread.Sleep(1000);

            Console.WriteLine("Kjell blinkar.");
            Thread.Sleep(500);

            Console.WriteLine("Du blinkar.");
            Thread.Sleep(500);

            Console.WriteLine();
            Console.WriteLine("Kjell blir uttråkad och tappar nyckeln.");

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.WriteLine("Du fick tillbaka nyckeln!");

            Thread.Sleep(1500);
        }
        else if (choice == "2")
        {
            Console.WriteLine();
            Console.WriteLine("Kjell springer iväg med nyckeln.");

            Thread.Sleep(1000);

            Console.WriteLine();
            Console.WriteLine("Du kommer inte längre in i ditt rum.");

            Thread.Sleep(1500);

            Console.WriteLine();
            Console.WriteLine("Du måste boka ett nytt rum.");

            Thread.Sleep(2000);

            Console.WriteLine();
            Console.WriteLine("Tryck på valfri tangent för att boka om.");

            Console.ReadKey();

            // Här kan vi senare skicka tillbaka spelaren
            // till bokningsmenyn.
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("Kjell förstår inte vad du menar.");

            Thread.Sleep(1000);

            Console.WriteLine("Kjell behåller nyckeln.");

            Thread.Sleep(1500);
        }
    }


    // Sabotage 3 - Kjell släcker lamporna
    private void TurnOffLights()
    {
        Console.WriteLine("Kjell släcker lamporna...");
        Thread.Sleep(1000);

        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.WriteLine();
        Console.WriteLine("...");
        Thread.Sleep(1000);

        Console.WriteLine();
        Console.WriteLine("Det är kolsvart.");
        Thread.Sleep(1000);

        Console.WriteLine();
        Console.WriteLine("Du hör ett ljud...");
        Thread.Sleep(1000);

        Console.WriteLine();
        Console.WriteLine("Mjau.");

        Console.ResetColor();

        Thread.Sleep(1500);
    }


    // Sabotage 4 - Kjell irriterar spelaren
    private void AnnoyPlayer()
    {
        Console.WriteLine("Kjell tittar på dig.");

        Thread.Sleep(1000);

        Console.WriteLine();
        Console.WriteLine("Kjell: \"Mjau.\"");

        Thread.Sleep(1000);

        Console.WriteLine();
        Console.WriteLine("...");

        Thread.Sleep(1000);

        Console.WriteLine();
        Console.WriteLine("Kjell: \"Mjau.\"");

        Thread.Sleep(1000);

        Console.WriteLine();
        Console.WriteLine("Kjell: \"MJAU!\"");

        Thread.Sleep(1500);
    }


    // Sabotage 5 - Kjell gör absolut ingenting
    private void DoNothing()
    {
        Console.WriteLine("Kjell dyker upp.");

        Thread.Sleep(1000);

        Console.WriteLine("Kjell tittar på dig.");

        Thread.Sleep(1500);

        Console.WriteLine("Kjell gör absolut ingenting.");

        Thread.Sleep(1000);

        Console.WriteLine("Kjell går därifrån.");

        Thread.Sleep(1000);
    }

    
}
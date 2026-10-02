namespace Hildur4._0;

class Program
{
    private static void Main(string[] args)
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"
======================================================
         *** REGISTRERA NYTT SPÖKE ***             
======================================================");
            Console.ResetColor();

            // 1. Skapa ett nytt spökobjekt
            var ghost = new GhostCustom.Ghost();

            // 2. Mata in Namn
            Console.Write("\n[1] Ange spökets namn: ");
            ghost.Name = Console.ReadLine() ?? "Okänt spöke";

            // 3. Mata in Dödsår (med validering så det blir ett nummer)
            int year;
            while (true)
            {
                Console.Write("[2] Ange bortgångsår (t.ex. 1650): ");
                if (int.TryParse(Console.ReadLine(), out year) && year <= DateTime.Now.Year)
                {
                    ghost.YearOfPassing = year;
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("    Ange ett giltigt år!");
                Console.ResetColor();
            }

            // 4. Bär spöket kedjor?
            Console.Write("[3] Bär spöket skrammelkedjor? (j\n): ");
            string chainInput = Console.ReadLine()?.ToLower() ?? "";
            ghost.HasChains = chainInput == "j" || chainInput == "ja";

            // 5. Mata in Ouppklarade ärenden (Unfinished Business)
            Console.WriteLine("\n[4] Ouppklarade ärenden:");
            bool addingBusiness = true;
            while (addingBusiness)
            {
                Console.Write("    -> Beskriv ärendet (lämna tomt och tryck Enter för att avsluta): ");
                string businessDesc = Console.ReadLine() ?? "";

                if (string.IsNullOrWhiteSpace(businessDesc))
                {
                    addingBusiness = false;
                }
                else
                {
                    ghost.BusinessList.Add(new GhostCustom.UnfinishedBusiness
                    {
                        Description = businessDesc,
                        IsResolved = false
                    });
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("       (Ärende tillagt!)");
                    Console.ResetColor();
                }
            }

            // 6. Kör Compliance-kontroll på det inmatade spöket
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine($"      EFTERLEVNADSKONTROLL FÖR: {ghost.Name.ToUpper()}");
            Console.WriteLine("==================================================\n");

            Console.WriteLine($"Namn:          {ghost.Name}");
            Console.WriteLine($"Bortgångsår:   {ghost.YearOfPassing} (Död för {DateTime.Now.Year - ghost.YearOfPassing} år sedan)");
            Console.WriteLine($"Skrammelkedjor: {(ghost.HasChains ? "Ja" : "Nej")}");
            Console.WriteLine($"Antal ärenden: {ghost.BusinessList.Count}");
            Console.WriteLine(new string('-', 50));

            var checker = new GhostCustom.ComplianceChecker();
            var (isCompliant, reason) = checker.ValidateCheckIn(ghost);

            if (isCompliant)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[GODKÄND] {reason}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[NEKAD] {reason}");
            }
            Console.ResetColor();

            // 7. Fråga om användaren vill mata in ett till spöke
            Console.WriteLine("\n==================================================");
            Console.Write("Vill du mata in ett till spöke? (j/n): ");
            string continueInput = Console.ReadLine()?.ToLower() ?? "";
            if (continueInput != "j" && continueInput != "ja")
            {
                keepRunning = false;
            }
        }

        Console.WriteLine("\nTack för besöket på Hotellet Hjortron för Efterlevnad!");
    }
}

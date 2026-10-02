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
         *** REGISTER NEW GHOST ***             
======================================================");
            Console.ResetColor();

            // 1. Create a new ghost object
            var ghost = new GhostCustom.Ghost();

            // 2. Input Name
            Console.Write("\n[1] Enter ghost name: ");
            ghost.Name = Console.ReadLine() ?? "Unknown Ghost";

            // 3. Input Year of Passing (with validation to ensure it's a number)
            int year;
            while (true)
            {
                Console.Write("[2] Enter year of passing (e.g., 1650): ");
                if (int.TryParse(Console.ReadLine(), out year) && year <= DateTime.Now.Year)
                {
                    ghost.YearOfPassing = year;
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("    Please enter a valid year!");
                Console.ResetColor();
            }

            // 4. Does the ghost wear chains?
            Console.Write("[3] Does the ghost wear rattling chains? (y/n): ");
            string chainInput = Console.ReadLine()?.ToLower() ?? "";
            ghost.HasChains = chainInput == "y" || chainInput == "yes";

            // 5. Input Unfinished Business
            Console.WriteLine("\n[4] Unfinished business:");
            bool addingBusiness = true;
            while (addingBusiness)
            {
                Console.Write("    -> Describe business item (leave blank and press Enter to finish): ");
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
                    Console.WriteLine("       (Item added!)");
                    Console.ResetColor();
                }
            }

            // 6. Run Compliance Check on the registered ghost
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine($"      COMPLIANCE CHECK FOR: {ghost.Name.ToUpper()}");
            Console.WriteLine("==================================================\n");

            Console.WriteLine($"Name:            {ghost.Name}");
            Console.WriteLine($"Year of Passing: {ghost.YearOfPassing} ({DateTime.Now.Year - ghost.YearOfPassing} years ago)");
            Console.WriteLine($"Rattling Chains: {(ghost.HasChains ? "Yes" : "No")}");
            Console.WriteLine($"Business Count:  {ghost.BusinessList.Count}");
            Console.WriteLine(new string('-', 50));

            var checker = new GhostCustom.ComplianceChecker();
            var (isCompliant, reason) = checker.ValidateCheckIn(ghost);

            if (isCompliant)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n[APPROVED] {reason}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[DENIED] {reason}");
            }
            Console.ResetColor();

            // 7. Ask if the user wants to register another ghost
            Console.WriteLine("\n==================================================");
            Console.Write("Would you like to register another ghost? (y/n): ");
            string continueInput = Console.ReadLine()?.ToLower() ?? "";
            if (continueInput != "y" && continueInput != "yes")
            {
                keepRunning = false;
            }
        }

        Console.WriteLine("\nThank you for visiting Hotel Cloudberry for Compliance!");
    }
}
using System;
using GTranslate.Translators;
using Hildur4._0.Booking;

namespace Hildur4._0;

class Program
{
    static void Main(string[] args)
    {
        // Initialize booking service/UI so Booking choice can launch it
        var bookingService = new BookingService();
        var bookingConsoleUi = new BookingConsoleUI(bookingService);

        bool keepRunning = true;

        while (keepRunning)
        {
            WelcomeUI welcomeUI = new WelcomeUI();
            var langCode = welcomeUI.ChooseLanguage();
            CheckOut checkOut = new CheckOut();
            Kjell kjell = new Kjell();

            var choice = welcomeUI.UserInterface(langCode);
            var tran1 = Translator.Translate("User selected Booking", langCode);
            var tran2 = Translator.Translate("User selected Check-in", langCode);
            var tran3 = Translator.Translate("User selected Checkout", langCode);

            var c1 = Translator.Translate("Booking", langCode);
            var c2 = Translator.Translate("Check-in", langCode);
            var c3 = Translator.Translate("Checkout", langCode);

            if (choice == c1)
            {
                Console.WriteLine(tran1);
                // Launch the booking UI (Spectre Console-based)
                bookingConsoleUi.RunMainMenu();
            }
            else if (choice == c2)
            {
                Console.WriteLine(tran2);

                Console.Clear();
                Console.WriteLine(@"\n======================================================\n         *** REGISTER NEW GHOST ***             \n======================================================");
                Console.ResetColor();

                var ghost = new GhostCustom.Ghost();

                Console.Write("\n[1] Enter ghost name: ");
                ghost.Name = Console.ReadLine() ?? "Unknown Ghost";

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

                Console.Write("[3] Does the ghost wear rattling chains? (y/n): ");
                string chainInput = Console.ReadLine()?.ToLower() ?? "";
                ghost.HasChains = chainInput == "y" || chainInput == "yes";

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

                Console.WriteLine("\n==================================================");
                Console.Write("Would you like to register another ghost? (y/n): ");
                string continueInput = Console.ReadLine()?.ToLower() ?? "";
                if (continueInput != "y" && continueInput != "yes")
                {
                    keepRunning = false;
                }
            }
            else if (choice == c3)
            {
                Console.WriteLine(tran3);
                kjell.MenuChoice();
                kjell.MenuChoice();
                kjell.MenuChoice();
                Console.ReadKey();
            }

            if (keepRunning)
            {
                Console.WriteLine("\nReturn to main menu? (y/n): ");
                string menuInput = Console.ReadLine()?.ToLower() ?? "";
                if (menuInput != "y" && menuInput != "yes")
                {
                    keepRunning = false;
                }
            }
        }

        Console.WriteLine("\nThank you for visiting Hotel Hjortronet for Compliance!");
    }
}


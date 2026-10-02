using System.Linq.Expressions;
using GTranslate.Translators;

namespace Hildur4._0;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        WelcomeUI welcomeUI = new WelcomeUI();
        welcomeUI.UserInterface();

        switch (welcomeUI.UserInterface())
        {
            case "Booking":
                Console.WriteLine("User selected Booking");
                break;
            case "Check-in":
                Console.WriteLine("User selected Check-in");
                break;
            case "Checkout":
                Console.WriteLine("User selected Checkout");
                break;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
}

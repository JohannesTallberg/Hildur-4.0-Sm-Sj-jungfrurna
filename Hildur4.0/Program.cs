using Hildur4._0.Booking;

namespace Hildur4._0;

class Program
{
    static void Main(string[] args)
    {
        var bookingService = new BookingService();

        // 2. Initialize the Spectre Console UI and pass the service
        var consoleUi = new BookingConsoleUI(bookingService);

        // 3. Launch the interactive menu
        consoleUi.RunMainMenu();
    }
}

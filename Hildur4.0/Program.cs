using Hildur4._0.Booking;

namespace Hildur4._0;

class Program
{
    static void Main(string[] args)
    {
        var bookingService = new BookingService();

        Console.WriteLine($"Current Guest: {bookingService.ActiveGuestName}\n");

        // Display all Funny House rooms
        Console.WriteLine("--- Available Rooms ---");
        foreach (var room in bookingService.GetAllRooms())
        {
            Console.WriteLine($"Room {room.RoomNumber} | Theme: {room.Type} | {room.BasePricePerNight} SEK/night | Max Capacity: {room.MaxCapacity}");
        }
    }
}

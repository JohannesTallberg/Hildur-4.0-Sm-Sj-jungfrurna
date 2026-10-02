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

        Console.WriteLine("\n[ACTION] Booking Room 101 for 3 nights starting tomorrow...");
        var booking = bookingService.CreateBooking(101, DateTime.Now.Date.AddDays(1), 3);

        if (booking != null)
        {
            Console.WriteLine($"\n[SUCCESS] Reservation #{booking.BookingId} Confirmed!");
            Console.WriteLine($"Guest: {booking.GuestName}");
            Console.WriteLine($"Room: {booking.RoomNumber}");
            Console.WriteLine($"Stay: {booking.CheckInDate:yyyy-MM-dd} to {booking.CheckOutDate:yyyy-MM-dd} ({booking.Nights} nights)");
            Console.WriteLine($"Total Cost: {booking.TotalPrice} SEK");
        }

        bookingService.DebugDumpBookings();
        bookingService.DebugDumpRooms();

    }
}

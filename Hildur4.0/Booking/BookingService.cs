namespace Hildur4._0.Booking;

public class BookingService
{
    // In-memory data storage
    private readonly List<FunnyHouseRoom> _rooms = new();
    private readonly List<Booking> _bookings = new();
    private int _nextBookingId = 1;

    // Minimum allowed price per night to prevent Kjell the Cat from setting rates to 0 or negative
    private const decimal MinimumSafePrice = 100.00m;

    // Single active user for the system
    public string ActiveGuestName { get; set; } = "Casper the Friendly Ghost";

    public BookingService()
    {
        SeedDefaultRooms();
    }

    private void SeedDefaultRooms()
    {
        _rooms.Add(new FunnyHouseRoom(101, FunnyHouseType.GravityReversed, 450.00m, 2, true));
        _rooms.Add(new FunnyHouseRoom(102, FunnyHouseType.EndlessHallway, 350.00m, 4, false));
        _rooms.Add(new FunnyHouseRoom(103, FunnyHouseType.UpsideDownSuite, 500.00m, 3, true));
        _rooms.Add(new FunnyHouseRoom(104, FunnyHouseType.MirageMirror, 400.00m, 2, false));
    }

    public List<FunnyHouseRoom> GetAllRooms() => _rooms;

    public bool IsRoomAvailable(int roomNumber, DateTime checkIn, int nights)
    {
        DateTime requestedCheckOut = checkIn.AddDays(nights);

        foreach (var booking in _bookings)
        {
            if (booking.RoomNumber == roomNumber)
            {
                bool overlaps = checkIn < booking.CheckOutDate && requestedCheckOut > booking.CheckInDate;
                if (overlaps) return false;
            }
        }
        return true;
    }

    public decimal CalculateTotalPrice(FunnyHouseRoom room, int nights)
    {
        decimal effectiveRate = room.BasePricePerNight < MinimumSafePrice
            ? MinimumSafePrice
            : room.BasePricePerNight;

        return effectiveRate * nights;
    }

    // Creates a booking directly for the single user
    public Booking? CreateBooking(int roomNumber, DateTime checkIn, int nights)
    {
        var room = _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

        if (room == null)
        {
            Console.WriteLine($"[ERROR] Room {roomNumber} does not exist in the Funny House.");
            return null;
        }

        if (!IsRoomAvailable(roomNumber, checkIn, nights))
        {
            Console.WriteLine($"[ERROR] Room {roomNumber} is already booked for these dates.");
            return null;
        }

        decimal totalPrice = CalculateTotalPrice(room, nights);

        var newBooking = new Booking
        {
            BookingId = _nextBookingId++,
            GuestName = ActiveGuestName,
            RoomNumber = roomNumber,
            CheckInDate = checkIn,
            Nights = nights,
            TotalPrice = totalPrice
        };

        _bookings.Add(newBooking);
        return newBooking;
    }

    public List<Booking> GetBookingsForSingleUser()
    {
        return _bookings;
    }
}
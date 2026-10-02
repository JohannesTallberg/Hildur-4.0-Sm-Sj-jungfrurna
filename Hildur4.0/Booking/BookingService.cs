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

    // ... existing code, fields, and constructors ...

    #region Debug Helpers

    /// <summary>
    /// Prints all internal room data and settings to the console.
    /// </summary>
    public void DebugDumpRooms()
    {
        Console.WriteLine("\n=================== [DEBUG: ROOMS DUMP] ===================");
        Console.WriteLine($"Total Rooms Loaded: {_rooms.Count}");
        Console.WriteLine("-----------------------------------------------------------");

        if (_rooms.Count == 0)
        {
            Console.WriteLine(" No rooms found in memory.");
        }
        else
        {
            foreach (var room in _rooms)
            {
                Console.WriteLine($"[Room {room.RoomNumber}]");
                Console.WriteLine($"  ├── Theme:            {room.Type}");
                Console.WriteLine($"  ├── Base Price:       {room.BasePricePerNight} SEK");
                Console.WriteLine($"  ├── Max Capacity:     {room.MaxCapacity} Ghosts");
                Console.WriteLine($"  └── Ectoplasm Proof:  {room.IsEctoplasmProof}");
            }
        }

        Console.WriteLine("===========================================================\n");
    }

    /// <summary>
    /// Prints all stored booking records and date ranges to the console.
    /// </summary>
    public void DebugDumpBookings()
    {
        Console.WriteLine("\n================== [DEBUG: BOOKINGS DUMP] ==================");
        Console.WriteLine($"Total Active Bookings: {_bookings.Count}");
        Console.WriteLine("-----------------------------------------------------------");

        if (_bookings.Count == 0)
        {
            Console.WriteLine(" No bookings currently stored in memory.");
        }
        else
        {
            foreach (var b in _bookings)
            {
                Console.WriteLine($"[Booking #{b.BookingId}]");
                Console.WriteLine($"  ├── Guest:       {b.GuestName}");
                Console.WriteLine($"  ├── Room Number: {b.RoomNumber}");
                Console.WriteLine($"  ├── Check-In:    {b.CheckInDate:yyyy-MM-dd}");
                Console.WriteLine($"  ├── Check-Out:   {b.CheckOutDate:yyyy-MM-dd} ({b.Nights} nights)");
                Console.WriteLine($"  ├── Total Cost:  {b.TotalPrice} SEK");
                Console.WriteLine($"  └── Created At:  {b.CreatedAt:yyyy-MM-dd HH:mm:ss}");
            }
        }

        Console.WriteLine("===========================================================\n");
    }

    #endregion
}
namespace Hildur4._0.Booking;

// Types of room themes available in the Funny House category
public enum FunnyHouseType
{
    GravityReversed,  // Ceiling is the floor
    EndlessHallway,   // Visually infinite room
    UpsideDownSuite,  // Heat flows down, furniture on ceiling
    MirageMirror      // Reflections act independently
}

// Represents a Funny House room entity
public class FunnyHouseRoom
{
    public int RoomNumber { get; set; }
    public FunnyHouseType Type { get; set; }
    public decimal BasePricePerNight { get; set; }
    public int MaxCapacity { get; set; }
    public bool IsEctoplasmProof { get; set; }

    public FunnyHouseRoom(int roomNumber, FunnyHouseType type, decimal basePricePerNight, int maxCapacity, bool isEctoplasmProof)
    {
        RoomNumber = roomNumber;
        Type = type;
        BasePricePerNight = basePricePerNight;
        MaxCapacity = maxCapacity;
        IsEctoplasmProof = isEctoplasmProof;
    }
}

// Represents a reservation made by the single ghost guest
public class Booking
{
    public int BookingId { get; set; }
    public string GuestName { get; set; } = "Default Ghost"; // Single user name
    public int RoomNumber { get; set; }
    public DateTime CheckInDate { get; set; }
    public int Nights { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime CheckOutDate => CheckInDate.AddDays(Nights);
}
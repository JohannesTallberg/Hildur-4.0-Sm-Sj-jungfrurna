using Spectre.Console;

namespace Hildur4._0.Booking;

public class BookingConsoleUI
{
    private readonly BookingService _bookingService;

    public BookingConsoleUI(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public void RunMainMenu()
    {
        while (true)
        {
            AnsiConsole.Clear();
            RenderHeader();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]What would you like to do?[/]")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "1. View All Funny House Rooms",
                        "2. Search Available Rooms",
                        "3. Book a Room",
                        "4. View Active Bookings",
                        "5. Dump Internal State",
                        "6. Exit"
                    }));

            switch (choice[0])
            {
                case '1':
                    ShowAllRooms();
                    break;
                case '2':
                    SearchAvailableRooms();
                    break;
                case '3':
                    CreateBookingPrompt();
                    break;
                case '4':
                    ShowAllBookings();
                    break;
                case '5':
                    ShowDebugDump();
                    break;
                case '6':
                    AnsiConsole.MarkupLine("\n[bold silver]Goodbye! Beware of Kjell the Cat...[/]");
                    return;
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[grey]Press any key to return to main menu...[/]");
            Console.ReadKey(true);
        }
    }

    private void RenderHeader()
    {
        var figlet = new FigletText("Hildur 4.0")
            .LeftJustified()
            .Color(Spectre.Console.Color.MediumPurple);

        AnsiConsole.Write(figlet);

        var panel = new Panel(
            "[bold cyan]HOTEL HJORTRONET[/] - [italic silver]Afterlife Edition[/]\n" +
            $"[grey]Guest On Duty:[/] [bold yellow]{_bookingService.ActiveGuestName}[/]")
        {
            Border = BoxBorder.Rounded,
            Padding = new Padding(1, 0, 1, 0),
            Header = new PanelHeader("[bold purple] Funny House Desk [/]")
        };

        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();
    }

    private void ShowAllRooms()
    {
        AnsiConsole.Clear();
        RenderHeader();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title("[bold yellow]Funny House Rooms[/]");

        table.AddColumn(new TableColumn("[bold]Room #[/]"));
        table.AddColumn(new TableColumn("[bold]Theme[/]"));
        table.AddColumn(new TableColumn("[bold]Rate (SEK)[/]"));
        table.AddColumn(new TableColumn("[bold]Max Ghosts[/]"));
        table.AddColumn(new TableColumn("[bold]Ectoplasm Proof?[/]"));

        foreach (var room in _bookingService.GetAllRooms())
        {
            table.AddRow(
                $"[bold cyan]{room.RoomNumber}[/]",
                $"[magenta]{room.Type}[/]",
                $"[green]{room.BasePricePerNight:N2}[/]",
                $"{room.MaxCapacity}",
                room.IsEctoplasmProof ? "[green]Yes[/]" : "[red]No[/]"
            );
        }

        AnsiConsole.Write(table);
    }

    private void SearchAvailableRooms()
    {
        AnsiConsole.Clear();
        RenderHeader();
        AnsiConsole.MarkupLine("[bold yellow]Search Room Availability[/]\n");

        int nights = AnsiConsole.Ask<int>("How many [cyan]nights[/] stay? ", 1);
        int daysAhead = AnsiConsole.Ask<int>("How many [cyan]days from today[/] for check-in? ", 1);

        DateTime checkIn = DateTime.Now.Date.AddDays(daysAhead);

        var table = new Table().Border(TableBorder.Rounded);
        table.AddColumn("Room #");
        table.AddColumn("Theme");
        table.AddColumn("Rate/Night");
        table.AddColumn("Total Price");
        table.AddColumn("Status");

        foreach (var room in _bookingService.GetAllRooms())
        {
            bool isFree = _bookingService.IsRoomAvailable(room.RoomNumber, checkIn, nights);
            decimal total = _bookingService.CalculateTotalPrice(room, nights);

            table.AddRow(
                $"{room.RoomNumber}",
                $"{room.Type}",
                $"{room.BasePricePerNight:N2} SEK",
                $"{total:N2} SEK",
                isFree ? "[bold green]AVAILABLE[/]" : "[bold red]BOOKED[/]"
            );
        }

        AnsiConsole.MarkupLine($"\n[grey]Checking range:[/] [yellow]{checkIn:yyyy-MM-dd}[/] to [yellow]{checkIn.AddDays(nights):yyyy-MM-dd}[/]");
        AnsiConsole.Write(table);
    }

    private void CreateBookingPrompt()
    {
        AnsiConsole.Clear();
        RenderHeader();
        AnsiConsole.MarkupLine("[bold yellow]New Reservation[/]\n");

        var availableRooms = _bookingService.GetAllRooms();
        var roomChoices = availableRooms.Select(r => r.RoomNumber).ToArray();

        int selectedRoomNumber = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("Select a [cyan]Room Number[/]:")
                .AddChoices(roomChoices));

        int nights = AnsiConsole.Ask<int>("Number of [cyan]nights[/]:", 1);
        int startInDays = AnsiConsole.Ask<int>("Check-in in how many [cyan]days from today[/]?", 1);

        DateTime checkIn = DateTime.Now.Date.AddDays(startInDays);

        AnsiConsole.Status()
            .Start("Processing reservation with Hildur 4.0...", ctx =>
            {
                Thread.Sleep(800);
            });

        var booking = _bookingService.CreateBooking(selectedRoomNumber, checkIn, nights);

        if (booking != null)
        {
            var confirmationPanel = new Panel(
                $"[bold green]RESERVATION CONFIRMED![/]\n\n" +
                $"[grey]Booking ID:[/]   [bold]{booking.BookingId}[/]\n" +
                $"[grey]Guest:[/]        [yellow]{booking.GuestName}[/]\n" +
                $"[grey]Room:[/]         [cyan]{booking.RoomNumber}[/]\n" +
                $"[grey]Check-In:[/]     {booking.CheckInDate:yyyy-MM-dd}\n" +
                $"[grey]Check-Out:[/]    {booking.CheckOutDate:yyyy-MM-dd} ({booking.Nights} nights)\n" +
                $"[grey]Total Price:[/]  [green]{booking.TotalPrice:N2} SEK[/]"
            )
            {
                Border = BoxBorder.Heavy
            };

            confirmationPanel.BorderColor(Spectre.Console.Color.Green);

            AnsiConsole.Write(confirmationPanel);
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Booking failed![/] Room is unavailable for those dates.");
        }
    }

    private void ShowAllBookings()
    {
        AnsiConsole.Clear();
        RenderHeader();

        var bookings = _bookingService.GetBookingsForSingleUser();

        if (bookings.Count == 0)
        {
            AnsiConsole.MarkupLine("[italic red]No active reservations found.[/]");
            return;
        }

        var table = new Table().Border(TableBorder.Square);
        table.AddColumn("#");
        table.AddColumn("Guest");
        table.AddColumn("Room");
        table.AddColumn("Check-In");
        table.AddColumn("Check-Out");
        table.AddColumn("Total Price");

        foreach (var b in bookings)
        {
            table.AddRow(
                $"{b.BookingId}",
                $"[yellow]{b.GuestName}[/]",
                $"[cyan]{b.RoomNumber}[/]",
                $"{b.CheckInDate:yyyy-MM-dd}",
                $"{b.CheckOutDate:yyyy-MM-dd}",
                $"[green]{b.TotalPrice:N2} SEK[/]"
            );
        }

        AnsiConsole.Write(table);
    }

    private void ShowDebugDump()
    {
        AnsiConsole.Clear();
        RenderHeader();

        _bookingService.DebugDumpRooms();
        _bookingService.DebugDumpBookings();
    }
}
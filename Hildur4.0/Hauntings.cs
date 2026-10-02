using System;
using System.Collections.Generic;
using System.Linq;

public record Person(string FirstName, string LastName, int Age)
{
    public string FullName => $"{FirstName} {LastName}";
}

public class Haunter
{
    private static readonly string[] FirstNames =
        { "Erik", "Anna", "Lars", "Karin", "Johan", "Maria", "Olof", "Ingrid", "Gustav", "Astrid",
          "Nils", "Elsa", "Anders", "Sofia", "Magnus", "Linnea", "Per", "Frida", "Björn", "Saga" };

    private static readonly string[] LastNames =
        { "Andersson", "Johansson", "Karlsson", "Nilsson", "Eriksson", "Larsson", "Olsson",
          "Persson", "Svensson", "Gustafsson", "Pettersson", "Jonsson", "Jansson", "Hansson",
          "Bengtsson", "Lindqvist", "Lindberg", "Magnusson", "Lindström", "Holmberg" };

    // Each haunting has several possible outcomes. {0} is the victim's first name.
    private static readonly List<(string Name, string[] Outcomes)> Hauntings = new()
    {
        ("Close a door", new[]
        {
            "A door swings shut behind {0}. They glance back, but nobody's there.",
            "{0} hears a soft click as the door closes itself. They decide not to open it again.",
            "The door shuts quietly. {0} shrugs and blames the draft."
        }),
        ("Flicker the lights", new[]
        {
            "The lights stutter and die for a moment. {0} freezes in the dark.",
            "The bulb flickers in a strange rhythm. {0} swears it looks like Morse code.",
            "The lights buzz and dim. {0} mutters something about the wiring."
        }),
        ("Create a cold spot", new[]
        {
            "{0} walks into a pocket of icy air and their breath fogs in front of them.",
            "A chill creeps up {0}'s spine. They pull their sleeves down and hurry on.",
            "{0} zips up their jacket indoors and has no idea why."
        }),
        ("Whisper their name", new[]
        {
            "A faint voice whispers \"{0}...\" from the empty room next door.",
            "{0} hears their name, very close to their ear. They spin around. Nothing.",
            "A whisper drifts by, but {0} doesn't seem to notice."
        }),
        ("Move an object", new[]
        {
            "A mug slides across the table on its own. {0} stares at it, wide-eyed.",
            "{0}'s keys are suddenly in the fridge. They are very confused.",
            "A picture frame tilts crooked. {0} straightens it, and it tilts back."
        }),
        ("Show yourself", new[]
        {
            "{0} sees a pale figure at the end of the hall. When they blink, it's gone.",
            "In the dark window, {0} sees a second reflection standing behind them.",
            "{0} screams, drops everything, and runs out of the house."
        })
    };

    private readonly Random _rng = new();

    public List<Person> GeneratePeople(int count) =>
        Enumerable.Range(0, count)
            .Select(_ => new Person(Pick(FirstNames), Pick(LastNames), _rng.Next(8, 86)))
            .ToList();

    public string Haunt(Person target, int hauntingIndex)
    {
        var outcomes = Hauntings[hauntingIndex].Outcomes;
        return string.Format(Pick(outcomes), target.FirstName);
    }

    public void Run()
    {
        var people = GeneratePeople(5);
        var personOptions = people.Select(p => $"{p.FullName}, age {p.Age}").ToList();
        var hauntingOptions = Hauntings.Select(h => h.Name).ToList();

        while (true)
        {
            int person = Choose("Who do you want to haunt?", personOptions, "Quit");
            if (person < 0) break;

            int haunting = Choose("How do you want to haunt them?", hauntingOptions, "Back");
            if (haunting < 0) continue;

            Console.WriteLine("\n" + Haunt(people[person], haunting));
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(intercept: true);
        }
    }

    /// <summary>
    /// Arrow-key menu. Up/Down moves, Enter selects, Esc picks the last entry.
    /// Returns the chosen option index, or -1 if the extra cancel entry was chosen.
    /// </summary>
    private int Choose(string title, IList<string> options, string cancelLabel)
    {
        var items = options.Concat(new[] { cancelLabel }).ToList();
        int selected = 0;

        Console.WriteLine($"\n{title}  (Up/Down to move, Enter to select)");
        Console.CursorVisible = false;

        // Draw once to find where the menu starts, even if the console scrolled.
        Draw(items, selected);
        int top = Console.CursorTop - items.Count;

        try
        {
            while (true)
            {
                var key = Console.ReadKey(intercept: true).Key;

                if (key == ConsoleKey.UpArrow)
                    selected = (selected - 1 + items.Count) % items.Count;
                else if (key == ConsoleKey.DownArrow)
                    selected = (selected + 1) % items.Count;
                else if (key == ConsoleKey.Escape)
                    return -1;
                else if (key == ConsoleKey.Enter)
                    return selected == items.Count - 1 ? -1 : selected;
                else
                    continue;

                Console.SetCursorPosition(0, top);
                Draw(items, selected);
            }
        }
        finally
        {
            Console.CursorVisible = true;
        }
    }

    private static void Draw(IList<string> items, int selected)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i == selected)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"> {items[i]}".PadRight(Console.WindowWidth - 1));
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine($"  {items[i]}".PadRight(Console.WindowWidth - 1));
            }
        }
    }

    private T Pick<T>(IReadOnlyList<T> items) => items[_rng.Next(items.Count)];
}

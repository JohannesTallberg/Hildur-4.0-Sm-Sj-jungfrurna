using Spectre.Console;
using System;
using System.Linq;

namespace Hildur4._0
{
    public class WelcomeUI
    {
        /// <summary>
        /// Show a simple welcome UI with three choices using Spectre.Console.
        /// Returns the selected option as a string.
        /// </summary>
        public string UserInterface()
        {
            AnsiConsole.Clear();

            var title = "Hildur 4.0";
            var art = "          #******#          \n      ****************      \n    +*******************    \n  ************************  \n  ************************  \n  ******    +***    ******  \n#*******    +**+    *******#\n#*******----****----*******#\n#**************************#\n#***++++****++++****++++***#\n#***.   #***    ****    ***#\n#*. +***    +**+    ***+ .*#\n#**************************#\n#***  ******    ******  ***#\n#*      ****    ****      *#";

            var messages = new[]
            {
                "Welcome, guest — please leave your past at the door.",
                "Afterlife Hotel: reservations are forever.",
                "Rooms with a view of eternity. Relax and roam.",
                "The lobby between heartbeats welcomes you.",
                "Make yourself at home among the stars."
            };

            var random = new Random();
            var message = messages[random.Next(messages.Length)];

            var content = string.Join('\n', new[] { message, "", art });

            // Show panel with title, message and art
            var panel = new Panel(content)
                .Padding(1, 1)
                .Border(BoxBorder.Rounded)
                .Header(title, Justify.Center);

            AnsiConsole.Write(panel);

            var choices = new[] { "Booking", "Check-in", "Checkout" };

            var prompt = new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(choices);

            var choice = AnsiConsole.Prompt(prompt);

            AnsiConsole.MarkupLine($"\n[green]Selected:[/] {choice}\n");
            return choice;
        }
    }
}

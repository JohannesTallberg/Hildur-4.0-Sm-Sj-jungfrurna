using System.Linq.Expressions;
using GTranslate.Translators;
namespace Hildur4._0;

using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
        static void Main(string[] args)
        {
            WelcomeUI welcomeUI = new WelcomeUI();
            var LangCode = welcomeUI.ChooseLanguage();
            CheckOut checkOut = new CheckOut();
            Kjell kjell = new Kjell();

            var choice = welcomeUI.UserInterface(LangCode);
            var tran1 = Translator.Translate("User selected Booking", LangCode);
            var tran2 = Translator.Translate("User selected Check-in", LangCode);
            var tran3 = Translator.Translate("User selected Checkout", LangCode);

            var c1 = Translator.Translate("Booking", LangCode);
            var c2 = Translator.Translate("Check-in", LangCode);
            var c3 = Translator.Translate("Checkout", LangCode);

        string s1 = c1;

        if (choice == c1)
        {
            Console.WriteLine(tran1);
        }
        else if (choice == c2)
        {
            Console.WriteLine(tran2);
        }
        else if (choice == c3)
        {
            Console.WriteLine(tran3);

            kjell.MenuChoice();
            kjell.MenuChoice();
            kjell.MenuChoice();

            Console.ReadKey();

        }



    }

}


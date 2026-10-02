using System.Linq.Expressions;
using GTranslate.Translators;

namespace Hildur4._0;

using System;

class Program
{
        static void Main(string[] args)
        {
            WelcomeUI welcomeUI = new WelcomeUI();
            var LangCode = welcomeUI.ChooseLanguage();

            var choice = welcomeUI.UserInterface(LangCode);

            switch (choice)
            {
                case "Booking":
                    var tran1 = Translator.Translate("User selected Booking", LangCode);
                    Console.WriteLine(tran1);
                    break;
                case "Check-in":
                    var tran2 = Translator.Translate("User selected Check-in", LangCode);
                    Console.WriteLine(tran2);
                    break;
                case "Checkout":
                    var tran3 = Translator.Translate("User selected Checkout", LangCode);
                    Console.WriteLine(tran3);

                    CheckOut checkOut = new CheckOut();
                    checkOut.Start();
                    break;
                default:
                    var tran4 = Translator.Translate("User selected an unknown option", LangCode);
                    Console.WriteLine(tran4);
                    break;
            }
        }
}


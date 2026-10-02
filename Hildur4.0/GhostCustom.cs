using System;
using System.Collections.Generic;
using System.Text;

namespace Hildur4._0
{
    internal class GhostCustom
    {
        public class UnfinishedBusiness
        {
            public string Description { get; set; } = string.Empty;
            public bool IsResolved { get; set; }
        }

        public class Ghost
        {
            public string Name { get; set; } = string.Empty;
            public int YearOfPassing { get; set; }
            public bool HasChains { get; set; }
            public List<UnfinishedBusiness> BusinessList { get; set; } = new();
        }

        public class ComplianceChecker
        {
            public (bool IsCompliant, string Reason) ValidateCheckIn(Ghost ghost)
            {
                // Regel 1: Ett spöke MÅSTE ha minst ett ouppklarat ärende
                if (!ghost.BusinessList.Any())
                {
                    return (false, "Denied check-in: The ghost has no unresolved business (must move on to the afterlife).");
                }

                // Regel 2: Om spöket har varit döpt/död i över 300 år måste kedjorna vara oljade
                if (DateTime.Now.Year - ghost.YearOfPassing > 300 && !ghost.HasChains)
                {
                    return (false, "Denied check-in: Ancient ghosts over 300 years old must wear approved rattling chains.");
                }

                return (true, "Compliance approved: Warmly welcome to Hotel Hjortronet. \n " +
                    "Your check-in is now complete, and we want you to feel entirely\n " +
                    "safe during your stay with us.\n" +
                    "Our safety pledge means that all your earthly memories, \n " +
                    "secrets, and personal data are handled with the utmost \n" +
                    "security in our cryptographic ether archive. \n" +
                    "Our AI assistant HILDUR is used in a completely responsible manner, \n" +
                    "and she personally guarantees that the Devil will under no circumstances\n " +
                    "gain access to your soul as long as you are checked in here. \n" +
                    "However, we urge you to be extra attentive and watch \n" +
                    "out for Kjell the hotel cat – he is a mischievoussaboteur who \n" +
                    " loves hiding keys, causing disruptions, \n" +
                    "and playing pranks on our guests.\n " +
                    "Your time here is your own, where you can book various activities.\n " +
                    "Your check-out occurs only once you have resolved \n" +
                    "your unfinished business and found your peace. \n" +
                    " We wish you a successful and peaceful stay.\n " +
                    "Kind regards from all of us at Hotel Hjortronet!\n");
                
            }
        }
    }
}



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

                return (true, "Compliance approved: Welcome to the Hotel!");
            }
        }
    }
}



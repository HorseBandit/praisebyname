using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermOneSWE.Utilities
{
    public enum DmgOutcome
    {
        NoEffect, // No damage taken, and no accessory was removed (e.g., already lost accessory)
        DamageDealt, // Damage was successfully dealt to the zombie's health
        AccessoryRemoved, // An accessory was removed, but no health damage was taken
    }
}

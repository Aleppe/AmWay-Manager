using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Persona
    {
        partial void Nominativo_Compute(ref string result)
        {
            // Impostare il risultato sul valore del campo desiderato
            result = string.Concat(Nome, " ", Cognome);
        }
    }
}
using Microsoft.LightSwitch;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;

namespace LightSwitchApplication
{
    public partial class Acquisto
    {
        partial void SpesaTotale_Compute(ref decimal result)
        {
            // Impostare il risultato sul valore del campo desiderato
            result = 0;
            foreach (ArticoliAcquistati item in ArticoliAcquistatiCollection)
            {
                if (item.Articolo != null)
                    result = item.Articolo.Prezzo + result;
            }
        }
    }
}
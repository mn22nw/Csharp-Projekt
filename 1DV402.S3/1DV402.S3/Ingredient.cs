using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S3
{
    struct Ingredient 
    {
        /* För att lagra information om en ingrediens ska en struktur användas. Strukturen ska vara enkelt 
            utformad och bara ha autoimplementerade egenskaper, vilket innebär att varken mängd, mått eller 
            namn på något sätt ska valideras. Den ska dock överskugga metoden ToString() så att en 
            textbeskrivning av en ingrediens kan fås på ett enkelt sätt.*/ 

        public string Amount { get; set; } // mängden det ka vara av en ingrediens
        public string Measure { get; set; }  // vilket mått ingrediensen ska använda
        public string Name { get; set; } // namnet på ingrediensen


        public override string ToString() // Ska enkelt beskriva en ingrediens 
        {
            return String.Format("{0}{1} {2}", Amount, Measure, Name );    
        }

    }   
}

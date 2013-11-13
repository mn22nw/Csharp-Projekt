using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S3
{
    struct Ingredient
    {
        public string Amount { get; set; }
        public string Measure { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
           
            return String.Format("{0} {1} {2}", Amount, Measure, Name );    
        }

    }
}

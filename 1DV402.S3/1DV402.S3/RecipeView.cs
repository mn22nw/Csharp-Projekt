using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S3
{
    class RecipeView
    {
        //En instans av klassen används för att skriva ut recept i ett konsolfönster

      /* ----  Metoderna Render() ska överlagras, d.v.s. det ska finnas två metoder med samma namn men med olika parameterlistor ----*/

        public void Render(IList<Recipe> recipes) //ska skriva ut samtliga recept i samlingen som skickades med som argument vid anropet av metoden
        {
            foreach (Recipe Name in recipes) 
            {
                Console.WriteLine(Name); 
            }

        
            
          Console.ReadLine();

        }

       /* public void Render(Recipe recipe) // ska skriva ut receptet som skickades med som argument vid anropet av metoden
        {
        }*/

        private void RenderHeader(string header)
        {
           /* Den privata metoden RenderHeader(string header) används för att skriva ut ett recepts rubrik
            innehållande receptets namn. Metoden anropas av metoderna Render(). */
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ╔══════════════════════════════════════╗ ");
            Console.WriteLine(" ║                 {0}                  ║ ", header);
            Console.WriteLine(" ╚══════════════════════════════════════╝ ");
            Console.ResetColor();


        }
    }
}

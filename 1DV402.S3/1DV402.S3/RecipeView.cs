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
           
            foreach (Recipe a in recipes) 
            {
                Render(recipes);
            }
          
          Console.ReadLine();
        }

       public void Render(Recipe recipe) // ska skriva ut receptet som skickades med som argument vid anropet av metoden
        {
            RenderHeader(recipe.Name);
            int numberBefore = 1;

            Console.WriteLine("\nIngredienser:");
            Console.WriteLine("_____________________________________\n");

            foreach (Ingredient a in recipe.Ingredients)
            {
                Console.Write("{0}\n", a);
            }

           Console.WriteLine("\nInstruktioner:");
           Console.WriteLine("_____________________________________\n");

            foreach (string a in recipe.Directions)
            {
                Console.WriteLine("<{0}>\n{1}", numberBefore++, a);
            }
        }

        private void RenderHeader(string header)
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine(" ╔══════════════════════════════════════╗ ");
            Console.WriteLine(String.Format("{0,-10}{1,10} {2,10}", " ║", header, "║ "));
            Console.WriteLine(" ╚══════════════════════════════════════╝ ");
            Console.ResetColor();

        }
    }
}

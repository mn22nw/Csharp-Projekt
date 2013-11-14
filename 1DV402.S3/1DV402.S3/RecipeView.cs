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
                RenderHeader(a.Name);  
                // här mäste det ligga koden som skriver ut hela recepten med detaljer etc!
            }
          Console.ReadLine();

        }

       public void Render(Recipe recipe) // ska skriva ut receptet som skickades med som argument vid anropet av metoden
        {
            //Här ska bara det recept som är valt skrivas ut//
            RenderHeader(recipe.Name);
            // här mäste det ligga koden som skriver ut hela det valda receptet med detaljer etc!

            //**** skriver ut ord**** //
            int hej = 0;
            Console.WriteLine("ALLA ORD MED COMMATECNKNK!!!!!!");
        //    foreach (string word in measures)
        //    { Console.Write(" {0}", measures[hej++]); }

            /*     Recipe newIngredientInR1 = new Recipe("hej");
                 foreach (Ingredient ingrediensObj in newIngredientInR1.Ingredients)
                 {
                     Console.WriteLine("RUMPLEEE {0}", ingrediensObj);
                 }*/
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

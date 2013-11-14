using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S3
{
    class Program
    {
        static void Main(string[] args)
        {   /*Metoden Main ska anropa metoden GetMenuChoice() för att visa en meny. Så länge som användaren 
            inte väljer att avsluta applikationen, genom att välja menyalternativet 0, ska menyn visas på nytt efter 
            att något av övriga menykommandon utförts.
            Beroende på vilket menykommando användaren väljer ska metoderna LoadRecipes(), 
            SaveRecipes(), CreateRecipe(), DeleteRecipe() eller ViewRecipe() anropas.*/

            do
            {
                
                RecipeRepository listRecipes = new RecipeRepository("recipes.txt");
                
                switch (GetMenuChoice())
                {       
                    case 0: // Programmet ska avslutas
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Programmet kommer nu att avslutas");
                        System.Threading.Thread.Sleep(1500);
                        return;

                    case 1:  // Ska läsa in recept (men inte visa dom)
                        LoadRecipes();
                        break;
                    case 2:   // ska kunna spara recept
                        break;
                    case 3:  // ska kunna ta bort recept
                      //  GetRecipe( );
                        break;
                    case 4:
                        bool viewAll = false;
                        ViewRecipe( listRecipes.Load(), viewAll); 
                       


                      //  Render(Resiperepostgs
                    // Här ska en lista med samtliga recepts namn presenteras varefter användaren ska kunna välja det recept som ska visas.
                   
                        break;
                    case 5:  // Ska visa ALLA recepten sorterade efter receptens namn.
                        //För att visa recept måste det finns recept att visa. Saknas recept ska ett meddelande visas som informerar 
                        //användaren att det inte finns några recept att visa
                        break;

                } // end switch

            } while (true);

            /*public void someMethod(){
    List<String> namesList = buildNamesList(); */

        }


        private static List<Recipe> LoadRecipes()
        {
       /*   Metoden LoadRecipes() läser in recepten från en textfil genom att använda en instans av klassen RecipeRepsoitory.
          Då recepten lästs in utan problem ska ett rättmeddelande visas och en referens till ett List-objekt innehållande referenser till
          Recipe-objekt ska returneras.
          Inträffar ett fel i samband med att recepten läses in ska ett felmeddelande visas och metoden returnera värdet null.*/
            try
            {
                RecipeRepository repository = new RecipeRepository("recipes.txt");  // Ny instans av RecipeRepository
                repository.Load(); // Anropar metoden Load i RecipeRepository, som läser in .txt filen
                ContinueOnKeyPressed();
            }
            catch
            {
                //**** FELMEDDELANDE**** //
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                Console.WriteLine(" ║       FEL! Ett fel inträffade        ║ ");
                Console.WriteLine(" ║        när recepten lästes in        ║ ");
                Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                Console.ResetColor();
                return null;
            }

            //**** skriver ut om det lyckats!**** //
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ╔══════════════════════════════════════╗ ");
            Console.WriteLine(" ║         Recepten har lästs in        ║ ");
            Console.WriteLine(" ╚══════════════════════════════════════╝ ");
            Console.ResetColor();

            return null;
        }

        public static void ViewErrorMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void ContinueOnKeyPressed()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("\n    Tryck tangent för att fortsätta ");
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.Clear();
            Console.CursorVisible = true;
        }

        private static int GetMenuChoice()
        {
            int index;
            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                Console.WriteLine(" ║         Receptsamling med fil        ║ ");
                Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                Console.ResetColor();
                Console.WriteLine("\n - Arkiv ----------------------------------\n");
                Console.WriteLine(" 0. Avsluta.");
                Console.WriteLine(" 1. Öppna textfil med recept.");
                Console.WriteLine(" 2. Spara recept på textfil.");
                Console.WriteLine("\n - Redigera--------------------------------\n");
                Console.WriteLine(" 3. Ta bort recept.");
                Console.WriteLine("\n - Visa------------------------------------\n");
                Console.WriteLine(" 4. Visa recept.");
                Console.WriteLine(" 5. Visa alla recept.");
                Console.WriteLine(" Ange menyval [0-5:]");

                if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index <= 5) // Om TryParse lyckas tolka Console.Readline ger den true och då körs "return index"; , out får värdet av inputen. 
                { return index; }

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n FEL! Ange ett nummer mellan 0 och 5. \n");

                ContinueOnKeyPressed(); //fortsätter man trycker på en tangent

            } while (true);
        }

        private static Recipe GetRecipe(string header, List<Recipe> recipes) // presenterar en indexerad lista med samtliga receptens namn
        {
                  
            int index;

            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                Console.WriteLine(" ║         Välj recept att {0}         ║ ", header);
                Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                Console.ResetColor();
                Console.WriteLine("\n 0. Avbryt.");
                Console.WriteLine("\n -----------------------------------------\n");

                int numberBefore = 1;
                int numberOfRecipies = 0;
                foreach (Recipe a in recipes)
                {
                  Console.WriteLine("{0}. {1}", numberBefore++, a.Name);
                  numberOfRecipies += 1;
                }

                Console.WriteLine(" Ange menyval [0-{0}:]", numberOfRecipies);

                if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index <= numberOfRecipies) // Om TryParse lyckas tolka Console.Readline ger den true och då körs "return index"; , out får värdet av inputen. 
                {

                    if (index == 0)
                    {
                      return null;
                    }


                    return recipes[index]; //   returnerar en referens till det recept som blivit valt
                } 
                
               
                

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n FEL! Ange ett nummer mellan 0 och 5. \n");

                ContinueOnKeyPressed(); //fortsätter man trycker på en tangent

            } while (true);

            
        }

  private static void ViewRecipe(List<Recipe> recipes, bool viewAll = false)
        {
            RecipeRepository viewR = new RecipeRepository("recipes.txt");  // Ny instans av RecipeRepository
            Recipe chosenRecipe = GetRecipe("visa", viewR.Load()); //anropar medoden som visar vilka recept man kan välja att visa
            RecipeView showARecipe = new RecipeView();
            if (viewAll == true)
            {
                showARecipe.Render(recipes);
            }
            else
            {
                showARecipe.Render(chosenRecipe); // Anropar metoden som visar 1 recept
            }
            ContinueOnKeyPressed();
            

            


           

           //   showARecipe.Render(listRecipes.Load()); // Load innehåller den sorterade listan


      /* 
       *ViewRecipe() anropar denna metod för att få reda på vilket recept som ska tas bort respektive visas.
          
          ViewRecipe kunna visa ett enskilt recept eller samtliga recept. Metoden har två 
          parametrar. Den första parametern recipes är en referens till listan med referenser till recept. Den 
          andra parametern viewAll, med standardvärdet false, bestämmer om ett eller flera recept ska visas.
          Om ett recept ska visas ska metoden GetRecipe() anropas för att erhålla en referens till receptet. 
          Oavsett om ett eller flera recept ska visas ska en instans av typen RecipeView sköta presentationen.
 */ }
    }
}



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
        {

            //  = GetMenuChoice();

            /*Metoden Main ska anropa metoden GetMenuChoice() för att visa en meny. Så längs som användaren 
            inte väljer att avsluta applikationen, genom att välja menyalternativet 0, ska menyn visas på nytt efter 
            att något av övriga menykommandon utförts.
            Beroende på vilket menykommando användaren väljer ska metoderna LoadRecipes(), 
            SaveRecipes(), CreateRecipe(), DeleteRecipe() eller ViewRecipe() anropas.*/


            try
            { // RecipeRepository add = new RecipeRepository();
                // add.Load();

                RecipeRepository repository = new RecipeRepository();  //anropar 
                repository.Load();
                repository.Path = "recipes.txt";

            }

            catch (ArgumentException ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                ViewErrorMessage(ex.Message);
                Console.ResetColor();
            }

            /*public void someMethod(){
    List<String> namesList = buildNamesList(); 
}*/

          /*  Metoden LoadRecipes
Metoden LoadRecipes() läser in recepten från en textfil genom att använda en instans av klassen 
RecipeRepsoitory.
Då recepten lästs in utan problem ska ett rättmeddelande visas och en referens till ett List-objekt 
innehållande referenser till Recipe-objekt ska returneras.
Inträffar ett fel i samband med att recepten läses in ska ett felmeddelande visas och metoden returnera 
värdet null.*/
        }

        public static void ViewErrorMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
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
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index <= 5)
                { return index; }
            } while (true);
            
            /*
           Metoden GetMenuChoice() ska presentera en meny, läsa in menyalternativet användaren väljer och 
            returnera det heltal som symboliserar menyvalet. Metoden ska validera det användaren matar in så att 
            endast heltal knutna till menykommandon godtas. Matar användaren in något som inte kan tolkas som 
               ett heltal knutet till ett menykommando ska ett felmeddelande visas.
           
           */


        }


    }



}



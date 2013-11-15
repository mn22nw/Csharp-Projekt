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
                        try { LoadRecipes(); }
                        catch
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                            Console.WriteLine(" ║       FEL! Ett fel inträffade        ║ ");
                            Console.WriteLine(" ║        när recepten lästes in        ║ ");
                            Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                            Console.ResetColor();
                        }

                        break;
                    case 2:   // ska kunna spara recept
                        try { SaveRecipes(listRecipes.Load()); }
                        catch
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                            Console.WriteLine(" ║       FEL! Det finns inga recept     ║ ");
                            Console.WriteLine(" ║              att spara               ║ ");
                            Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                            Console.ResetColor(); ContinueOnKeyPressed();
                        }
                        break;
                    case 3:  // ska kunna ta bort recept
                        try { DeleteRecipes(listRecipes.Load()); }
                        catch
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                            Console.WriteLine(" ║       FEL! Det finns inga recept     ║ ");
                            Console.WriteLine(" ║              att ta bort!            ║ ");
                            Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                            Console.ResetColor(); ContinueOnKeyPressed();
                        }
                        break;
                    case 4:
                        bool viewAll = false;
                        try
                        {
                            ViewRecipe(listRecipes.Load(), viewAll);
                        }

                        catch
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                            Console.WriteLine(" ║       FEL! Det finns inga recept     ║ ");
                            Console.WriteLine(" ║               att visa!              ║ ");
                            Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                            Console.ResetColor(); ContinueOnKeyPressed();
                        }
                        break;
                    case 5:  // Ska visa ALLA recepten sorterade efter receptens namn.
                        try { ViewRecipe(listRecipes.Load(), viewAll = true); }
                        catch
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                            Console.WriteLine(" ║       FEL! Det finns inga recept     ║ ");
                            Console.WriteLine(" ║               att visa!              ║ ");
                            Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                            Console.ResetColor(); ContinueOnKeyPressed();
                        }
                        break;
                } // end switch

            } while (true);
        }

        private static void ContinueOnKeyPressed()
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
        private static void DeleteRecipes(List<Recipe> recipes)
        {
            RecipeRepository deleteR = new RecipeRepository("recipes.txt");  // Ny instans av RecipeRepository
            ConsoleKeyInfo readKey = new ConsoleKeyInfo();
            try
            {
                do
                {
                    Recipe chosenRecipe = GetRecipe("ta bort", deleteR.Load()); //anropar medoden som visar vilka recept man kan välja att ta bort

                    if (chosenRecipe == null)
                        break;
                    bool check = false;

                    if (chosenRecipe != null)
                    {  // för att det inte ska köra ett nullobjekt
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Är du säker på att du vill ta bort {0}? [j/N]\n", chosenRecipe.Name);
                        Console.ResetColor();
                        readKey = Console.ReadKey(true);
                        check = !((readKey.Key == ConsoleKey.Y) || (readKey.Key == ConsoleKey.N));
                        switch (readKey.Key)
                        {
                            case ConsoleKey.J:
                                recipes.RemoveAt(1); // tar bort det valda receptet

                                foreach (Recipe a in recipes)
                                { Console.WriteLine("hihi"); }
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                                Console.WriteLine(" ║      Receptet har tagits bort        ║ ");
                                Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                                Console.ResetColor();
                                break;

                            case ConsoleKey.N:
                                break;
                        }
                    }
                    ContinueOnKeyPressed();
                } while (true);
            }

            catch (ArgumentException ex)
            {
                ViewErrorMessage(ex.Message);
            }
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
                Console.WriteLine("\n - Redigera --------------------------------\n");
                Console.WriteLine(" 3. Ta bort recept.");
                Console.WriteLine("\n - Visa ------------------------------------\n");
                Console.WriteLine(" 4. Visa recept.");
                Console.WriteLine(" 5. Visa alla recept.");
                Console.WriteLine(" Ange menyval [0-5:]");

                if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index <= 5) // Om TryParse lyckas tolka Console.Readline ger den true och då körs "return index"; , out får värdet av inputen. 
                { Console.Clear(); return index; }

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
                    return recipes[index - 1]; //   returnerar en referens till det recept som blivit valt
                }

                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n FEL! Ange ett nummer mellan 0 och {0}. \n", numberOfRecipies);

                ContinueOnKeyPressed(); //fortsätter man trycker på en tangent

            } while (true);
        }
        private static List<Recipe> LoadRecipes()
        {
            List<Recipe> listToReturn = new List<Recipe>();
            RecipeRepository repository = new RecipeRepository("recipes.txt");  // Ny instans av RecipeRepository 
            try
            {
                listToReturn = repository.Load(); // Anropar metoden Load i RecipeRepository, som läser in .txt filen   
            }
            catch
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                Console.WriteLine(" ║       FEL! Ett fel inträffade        ║ ");
                Console.WriteLine(" ║        när recepten lästes in        ║ ");
                Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                Console.ResetColor();
                ContinueOnKeyPressed();
                return null;
            }

            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ╔══════════════════════════════════════╗ ");
            Console.WriteLine(" ║         Recepten har lästs in        ║ ");
            Console.WriteLine(" ╚══════════════════════════════════════╝ ");
            Console.ResetColor();
            ContinueOnKeyPressed();

            return listToReturn;
        }

        public static void ViewErrorMessage(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n{0}", message);
            Console.ResetColor();
        }

        private static void SaveRecipes(List<Recipe> recipes)
        {
            try
            {
                RecipeRepository listofRecipes = new RecipeRepository("recipes.txt");

                if (recipes != null)
                {
                    listofRecipes.Save(recipes);
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                    Console.WriteLine(" ║         Recepten har sparats         ║ ");
                    Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                    Console.ResetColor();
                }
            }

            catch (ArgumentException ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                Console.WriteLine(" ║     FEL! Ett fel inträffade då       ║ ");
                Console.WriteLine(" ║       recepten skulle sparas.        ║ ");
                Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                Console.ResetColor();
                ViewErrorMessage(ex.Message);
            }
            ContinueOnKeyPressed();
        }

        private static void ViewRecipe(List<Recipe> recipes, bool viewAll) // Den andra parametern viewAll, med standardvärdet false, bestämmer om ett eller flera recept ska visas.
        {
            try
            {
                RecipeRepository viewR = new RecipeRepository("recipes.txt");  // Ny instans av RecipeRepository
                RecipeView showARecipe = new RecipeView();

                if (viewAll == true)
                {
                    showARecipe.Render(recipes);
                    ContinueOnKeyPressed();
                }
                else
                {
                    do
                    {
                        Recipe chosenRecipe = GetRecipe("visa", viewR.Load()); //anropar medoden som visar vilka recept man kan välja att visa
                        if (chosenRecipe == null)
                            break;
                        if (chosenRecipe != null)
                        {  // för att det inte ska köra ett nullobjekt
                            Console.Clear();
                            showARecipe.Render(chosenRecipe); // Anropar metoden som visar 1 recept
                            ContinueOnKeyPressed();
                        }
                    }
                    while (true);
                }
            }
            catch (ArgumentException ex)
            {
                ViewErrorMessage(ex.Message);
                ContinueOnKeyPressed();
            }
        }
    }
}

//http://stackoverflow.com/questions/17909008/option-yes-no-c-sharp-console



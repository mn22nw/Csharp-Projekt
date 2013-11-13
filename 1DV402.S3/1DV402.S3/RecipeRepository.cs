using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S3
{
    class RecipeRepository
    {
        /*  RecipeRepository ansvarar för allt som har med persistent lagring av recept, d.v.s. klassen har 
               metoder för att läsa recept från en textfil och skriva recept till en textfil.
               Klassen använder i samband med inläsning av recept lämpligen den uppräkningsbara typen RecipeReadStatus för att hålla 
               ordningen på vilken typ av data som lästs in från textfilen.*/

        public enum RecipeReadStatus { Indefinite, New, Ingredient, Direction };

        private string _path = "recipes.txt"; // Privat fält av typen string innehållande sökvägen till den fil en instans av RecipeRepository arbetar mot
        public string Path
        {
            get { return _path; }

            set
            {
                if ((string.IsNullOrWhiteSpace(value))) //kollar att namnet inte refererar till null eller är en tom sträng
                {
                    throw new ArgumentException("Sökvägen är tom!!!");
                }
                _path = value;
            }
        }

        public List<Recipe> Load() // public List<Recipe> Load()
        {
            List<string> measures = new List<string>();  // skapar en lista som kan lagra informationen mellan skiljetecknen
            List<Recipe> listRecipes = new List<Recipe>(); // skapar en lista för recept
          // listRecipes.Add(Ingredient); 

           ///////// List<Ingredient> listIngredients = new List<Ingredient>();
            

            RecipeReadStatus status = new RecipeReadStatus(); // skapar en ny instans av enum RecipeReadStatus 
            
                try
                {
                    using (StreamReader reader = new StreamReader(Path)) // using öppnar/stänger filen automatiskt när man har använt den. 
                    {
                        string line; // skapar en ny variabel string

                        //   Den publika metoden Load() ska läsa in textfilen och tolka den för att skapa en lista med referenser 
                        //    till Recipe-objekt som returneras.
                        Recipe currentRecipe = null;
                   
                        do
                        {
                            line = reader.ReadLine(); // tilldelar variabeln line värdet för en avläst rad
                           if (line == null) { continue; }  // för att inte få Error-meddelandet att instance of object = null
                                                       
                              
                            // Console.WriteLine(measures);

                            if (line == String.Empty)  //fortsätter läsa in ifall det finns en tom rad
                            {
                                continue;
                            }

                            if (line == "[Recept]" || line == "[Ingredienser]" || line == "[Instruktioner]")
                            {

                                if (line == "[Recept]")
                                {
                                    status = RecipeReadStatus.New;  //* RÄTT *//
                                    continue;
                                }

                                if (line == "[Ingredienser]")
                                {

                                    //    Då textfilen tolkas används lämpligen en instans av typen RecipeReadStatus så metoden vet hur den 
                                    //    aktuella raden som lästs in ska tolkas.

                                    //List<RecipeReadStatus> newSaveRecipeName = new List<RecipeReadStatus>();

                                    //  RecipeReadStatus Ingredient = (RecipeReadStatus)System.Enum.Parse(typeof(RecipeReadStatus), "Ingredient");
                                    // Enum.IsDefined(typeof(RecipeReadStatus), Ingredient);
                                    // RecipeReadStatus ingredient = RecipeReadStatus.Ingredient;

                                    status = RecipeReadStatus.Ingredient;  //* RÄTT *//
                                    continue;
                                }

                                if (line == "[Instruktioner]")
                                {
                                    Console.WriteLine("INSTRUKTIOOONER");
                                    status = RecipeReadStatus.Direction;
                                    continue; 
                                }                             
                            } //End of första if-sats
                            else
                            {
                                /*  List<Recipe> recipeTitle = new List<Recipe>();
                                  recipeTitle.Add(new Recipe(nameofReci));
                                  return recipeTitle;*/
                              

                            //    Console.WriteLine("MEASURES {0}",measures[1]);    -INDEX OUT OF RANGE??
                               

                                switch (status)
                                {

                                    case RecipeReadStatus.New ://  nytt recept 
                                    
                                             if(currentRecipe != null)
                                             listRecipes.Add(currentRecipe);
                                            // Skapa nytt receptobjekt med receptets namn
                                            currentRecipe = new Recipe(line);
                                            Console.WriteLine("TITELI \n{0}", currentRecipe.Name);
                                            break;
                                                                               
                                    case RecipeReadStatus.Ingredient: //ingrediens
                                      //   string[] scores = line.Split(new char[] { ';', ' ' },
                                      //    StringSplitOptions.RemoveEmptyEntries); // tar bort mellanslag

                                           
                                    string[] scores = line.Split(';');
                                    int count = scores.Count(); //Räknar ut hur många 
                                    Console.WriteLine("HUR MÅNGA!!!!! {0}", count);
                                    if (count != 3)
                                    {
                                        throw new ArgumentException("Fel antal ingredienser!!");
                                    }
                                   
                                    measures.AddRange(scores); 
 
                                      Ingredient IngrediensObj = new Ingredient();  //Skapar ingrediensobjekt och initiera det med de tre delarna för mängd, mått och namn.
                                      IngrediensObj.Amount = scores[0];
                                      IngrediensObj.Measure = scores[1];
                                      IngrediensObj.Name = scores[2];
                     

                                      currentRecipe.Add(IngrediensObj);//Lägg till ingrediensen till receptets lista med ingredienser.

                                        break;

                                   
                                    case RecipeReadStatus.Direction: //instruktion

                                        //eller om status är satt att raden ska tolkas som en instruktion… 
                                        //1.  Lägg till raden till receptets lista med instruktioner.

                                        Console.Write(status);
                                        Console.WriteLine(line);
                                        break;
                                }
                                continue;
                            }
                        }
                        while (line != null); // avbryter loopen när det inte finns några mer rader med text

                        listRecipes.Add(currentRecipe);
                    }


                }
                catch 
                {
                    //**** FELMEDDELANDE**** //
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" ╔══════════════════════════════════════╗ ");
                    Console.WriteLine(" ║       FEL! Ett fel inträffade        ║ ");
                    Console.WriteLine(" ║        när recepten lästes in!       ║ ");
                    Console.WriteLine(" ╚══════════════════════════════════════╝ ");
                    Console.ResetColor();
                }



                //**** skriver ut ord**** //
                int hej = 0;
                Console.WriteLine("ALLA ORD MED COMMATECNKNK!!!!!!");
                foreach (string word in measures)
                { Console.Write(" {0}", measures[hej++]); }

           /*     Recipe newIngredientInR1 = new Recipe("hej");
                foreach (Ingredient ingrediensObj in newIngredientInR1.Ingredients)
                {
                    Console.WriteLine("RUMPLEEE {0}", ingrediensObj);
                }*/

                return listRecipes.OrderBy(row => row.Name).ToList(); // sorterar raderna efter namn på Recipe
             
        }

        public RecipeRepository(string path)
        {

            Path = path;
            //  Konstruktorn ska initiera fältet _path, via egenskapen Path, så att det instansierade objektet innehåller 
            //en sökväg.
        }

        public void Save(List<Recipe> recipes)
        {
            /*Metoden Save() ska spara de recept som skickas med som argument vid anrop av metoden på en textfil. 
             Recepten ska spara enligt det format som beskrivs under rubriken ’Format på textfil med recept’*/
        }
  
    }
}
// ANVÄNDA LÄNKAR
// http://stackoverflow.com/questions/12314555/how-to-remove-empty-line-when-reading-text-file-using-c-sharp
// http://www.dreamincode.net/forums/topic/38937-working-with-enums-in-c%23/
//http://www.dotnetperls.com/count-array

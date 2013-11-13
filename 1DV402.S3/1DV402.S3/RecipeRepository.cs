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
            List<string> measures = new List<string>(1000);  // skapar en lista som kan lagra informationen mellan skiljetecknen
            List<Recipe> listRecipes = new List<Recipe>(); // skapar en lista för recept
          //  listRecipes.Add(Ingredient); 

           ///////// List<Ingredient> listIngredients = new List<Ingredient>();
            

            RecipeReadStatus status = new RecipeReadStatus(); // skapar en ny instans av enum RecipeReadStatus 
            
                try
                {
                    using (StreamReader reader = new StreamReader(Path)) // using öppnar/stänger filen automatiskt när man har använt den. 
                    {
                        string line; // skapar en ny variabel string
                       
                        


                        //   Den publika metoden Load() ska läsa in textfilen och tolka den för att skapa en lista med referenser 
                        //    till Recipe-objekt som returneras.

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
                                    Console.Write(status);
                                        // Skapa nytt receptobjekt med receptets namn
                                         Recipe nameOfRecipe = new Recipe(line);
                                         Console.WriteLine("TITELI \n{0}", nameOfRecipe.Name);
                                        break;
                                                                               
                                    case RecipeReadStatus.Ingredient: //ingrediens
                                        Console.Write(status);
                                      //   string[] scores = line.Split(new char[] { ';', ' ' },
                                      //    StringSplitOptions.RemoveEmptyEntries); // tar bort mellanslag

                                           string[] scores = line.Split(';');
                                           measures.AddRange(scores);
                                           
                                        var e = from s in scores select s;  
                                        int c = e.Count(); //Räknar ut hur många 
                                        Console.WriteLine("HUR MÅNGA!!!!! {0}",c);
                                      if (c != 3)
                                        {
                                           throw new ArgumentException("Fel antal ingredienser!!");
                                         }

                                      Ingredient IngrediensObj = new Ingredient();  //Skapar ingrediensobjekt och initiera det med de tre delarna för mängd, mått och namn.
                                      IngrediensObj.Amount = scores[0];
                                      IngrediensObj.Measure = scores[1];
                                      IngrediensObj.Name = scores[2];
                      
                                    //  List<Ingredient> listIngredients = new List<Ingredient>();


                                      Recipe newIngredientInR = new Recipe(line);
                                      newIngredientInR.Add(IngrediensObj);

                                   //   Console.WriteLine("RUMPLEEE {0}",newIngredientInR.Ingredients);
                                      

                                    //   listIngredients.Add(IngrediensObj);  // lägger till ingrediensobjectet i en lista med ingredienser

                                  // funkaar?!  listIngredients.Add(IngrediensObj); 

                                        

                               //     ReadOnlyCollection<string> readOnlyDinosaurs = new ReadOnlyCollection<string>(dinosaurs);
                                      

                                                                         
                                    //Lägg till ingrediensen till receptets lista med ingredienser.

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
                    }


                }
                catch (Exception)
                {
                  
                  Console.WriteLine("Det uppstod ett fel vid inläsning av fil");
                }


                //**** skriver ut ord**** //

                int hej = 0;
                Console.WriteLine("ALLA ORD MED COMMATECNKNK!!!!!!");
                foreach (string word in measures)
                { Console.Write(" {0}", measures[hej++]); }

                Recipe newIngredientInR1 = new Recipe("hej");
                foreach (Ingredient ingrediensObj in newIngredientInR1.Ingredients)
                {
                    Console.WriteLine("RUMPLEEE {0}", ingrediensObj);
                }

                return listRecipes;
               


            
            /*


        

    */

            /*1.  Skapa lista som kan innehålla referenser till receptobjekt.
                   XXXXXX  2.  Öppna textfilen för läsning.
                   XXXXXX  3.  Läs rad från textfilen tills det är slut på filen.
                    XX  a.  Om det är en tom rad…
                    i.  …fortsätt med att läsa in nästa rad.
                    b.  Om det är en avdelning för nytt recept…
                    i.  …sätt status till att nästa rad som läses in kommer att vara receptets namn.
                    c.  …eller om det är avdelningen för ingredienser…
                    i.  …sätt status till att kommande rader som läses in kommer att vara receptets 
                    ingredienser.
                    d.  …eller om det är avdelningen för instruktioner…
                    i.  …sätt status till att kommande rader som läses in kommer att vara receptets 
                    instruktioner.
                    e.  …annars är det ett namn, en ingrediens eller en instruktion
                    i.  Om status är satt att raden ska tolkas som ett recepts namn…
                    1.  Skapa nytt receptobjekt med receptets namn.
                    ii.  …eller om status är satt att raden ska tolkas som en ingrediens…
                    1.  Dela upp raden i delar genom att använda metoden Split() i klassen 
                    String. De olika delarna separeras åt med semikolon varför det 
                    alltid ska bli tre delar.
                    2.  Om antalet delar inte är tre…
                    Inledande programmering med C# (1DV402)    9 (19) 
                    a.  …är något fel varför ett undantag ska kastas.   
                    3.  Skapa ett ingrediensobjekt och initiera det med de tre delarna för 
                    mängd, mått och namn.
                    4.  Lägg till ingrediensen till receptets lista med ingredienser.
                    iii.   …eller om status är satt att raden ska tolkas som en instruktion…
                    1.  Lägg till raden till receptets lista med instruktioner.
                    iv.  …annars…
                    1.  …är något fel varför ett undantag ska kastas.
                    4.  Sortera listan med recept med avseende på receptens namn.
                    5.  Returnera en referens till listan.
        
        }

        public RecipeRepository(string path)
        {

            Path = _path;
            //  Konstruktorn ska initiera fältet _path, via egenskapen Path, så att det instansierade objektet innehåller 
            //en sökväg.
        }*/

        }
    }
}
// ANVÄNDA LÄNKAR
// http://stackoverflow.com/questions/12314555/how-to-remove-empty-line-when-reading-text-file-using-c-sharp
// http://www.dreamincode.net/forums/topic/38937-working-with-enums-in-c%23/
//http://www.dotnetperls.com/count-array


/*Klassen Recipe beskriver ett recept med ett namn, en lista med ingredienser och en lista med 
instruktioner. Strukturen Ingredient beskriver en ingrediens med mängd, mått och ingrediensens 
namn.
RecipeRepository ansvarar för allt som har med persistent lagring av recept, d.v.s. klassen har 
metoder för att läsa recept från en textfil och skriva recept till en textfil. Klassen använder i samband 
med inläsning av recept lämpligen den uppräkningsbara typen RecipeReadStatus för att hålla 
ordningen på vilken typ av data som lästs in från textfilen.
Då recept ska visas ska en instans av klassen RecipeView användas, som till skillnad mot klassen 
Recipe vet hur ett, eller flera, recept skrivs ut i ett konsolfönster.
Klassen Program har huvudansvaret för exekveringen av applikationen och erbjuder användaren med 
hjälp av en meny ett antal kommandon som kan användas för att hantera recept.
 
 
 */
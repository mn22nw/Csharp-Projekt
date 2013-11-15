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
            List<Recipe> listRecipes = new List<Recipe>(); // skapar en lista för recept
            RecipeReadStatus status = new RecipeReadStatus(); // skapar en ny instans av enum RecipeReadStatus 
            
                try
                {
                    using (StreamReader reader = new StreamReader(Path)) // using öppnar/stänger filen automatiskt när man har använt den. 
                    {
                        string line; // skapar en ny string-variabel
                        Recipe currentRecipe = null;
                   
                        do
                        {
                            line = reader.ReadLine(); // tilldelar variabeln line värdet för en avläst rad
                            if (line == null) {
                            continue;                           
                             
                            }  // för att inte få Error-meddelandet att instance of object = null

                            if (line == String.Empty)  //fortsätter läsa in ifall det finns en tom rad
                            {
                                continue;
                            }

                            if (line == "[Recept]" || line == "[Ingredienser]" || line == "[Instruktioner]")
                            {
                                if (line == "[Recept]")
                                {
                                    status = RecipeReadStatus.New;  // Raden tolkas som ett nytt recept
                                    continue;
                                }

                                if (line == "[Ingredienser]")
                                {
                                    status = RecipeReadStatus.Ingredient; // Raden tolkas som en ingrediens
                                    continue;
                                }

                                if (line == "[Instruktioner]")
                                {
                                    status = RecipeReadStatus.Direction; // Raden tolkas som en instruktion
                                    continue; 
                                }                             
                            }

                            else
                            {
                                switch (status)
                                {
                                    case RecipeReadStatus.New ://  om status är satt till nytt recept 

                                        if (currentRecipe != null) //  för att den inte ska lägga till ett null object
                                        
                                            listRecipes.Add(currentRecipe);
                                            currentRecipe = new Recipe(line); // Skapar nytt receptobjekt med receptets namn                                     
                                            break;
                                                                               
                                    case RecipeReadStatus.Ingredient: // om status satt till ingrediens
              
                                    string[] scores = line.Split(';');
                                    int count = scores.Count(); //Räknar ut hur många delar det är per rad
                                    if (count != 3) // om det är 3st kastas exeption (som inte verkar hanteras??)
                                    {
                                        throw new ArgumentException("Fel antal ingredienser!!");
                                    }
 
                                      Ingredient ingrediensObj = new Ingredient();  //Skapar ingrediensobjekt och initiera det med de tre delarna för mängd, mått och namn.
                                      ingrediensObj.Amount = scores[0];
                                      ingrediensObj.Measure = scores[1];
                                      ingrediensObj.Name = scores[2];
 
                                      currentRecipe.Add(ingrediensObj);//Lägger till ingrediensen till receptets lista med ingredienser. (Anropar Add-metoden som tar en ingrediens!)
                                      break;
                                 
                                    case RecipeReadStatus.Direction: // om status satt till instruktion
                                    currentRecipe.Add(line);  // Lägger till raden till receptets lista med instruktioner.(Anropar Add-metoden som tar en string!)
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
                    throw new ArgumentException("Fel! Ett fel inträffade när recepten lästes in!"); 
                }

                return listRecipes.OrderBy(row => row.Name).ToList(); // sorterar raderna efter namn på Recipe            
        }

        public RecipeRepository(string path) // initierar fältet _path, via egenskapen Path, så att det instansierade objektet innehåller en sökväg.
        {
            Path = path;
        }

        public void Save(List<Recipe> recipes)
        {
            using (StreamWriter writer = new StreamWriter(Path, true))
            {
                foreach (Recipe a in recipes)  // för varje receptObject
                {
                    writer.WriteLine("[Recept]");
                    writer.WriteLine(a.Name);
                    
                    writer.WriteLine("[Ingredienser]");
                    foreach (Ingredient ingr in a.Ingredients)
                    { writer.WriteLine(ingr); }

                    writer.WriteLine("[Instruktioner]");
                    foreach (string descr in a.Directions)
                    { writer.WriteLine(descr); }
                }
            }          
        }
    }
}
// ANVÄNDA LÄNKAR
// http://stackoverflow.com/questions/12314555/how-to-remove-empty-line-when-reading-text-file-using-c-sharp
// http://www.dreamincode.net/forums/topic/38937-working-with-enums-in-c%23/
//http://www.dotnetperls.com/count-array

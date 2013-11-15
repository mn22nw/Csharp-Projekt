using System;
using System.Collections.Generic;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace _1DV402.S3
{
    class Recipe : IComparable, IComparable<Recipe> //Klassen Recipe beskriver ett recept med ett namn, en lista med ingredienser och en lista med instruktioner. 

    {
        private List<string> _direction = new List<string>();   // används för att representera ett recepts instruktioner.
        private List<Ingredient> _ingredients = new List<Ingredient>();   // används för att representera ett recepts ingredienser.
        private string _name; //används för att representera ett recepts namn

       public ReadOnlyCollection<string> Directions //Publik egenskap av typen ReadOnlyCollection<string> som ger en ”read-only”-referens till fältet _directions. 
       {
           get { return new ReadOnlyCollection<string>(_direction); } //returnerar _direction
       }

       public ReadOnlyCollection<Ingredient> Ingredients // Publik egenskap av typen ReadOnlyCollection<Ingredient> som ger en ”read-only”-referens till fältet _ingredients
       {
           get { return new ReadOnlyCollection<Ingredient>(_ingredients); } //returnerar _ingredients    
       }

       public string Name  //Publik egenskap av typen string som ger eller sätter namnet på receptet
       {
           get { return _name; }  

           set {
               if (value == null || value == String.Empty) //kollar att namnet inte refererar till null eller är en tom sträng
               {
                   throw new ArgumentException("Inget namn är angivet!! ");
               }
               _name = value;   
           }
       } 

       public void Add(Ingredient ingredient) //används för att lägga till en ny ingrediens till ett recept
       {
           _ingredients.Add(ingredient); 
       }

       public void Add(string direction) //används för att lägga till en ny instruktion till ett recept
       {
           _direction.Add(direction);
       }
             //----- CompareTo -----//   
     /*  Metoderna CompareTo ska jämföra två objekt med avseende på fältet för receptets namn. 
      •  Refererar parametern till ett objekt vars namn ska sorteras efter det anropande objektets namn ska ett heltal mindre än 0 returneras.
      •  Refererar parametern till ett objekt vars namn ska sorteras före det anropande objektets namn ska ett heltal större än 0 returneras.
      •  Refererar parametern till ett objekt ett objekt vars namn är samma som det anropande objektets namn ska heltalet 0 returneras.*/

       public int CompareTo(object obj) //används t.ex. av metoden Array.Sort() då instanser av typen Recipe ska sorteras
       { 
           
           if (obj == null) // om parametern = null returneras ett heltal större än 0 
           { return 1; 
           }

           Recipe other = obj as Recipe; // om obj (parametern) inte är av typen Recipe så kastas ett undantag
           if (other == null)
           { throw new ArgumentException("Objektet är inte ett recept"); }

           return Name.CompareTo(other.Name); 
       }

       public int CompareTo(Recipe other) //används av metoden List.Sort() då instanser av typen Recipe ska sorteras
       {
           if (other == null) // om parametern = null returneras ett heltal större än 0 
           {
               return 1;
           }
           return 5; //Måste retourrurunera ;)
       }

        // ------ KONSTRUKTORER ------ //
       public Recipe(string name) //:this(name,0,0) ->funkade inte...
       {
           Name= name;
       }

       public Recipe(string name, List<Ingredient> ingredients, List<string> directions)
       {
           Name = name;
           _direction = directions;
           _ingredients = ingredients;         
       }
    }
}

// http://stackoverflow.com/questions/16388740/why-am-i-getting-error-cannot-implicitly-convert-type-system-collections-gener
// http://stackoverflow.com/questions/10415276/how-to-check-if-variable-is-of-type-of-a-type-thats-stored-within-a-variable

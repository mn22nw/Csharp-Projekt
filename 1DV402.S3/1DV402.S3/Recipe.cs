using System;
using System.Collections.Generic;
using System.IO; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace _1DV402.S3
{
    class Recipe :IComparable<Recipe>

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
       } //returnerar namnet


       public void Add(Ingredient ingredient) //används för att lägga till en ny ingrediens till ett recept
       {
          //// List<Ingredient> newIngredient = new List<Ingredient>();
           _ingredients.Add(ingredient); 

       //  //  newIngredient.Add(ingredient); 

       // //   ReadOnlyCollection<Ingredient> newIngredient2 = new ReadOnlyCollection<Ingredient>(newIngredient);
       }

       public void Add(string direction) //används för att lägga till en ny instruktion till ett recept
       {
           //string WHOUH
       }

       public int CompareTo(object obj)
       {
          //Metoden CompareTo(object obj) används t.ex. av metoden Array.Sort() då instanser av typen Recipe ska sorteras
           
           if (obj == null) // om parametern = null returneras ett heltal större än 0 
           { obj = 1; }

           
           if (obj.GetType() != typeof(Recipe)) // om obj (parametern) inte är av typen Recipe så kastas ett undantag
           { 
               throw new ArgumentException("Value is not a number.");}

               return Convert.ToInt32(obj); //Måste retourrurunera ;)
       }

       /*Metoderna CompareTo ska jämföra två objekt med avseende på fältet för receptets namn. 

•  Refererar parametern till ett objekt vars namn ska sorteras efter det anropande objektets namn 
ska ett heltal mindre än 0 returneras.
•  Refererar parametern till ett objekt vars namn ska sorteras före det anropande objektets namn 
ska ett heltal större än 0 returneras.
•  Refererar parametern till ett objekt ett objekt vars namn är samma som det anropande 
objektets namn ska heltalet 0 returneras.*/

     

       public int CompareTo(Recipe other)
       {    //Metoden CompareTo(Recipe other) används av metoden List.Sort() då instanser av typen Recipe ska sorteras
           return 5; //Måste retourrurunera ;)
       }

        // ------ KONSTRUKTORER ------ //
       public Recipe(string name) //:this(name,0,0)
       {
           _name= name;
           
           //namnet på receptet JUUEE   ÅHÅ!  - anropa denna från RecipeRepository juue
       }

       public Recipe(string name, List<Ingredient> ingredients, List<string> directions)
       {
           _name = name;
           _direction = directions;
           _ingredients = ingredients;
          
       }

      // HUUUH?! RUMPLE BAJSAR?!     




                //    StringBuilder[] array = new StringBuilder[1];
                //array[0] = new StringBuilder();
                //var read = new ReadOnlyCollection<StringBuilder>(array);
    }
}

// http://stackoverflow.com/questions/16388740/why-am-i-getting-error-cannot-implicitly-convert-type-system-collections-gener
// http://stackoverflow.com/questions/10415276/how-to-check-if-variable-is-of-type-of-a-type-thats-stored-within-a-variable

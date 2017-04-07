using System;
using System.Collections.Generic;

namespace SmartCafePlus
{
	public enum TypesOfIngredients
	{
		CoffeeBeans, GroundCoffee, Milk, SteamedMilk, Water, Sugar, SugarPowder, Salt, Cream, WhippedCream, CacaoPowder,
		VanillinPowder, NutmegPowder, Cinnamon, Chocolate
	}

	public class Ingredient
	{
		private float quantity = 0;
		public float Quantity
		{
			get {
				return this.quantity;
			}
		}

		private TypesOfIngredients type;
		public TypesOfIngredients Type
		{
			get {
				return this.type;
			}
		}

		public Ingredient(TypesOfIngredients type, float quantity)
		{
			this.quantity = quantity;
			this.type = type;
		}

		public Ingredient Process(TypesOfIngredients output, float percentOfRefuse)
		{
			this.quantity = 0;
			return new Ingredient(output, (percentOfRefuse <= 0 || percentOfRefuse > 100
               ? this.quantity
			   : this.quantity - this.quantity / 100 * percentOfRefuse)
			);
		}

	}

	public enum TypesOfCoffee
	{
		Mocha, Espresso, Mokachino, BlackCoffee, Turkish, French, Americano, Cappuccino, Irish, Jamaican
	}

	interface Beverage 
	{
		
	}

	public class Coffee : Beverage
	{
		private TypesOfCoffee species;
		public TypesOfCoffee Species
		{
			get
			{
				return this.species;
			}
		}

		private float weight = 0;
		public float Weight
		{
			get
			{
				return this.weight;
			}
		}

		private List<Ingredient> ingredients = new List<Ingredient>();

		private float sumOfIngredients = 0;
		public float SumOfIngredients
		{
			get
			{
				return this.sumOfIngredients;
			}
		}

		public Coffee(TypesOfCoffee species, float weight) {
			this.weight = weight;
			this.species = species;
		}

		public Coffee(float weight, List<Ingredient> ingredients) 
		{
			this.weight = weight;
			this.addIngredients(ingredients);
		}

		public Coffee addIngredient(Ingredient ingredient)
		{
			if (ingredient != null)
			{
				this.ingredients.Add(ingredient);
			}
			return this;
		}
		public Coffee addIngredients(List<Ingredient> ingredients)
		{
			if (ingredients !=null && ingredients.Count > 0)
			{
				this.ingredients.AddRange(ingredients.GetRange(0, ingredients.Count));
			}
			return this;
		}
		public Coffee make()
		{
			ingredients.RemoveAll(item => item == null);

			sumOfIngredients = 0;
			ingredients.ForEach(item => {
				sumOfIngredients += item.Quantity;
			});
			return this;
		}
	}

	/*
	 * Species of coffee: Mocha
	 * Instance builder with static method getInstance()
	 */
	public sealed class Mocha : Coffee
	{
		public Mocha() : base(TypesOfCoffee.Mocha, 100) // 100 gr per 1 cup of Mocha Coffee
		{
			addIngredients(new List<Ingredient>() {
				new Ingredient(TypesOfIngredients.CoffeeBeans, this.Weight / 4).Process(TypesOfIngredients.GroundCoffee, 20),
				new Ingredient(TypesOfIngredients.Milk, this.Weight / 3).Process(TypesOfIngredients.SteamedMilk, 5),
				new Ingredient(TypesOfIngredients.Cream, this.Weight / 4).Process(TypesOfIngredients.WhippedCream, 5),
				new Ingredient(TypesOfIngredients.Chocolate, this.Weight / 3),
				new Ingredient(TypesOfIngredients.CacaoPowder, 2),
				new Ingredient(TypesOfIngredients.Sugar, 5),
				new Ingredient(TypesOfIngredients.NutmegPowder, 1),
				new Ingredient(TypesOfIngredients.Cinnamon, 1)
			}).make();
		}

		public static Coffee Make()
		{
			return new Mocha();
		}
	}

	/*
	 * Species of coffee: Mokachino
	 * Instance builder with static method getInstance()
	 */
	public sealed class Mokachino : Coffee
	{
		public Mokachino() : base(TypesOfCoffee.Mokachino, 100) // 100 gr per 1 cup of Mokachino Coffee
		{
			addIngredients(new List<Ingredient>() {
				new Ingredient(TypesOfIngredients.CoffeeBeans, this.Weight / 5).Process(TypesOfIngredients.GroundCoffee, 20),
				new Ingredient(TypesOfIngredients.Milk, this.Weight / 2),
				new Ingredient(TypesOfIngredients.Chocolate, this.Weight / 3),
				new Ingredient(TypesOfIngredients.Cream, this.Weight / 4).Process(TypesOfIngredients.WhippedCream, 5)
			}).make();
		}

		public static Coffee Make()
		{
			return new Mokachino();;
		}
	}

	/*
	 * Species of coffee: Espresso
	 * Instance builder with static method getInstance()
	 */
	public sealed class Espresso : Coffee
	{
		public Espresso() : base(TypesOfCoffee.Espresso, 100) // 100 gr per 1 cup of Espresso Coffee
		{
			addIngredients(new List<Ingredient>() {
				new Ingredient(TypesOfIngredients.CoffeeBeans, this.Weight / 3).Process(TypesOfIngredients.GroundCoffee, 20),
				new Ingredient(TypesOfIngredients.Water, this.Weight),
				new Ingredient(TypesOfIngredients.Sugar, 5),
				new Ingredient(TypesOfIngredients.Salt, 1)
			}).make();
		}

		public static Coffee Make()
		{
			return new Espresso();
		}
	}

	/*
	 * Species of coffee: BlackCoffee
	 * Instance builder with static method MakeBlackCoffee()
	 * Instance builder with static method MakeBlackCoffeeWithSugar()
	 * Instance builder with static method MakeBlackCoffeeWithMilk()
	 * Instance builder with static method MakeBlackCoffeeWithSugarAndMilk()
	 */
	public sealed class BlackCoffee : Coffee
	{
		public BlackCoffee(bool plusSugar, bool plusMilk) : base(TypesOfCoffee.BlackCoffee, 100) // 100 gr per 1 cup of BlackCoffee
		{
			addIngredients(new List<Ingredient>() {
				new Ingredient(TypesOfIngredients.CoffeeBeans, this.Weight / 3).Process(TypesOfIngredients.GroundCoffee, 20),
				new Ingredient(TypesOfIngredients.Water, plusMilk ? this.Weight * 3 / 4 : this.Weight),
				plusMilk ? new Ingredient(TypesOfIngredients.Milk, this.Weight / 4) : null,
				plusSugar ? new Ingredient(TypesOfIngredients.Sugar, 5) : null
			}).make();
		}

		public static Coffee Make()
		{
			return new BlackCoffee(false, false);
		}

		public static Coffee MakeWithSugar()
		{
			return new BlackCoffee(true, false);
		}

		public static Coffee MakeWithMilk()
		{
			return new BlackCoffee(false, true);
		}

		public static Coffee MakeWithSugarAndMilk()
		{
			return new BlackCoffee(true, true);;
		}
	}

	/*
	 * Coffee Machine
	 * Singleton accessible via static method GetInstance()
	 */
	public sealed class CoffeeMachine
	{

		private static CoffeeMachine instance = null; 

		private Dictionary<Coffee, int> drinks = new Dictionary<Coffee, int>();
		
		public CoffeeMachine() {}

		public static CoffeeMachine GetInstance()
		{
			if (instance == null)
			{
				instance = new CoffeeMachine();
			}
			return instance;
		}

		public class Response
		{
			public int quantityOfCups = 0;
			public float totalOfIngredients = 0;
		}

		public Response Calculate()
		{
			Response response = new Response();

			foreach (KeyValuePair<Coffee, int> entry in drinks)
			{
				response.totalOfIngredients += entry.Key.SumOfIngredients * entry.Value;
				response.quantityOfCups += entry.Value;
			}

			return response;
		}

		public Response GetLastOrder()
		{
			Response response = new Response();

			if (drinks.Count > 0)
			{
				int index = 0;
				foreach (KeyValuePair<Coffee, int> entry in drinks)
				{
					if (++index == drinks.Count)
					{
						response.totalOfIngredients = entry.Key.SumOfIngredients * entry.Value;
						response.quantityOfCups = entry.Value;
						break;
					}
				}
			}
			return response;
		}

		public void MakeMocha(int quantity)
		{
			drinks.Add(Mocha.Make(), quantity);
		}
	
		public void MakeMokachino(int quantity)
		{
			drinks.Add(Mokachino.Make(), quantity);
		}
	
		public void MakeEspresso(int quantity)
		{
			drinks.Add(Espresso.Make(), quantity);
		}
	
		public void MakeBlackCoffee(int quantity)
		{
			drinks.Add(BlackCoffee.Make(), quantity);
		}
	
		public void MakeBlackCoffeeWithSugar(int quantity)
		{
			drinks.Add(BlackCoffee.MakeWithSugar(), quantity);
		}

		public void MakeBlackCoffeeWithMilk(int quantity)
		{
			drinks.Add(BlackCoffee.MakeWithMilk(), quantity);
		}
	
		public void MakeBlackCoffeeWithSugarAndMilk(int quantity)
		{
			drinks.Add(BlackCoffee.MakeWithSugarAndMilk(), quantity);
		}
	}

	class MainClass
	{
		public static void Main(string[] args)
		{

			Func<bool> DisplayMenu = () =>
			{
				Console.WriteLine("Coffee Machine");
				Console.WriteLine("Please select one item from menu:");
				Console.WriteLine("1A1\t<= Black Coffee (10oz)");
				Console.WriteLine("1A1+\t <= Black Coffee with sugar (10oz)");
				Console.WriteLine("1A1#\t <= Black Coffee with milk (10oz)");
				Console.WriteLine("1A1*\t <= Black Coffee with milk and sugar (10oz)");
				Console.WriteLine("1B0\t <= Espresso (10oz)");
				Console.WriteLine("1C0\t <= Mocha (10oz)");
				Console.WriteLine("1D0\t <= Mokachino (10oz)");
				Console.WriteLine("=\t <= Calculate total ordered");
				Console.WriteLine("*\t <= Clean console");
				Console.WriteLine("ESC+ENTER\t => Exit");
				return true;
			};

			bool flagReset = false;

			CoffeeMachine coffeeMachine = CoffeeMachine.GetInstance();

			DisplayMenu();

			flagReset = false;

			do
			{
				bool ex = false;

				Console.Write("=>");
				String yourChoice = Console.ReadLine();

				switch (yourChoice.ToUpper())
				{
					case "1A1":
						do {
							Console.Write("\nYour choice is {0}.", TypesOfCoffee.BlackCoffee.ToString());
							Console.Write("\nEnter the quantity: ");
							string line = Console.ReadLine();
							int qty;
							if (int.TryParse(line, out qty) && qty > 0 && qty < 100)
							{
								coffeeMachine.MakeBlackCoffee(qty);
								ex = true;
							}
							else
							{
								Console.WriteLine("Not a number, please try again...");
							}
						}
						while (!ex);
						Console.WriteLine("\nThank you!! Coffee machine made {0} cups of coffee for you.", coffeeMachine.GetLastOrder().quantityOfCups);
						break;
					case "1A1+":
						do
						{
							Console.Write("\nYour choice is {0} with sugar.", TypesOfCoffee.BlackCoffee.ToString());
							Console.Write("\nEnter the quantity: ");
							string line = Console.ReadLine();
							int qty;
							if (int.TryParse(line, out qty) && qty > 0 && qty < 100)
							{
								coffeeMachine.MakeBlackCoffeeWithSugar(qty);
								ex = true;
							}
							else
							{
								Console.WriteLine("Not a number, please try again...");
							}
						}
						while (!ex);
						Console.WriteLine("\nThank you!! Coffee machine made {0} cups of coffee for you.", coffeeMachine.GetLastOrder().quantityOfCups);
						break;
					case "1A1#":
						do
						{
							Console.Write("\nYour choice is {0} with milk.", TypesOfCoffee.BlackCoffee.ToString());
							Console.Write("\nEnter the quantity: ");
							string line = Console.ReadLine();
							int qty;
							if (int.TryParse(line, out qty) && qty > 0 && qty < 100)
							{
								coffeeMachine.MakeBlackCoffeeWithMilk(qty);
								ex = true;
							}
							else
							{
								Console.WriteLine("Not a number, please try again...");
							}
						}
						while (!ex);
						Console.WriteLine("\nThank you!! Coffee machine made {0} cups of coffee for you.", coffeeMachine.GetLastOrder().quantityOfCups);
						break;
					case "1A1*":
						do
						{
							Console.Write("\nYour choice is {0} with sugar and milk.", TypesOfCoffee.BlackCoffee.ToString());
							Console.Write("\nEnter the quantity: ");
							string line = Console.ReadLine();
							int qty;
							if (int.TryParse(line, out qty) && qty > 0 && qty < 100)
							{
								coffeeMachine.MakeBlackCoffeeWithSugarAndMilk(qty);
								ex = true;
							}
							else
							{
								Console.WriteLine("Not a number, please try again...");
							}
						}
						while (!ex);
						Console.WriteLine("\nThank you!! Coffee machine made {0} cups of coffee for you.", coffeeMachine.GetLastOrder().quantityOfCups);
						break;
					case "1B0":
						do
						{
							Console.Write("\nYour choice is {0}.", TypesOfCoffee.Espresso.ToString());
							Console.Write("\nEnter the quantity: ");
							string line = Console.ReadLine();
							int qty;
							if (int.TryParse(line, out qty) && qty > 0 && qty < 100)
							{
								coffeeMachine.MakeEspresso(qty);
								ex = true;
							}
							else
							{
								Console.WriteLine("Not a number, please try again...");
							}
						}
						while (!ex);
						Console.WriteLine("\nThank you!! Coffee machine made {0} cups of coffee for you.", coffeeMachine.GetLastOrder().quantityOfCups);
						break;
					case "1C0":
						do
						{
							Console.Write("\nYour choice is {0}.", TypesOfCoffee.Mocha.ToString());
							Console.Write("\nEnter the quantity: ");
							string line = Console.ReadLine();
							int qty;
							if (int.TryParse(line, out qty) && qty > 0 && qty < 100)
							{
								coffeeMachine.MakeMocha(qty);
								ex = true;
							}
							else
							{
								Console.WriteLine("Not a number, please try again...");
							}
						}
						while (!ex);
						Console.WriteLine("\nThank you!! Coffee machine made {0} cups of coffee for you.", coffeeMachine.GetLastOrder().quantityOfCups);
						break;
					case "1D0":
						do
						{
							Console.Write("\nYour choice is {0}.", TypesOfCoffee.Mokachino.ToString());
							Console.Write("\nEnter the quantity: ");
							string line = Console.ReadLine();
							int qty;
							if (int.TryParse(line, out qty) && qty > 0 && qty < 100)
							{
								coffeeMachine.MakeMokachino(qty);
								ex = true;
							}
							else
							{
								Console.WriteLine("Not a number, please try again...");
							}
						}
						while (!ex);
						Console.WriteLine("\nThank you!! Coffee machine made {0} cups of coffee for you.", coffeeMachine.GetLastOrder().quantityOfCups);
						break;
					case "=":
						Console.WriteLine("Total ordered:\t{0:N}", coffeeMachine.Calculate().quantityOfCups);
						Console.WriteLine("Total ingredients used:\t{0:F}", coffeeMachine.Calculate().totalOfIngredients);
						break;
					case "*":
						Console.Clear();
						DisplayMenu();
						break;
					case "\u001B":
						flagReset = true;
						Console.Clear();
						Console.WriteLine("Thank you to use our service. Bye!");
						break;
					default:
						Console.WriteLine("\nPlease try again...");
						break;
				}

			} while (!flagReset);

		}
	}
}

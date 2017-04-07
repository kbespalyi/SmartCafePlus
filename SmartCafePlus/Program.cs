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

	public enum CoffeeTypes
	{
		Mocha, Espresso, Mokachino, BlackCoffee, Turkish, French, Americano, Cappuccino, Irish, Jamaican
	}

	interface Beverage 
	{
		
	}

	public class Coffee : Beverage
	{
		private CoffeeTypes species;
		public CoffeeTypes Species
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

		public Coffee(CoffeeTypes species, float weight) {
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
		public Mocha() : base(CoffeeTypes.Mocha, 100) // 100 gr per 1 cup of Mocha Coffee
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
		public Mokachino() : base(CoffeeTypes.Mokachino, 100) // 100 gr per 1 cup of Mokachino Coffee
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
		public Espresso() : base(CoffeeTypes.Espresso, 50) // 50 gr per 1 cup of Espresso Coffee
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
		public BlackCoffee(bool plusSugar, bool plusMilk) : base(CoffeeTypes.BlackCoffee, 100) // 100 gr per 1 cup of BlackCoffee
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
		private Dictionary<CoffeeTypes, Response> report = new Dictionary<CoffeeTypes, Response>();

		private CoffeeMachine() {}

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
			private int quantityOfCups = 0;
			public int QuantityOfCups
			{
				get
				{
					return quantityOfCups;
				}
				set
				{
					quantityOfCups = value;
				}
			}
			private float totalOfIngredients = 0;
			public float TotalOfIngredients
			{
				get
				{
					return totalOfIngredients;
				}
				set
				{
					totalOfIngredients = value;
				}
			}

			public Response()
			{
				this.quantityOfCups = 0;
				this.totalOfIngredients = 0;
			}

			public Response(int quantityOfCups, float totalOfIngredients) {
				this.quantityOfCups = quantityOfCups;
				this.totalOfIngredients = totalOfIngredients;
			}
		}

		public Response CalculateTotals()
		{
			Response response = new Response();

			foreach (KeyValuePair<Coffee, int> entry in drinks)
			{
				response.TotalOfIngredients += entry.Key.SumOfIngredients * entry.Value;
				response.QuantityOfCups += entry.Value;
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
						response.TotalOfIngredients = entry.Key.SumOfIngredients * entry.Value;
						response.QuantityOfCups = entry.Value;
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

	delegate void MakeCoffeeFunc(int qty);
	delegate bool DoChoiceFunc(CoffeeTypes coffeeTypes, MakeCoffeeFunc makeCoffeeImpl);

	class MainClass
	{
		public static void Main(string[] args)
		{

			CoffeeMachine coffeeMachine = CoffeeMachine.GetInstance();

			Func<bool> DisplayMenu = () =>
			{
				Console.WriteLine("Coffee Machine");
				Console.WriteLine("Please select one item from menu using code:");
				Console.WriteLine("1A\t <= Black Coffee (8oz)");
				Console.WriteLine("1A+\t <= Black Coffee with sugar (8oz)");
				Console.WriteLine("1A#\t <= Black Coffee with milk (8oz)");
				Console.WriteLine("1A*\t <= Black Coffee with milk and sugar (8oz)");
				Console.WriteLine("1B\t <= Espresso (5oz)");
				Console.WriteLine("1C\t <= Mocha (8oz)");
				Console.WriteLine("1D\t <= Mokachino (8oz)");
				Console.WriteLine("=\t <= Calculate the total ordered");
				Console.WriteLine("*\t <= Clean console");
				Console.WriteLine("ESC+ENTER\t => Exit");
				return true;
			};

			DoChoiceFunc DoChoiceImpl = (coffeeTypes, makeCoffeeImpl) =>
			{
				int qty = 0;
				bool ex = false;

				do
				{
					Console.Write("\nYour choice is {0}.", coffeeTypes.ToString());
					Console.Write("\nEnter the quantity: ");
					string line = Console.ReadLine();
					if (int.TryParse(line, out qty) && qty > 0 && qty < 100)
					{
						makeCoffeeImpl(qty);
						ex = true;
					}
					else
					{
						Console.WriteLine("Not a number, please try again...");
					}
				}
				while (!ex);
				Console.WriteLine("\nThank you!! Coffee machine made {0} cups of coffee for you.", coffeeMachine.GetLastOrder().QuantityOfCups);
				Console.WriteLine("\nReady to have your next command.\n");
				return true;
			};

			DisplayMenu();

			bool flagReset = false;

			do
			{
				Console.Write("=> ");
				String yourChoice = Console.ReadLine();

				switch (yourChoice.ToUpper())
				{
					case "1A":
						DoChoiceImpl(CoffeeTypes.BlackCoffee, qty => coffeeMachine.MakeBlackCoffee(qty));
						break;
					case "1A+":
						DoChoiceImpl(CoffeeTypes.BlackCoffee, qty => coffeeMachine.MakeBlackCoffeeWithSugar(qty));
						break;
					case "1A#":
						DoChoiceImpl(CoffeeTypes.BlackCoffee, qty => coffeeMachine.MakeBlackCoffeeWithMilk(qty));
						break;
					case "1A*":
						DoChoiceImpl(CoffeeTypes.BlackCoffee, qty => coffeeMachine.MakeBlackCoffeeWithSugarAndMilk(qty));
						break;
					case "1B":
						DoChoiceImpl(CoffeeTypes.Espresso, qty => coffeeMachine.MakeEspresso(qty));
						break;
					case "1C":
						DoChoiceImpl(CoffeeTypes.Mocha, qty => coffeeMachine.MakeMocha(qty));
						break;
					case "1D":
						DoChoiceImpl(CoffeeTypes.Mokachino, qty => coffeeMachine.MakeMokachino(qty));
						break;
					case "=":
						CoffeeMachine.Response response = coffeeMachine.CalculateTotals();
						Console.WriteLine("Total ordered:\t{0:N} (units)", response.QuantityOfCups);
						Console.WriteLine("Total ingredients used:\t{0:F} (grs)", response.TotalOfIngredients);
						Console.WriteLine("\nReady to have your next command.\n");
						break;
					case "*":
						Console.Clear();
						DisplayMenu();
						break;
					case "EXIT":
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

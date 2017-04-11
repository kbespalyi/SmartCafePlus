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
			set {
				if (value >= 0) {
					this.quantity = value;
				} else {
					this.quantity = 0;
				}
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
			float qty = this.quantity;
			this.quantity = 0;
			return new Ingredient(output, (percentOfRefuse <= 0 || percentOfRefuse > 100
               			? qty
			   	: qty - qty / 100 * percentOfRefuse)
			);
		}

		public Ingredient AddQuantity(float quantity) {
			if (quantity > 0)
			{
				this.quantity += quantity;
			}
			return this;
		}

		public Ingredient SubstrucQuantity(float quantity)
		{
			if (this.quantity - quantity >= 0) {
				this.quantity -= quantity;
			} else {
				this.quantity = 0;
			}
			return this;
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
		public List<Ingredient> ListOfIngredients
		{
			get
			{
				return this.ingredients.GetRange(0, this.ingredients.Count);
			}
		}


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
		public Coffee make(int quantity)
		{
			ingredients.RemoveAll(item => item == null);

			sumOfIngredients = 0;
			ingredients.ForEach(item => {
				sumOfIngredients += item.Quantity * quantity;
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
		public Mocha(int quantity) : base(CoffeeTypes.Mocha, 100) // 100 gr per 1 cup of Mocha Coffee
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
			}).make(quantity);
		}

		public static Coffee Make(int quantity)
		{
			return new Mocha(quantity);
		}
	}

	/*
	 * Species of coffee: Mokachino
	 * Instance builder with static method getInstance()
	 */
	public sealed class Mokachino : Coffee
	{
		public Mokachino(int quantity) : base(CoffeeTypes.Mokachino, 100) // 100 gr per 1 cup of Mokachino Coffee
		{
			addIngredients(new List<Ingredient>() {
				new Ingredient(TypesOfIngredients.CoffeeBeans, this.Weight / 5).Process(TypesOfIngredients.GroundCoffee, 20),
				new Ingredient(TypesOfIngredients.Milk, this.Weight / 2),
				new Ingredient(TypesOfIngredients.Chocolate, this.Weight / 3),
				new Ingredient(TypesOfIngredients.Cream, this.Weight / 4).Process(TypesOfIngredients.WhippedCream, 5)
			}).make(quantity);
		}

		public static Coffee Make(int quantity)
		{
			return new Mokachino(quantity);
		}
	}

	/*
	 * Species of coffee: Espresso
	 * Instance builder with static method getInstance()
	 */
	public sealed class Espresso : Coffee
	{
		public Espresso(int quantity) : base(CoffeeTypes.Espresso, 50) // 50 gr per 1 cup of Espresso Coffee
		{
			addIngredients(new List<Ingredient>() {
				new Ingredient(TypesOfIngredients.CoffeeBeans, this.Weight / 3).Process(TypesOfIngredients.GroundCoffee, 20),
				new Ingredient(TypesOfIngredients.Water, this.Weight),
				new Ingredient(TypesOfIngredients.Sugar, 5),
				new Ingredient(TypesOfIngredients.Salt, 1)
			}).make(quantity);
		}

		public static Coffee Make(int quantity)
		{
			return new Espresso(quantity);
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
		public BlackCoffee(int quantity, bool plusSugar, bool plusMilk) : base(CoffeeTypes.BlackCoffee, 100) // 100 gr per 1 cup of BlackCoffee
		{
			addIngredients(new List<Ingredient>() {
				new Ingredient(TypesOfIngredients.CoffeeBeans, this.Weight / 3).Process(TypesOfIngredients.GroundCoffee, 20),
				new Ingredient(TypesOfIngredients.Water, plusMilk ? this.Weight * 3 / 4 : this.Weight),
				plusMilk ? new Ingredient(TypesOfIngredients.Milk, this.Weight / 4) : null,
				plusSugar ? new Ingredient(TypesOfIngredients.Sugar, 5) : null
			}).make(quantity);
		}

		public static Coffee Make(int quantity)
		{
			return new BlackCoffee(quantity, false, false);
		}

		public static Coffee MakeWithSugar(int quantity)
		{
			return new BlackCoffee(quantity, true, false);
		}

		public static Coffee MakeWithMilk(int quantity)
		{
			return new BlackCoffee(quantity, false, true);
		}

		public static Coffee MakeWithSugarAndMilk(int quantity)
		{
			return new BlackCoffee(quantity, true, true);
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

			private readonly List<Ingredient> ingredients = new List<Ingredient>();
			public List<Ingredient> Ingredients
			{
				get
				{
					return ingredients.GetRange(0, ingredients.Count);
				}
				set
				{
					ingredients.Clear();
					ingredients.AddRange(value.GetRange(0, value.Count));
				}
			}

			public Response()
			{
				this.quantityOfCups = 0;
				this.totalOfIngredients = 0;
			}

			public Response(int quantityOfCups, float totalOfIngredients)
			{
				this.quantityOfCups = quantityOfCups;
				this.totalOfIngredients = totalOfIngredients;
			}

			public Response(int quantityOfCups, float totalOfIngredients, List<Ingredient> ingredients)
			{
				this.quantityOfCups = quantityOfCups;
				this.totalOfIngredients = totalOfIngredients;
				this.ingredients.AddRange(ingredients.GetRange(0, ingredients.Count));
			}

			public Response MergeIngredients(List<Ingredient> ingredients)
			{
				foreach (Ingredient entry in ingredients)
				{
					float qty = entry.Quantity;
					Ingredient obj = this.ingredients.Find(item =>
					{
						return item.Type.Equals(entry.Type);
					});

		                     	if (obj == null) {
						// Add new
						this.ingredients.Add(new Ingredient(entry.Type, qty));
					} else {
						// Sum to Quantity
						obj.AddQuantity(qty);
					}
				}
				return this;
			}
		}

		public Response CalculateTotals()
		{
			Response response = new Response();

			foreach (KeyValuePair<Coffee, int> entry in drinks)
			{
				response.TotalOfIngredients += entry.Key.SumOfIngredients;
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
						response.TotalOfIngredients = entry.Key.SumOfIngredients; // * entry.Value;
						response.QuantityOfCups = entry.Value;
						break;
					}
				}
			}
			return response;
		}

		public Dictionary<CoffeeTypes, Response> GetCoffeeReport()
		{
			Dictionary<CoffeeTypes, Response> result = new Dictionary<CoffeeTypes, Response>();

			foreach (KeyValuePair<Coffee, int> entry in drinks)
			{
				Coffee item = entry.Key;
				if (item == null)
				{
					continue;
				}

				List<Ingredient> ingredients = item.ListOfIngredients.GetRange(0, item.ListOfIngredients.Count);

				Response response = new Response(entry.Value, item.SumOfIngredients, ingredients);

				if (result.ContainsKey(item.Species))
				{
					// Add to exist record

					response = null;

					if (result.TryGetValue(item.Species, out response) && response != null)
					{
						response.MergeIngredients(ingredients);
						response.QuantityOfCups += entry.Value;
						response.TotalOfIngredients += item.SumOfIngredients;
					}
				}
				else
				{
					// Otherwise add as new record
					result.Add(item.Species, response);
				}
			}

			return result;
		}

		public Response GetTotalIndegrientsReport() {
			Response result = new Response();
			Dictionary<CoffeeTypes, Response> report = GetCoffeeReport();

			foreach (KeyValuePair<CoffeeTypes, CoffeeMachine.Response> entry in report)
			{
				Response response = entry.Value;
				if (response == null)
				{
					continue;
				}

				result.TotalOfIngredients += response.TotalOfIngredients;
				result.QuantityOfCups += response.QuantityOfCups;
				result.MergeIngredients(response.Ingredients.GetRange(0, response.Ingredients.Count));

			}
			return result;
		}

		public void Restart()
		{
			Console.WriteLine("Restarting...");
			drinks.Clear();
			System.Threading.Thread.Sleep(5000);
		}

		public void MakeMocha(int quantity)
		{
			drinks.Add(Mocha.Make(quantity), quantity);
		}
	
		public void MakeMokachino(int quantity)
		{
			drinks.Add(Mokachino.Make(quantity), quantity);
		}
	
		public void MakeEspresso(int quantity)
		{
			drinks.Add(Espresso.Make(quantity), quantity);
		}
	
		public void MakeBlackCoffee(int quantity)
		{
			drinks.Add(BlackCoffee.Make(quantity), quantity);
		}
	
		public void MakeBlackCoffeeWithSugar(int quantity)
		{
			drinks.Add(BlackCoffee.MakeWithSugar(quantity), quantity);
		}

		public void MakeBlackCoffeeWithMilk(int quantity)
		{
			drinks.Add(BlackCoffee.MakeWithMilk(quantity), quantity);
		}
	
		public void MakeBlackCoffeeWithSugarAndMilk(int quantity)
		{
			drinks.Add(BlackCoffee.MakeWithSugarAndMilk(quantity), quantity);
		}
	}

	delegate void MakeCoffeeFunc(int qty);
	delegate bool DoChoiceFunc(CoffeeTypes coffeeTypes, MakeCoffeeFunc MakeCoffeeImpl);

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
				Console.WriteLine("Rep1\t <= Display the details report");
				Console.WriteLine("Rep2\t <= Display the total ingredients report");
				Console.WriteLine("*\t <= Clean console");
				Console.WriteLine("Restart\t <= Restart machine");
				Console.WriteLine("ESC+ENTER\t => Exit");
				return true;
			};

			DoChoiceFunc DoChoiceImpl = (coffeeTypes, MakeCoffeeImpl) =>
			{
				int qty = 0;
				bool ex = false;

				do
				{
					Console.Write("\nYour choice is {0}.", coffeeTypes.ToString());
					Console.Write("\nEnter the quantity: ");
					string line = Console.ReadLine();
					if (int.TryParse(line, out qty) && qty >= 0 && qty < 100)
					{
						if (qty > 0) {
							MakeCoffeeImpl(qty);
						}
						ex = true;
					}
					else
					{
						Console.WriteLine("Not a number, please try again...");
					}
				}
				while (!ex);
				if (qty > 0) {
					Console.WriteLine("\nThank you!! Coffee machine made {0} cups of coffee for you.", coffeeMachine.GetLastOrder().QuantityOfCups);
				} else {
					Console.WriteLine("\nOrder is cancelled.");
				}
				Console.WriteLine("\nReady to have your next command.\n");
				return true;
			};

			DisplayMenu();

			CoffeeMachine.Response response;

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
						response = coffeeMachine.CalculateTotals();
						Console.WriteLine("Total ordered:\t{0:N} (units)", response.QuantityOfCups);
						Console.WriteLine("Total ingredients used:\t{0:F} (grs)", response.TotalOfIngredients);
						Console.WriteLine("\nReady to have your next command.\n");
						break;
					case "REP1":

						int quantityOfCups = 0;
						float totalOfIngredients = 0;

						Dictionary<CoffeeTypes, CoffeeMachine.Response> report = coffeeMachine.GetCoffeeReport();
						foreach (KeyValuePair<CoffeeTypes, CoffeeMachine.Response> entry in report)
						{
							Console.WriteLine(new String('~', 50));
							Console.WriteLine("{0}:", entry.Key.ToString());
							Console.WriteLine(new String('~', 50));
							response = entry.Value;
							if (response != null)
							{
								Console.WriteLine("Total ordered:\t{0:N} (units)", response.QuantityOfCups);
								Console.WriteLine("Total ingredients used:\t{0:F} (grs)", response.TotalOfIngredients);
								foreach (Ingredient item in response.Ingredients)
								{
									Console.WriteLine("\t{0}\t" + (item.Type.ToString().Length < 6 ? "\t" : "") + "{1:F} (grs)", item.Type.ToString(), item.Quantity);
								}
								quantityOfCups += response.QuantityOfCups;
								totalOfIngredients += response.TotalOfIngredients;
							}

						}
						Console.WriteLine(new String('~', 50));
						Console.WriteLine("Grand total ordered:\t{0:N} (units)", quantityOfCups);
						Console.WriteLine("Grand total ingredients used:\t{0:F} (grs)", totalOfIngredients);
						Console.WriteLine(new String('~', 50));

						Console.WriteLine("\nReady to have your next command.\n");
						break;
					case "REP2":

						response = coffeeMachine.GetTotalIndegrientsReport();

						Console.WriteLine(new String('~', 50));
						Console.WriteLine("Total ingredients used:\t{0:F} (grs)", response.TotalOfIngredients);
						Console.WriteLine(new String('~', 50));

						foreach (Ingredient item in response.Ingredients)
						{
							Console.WriteLine("\t{0}\t" + (item.Type.ToString().Length < 6 ? "\t" : "") + "{1:F} (grs)", item.Type.ToString(), item.Quantity);
						}
						Console.WriteLine(new String('~', 50));

						Console.WriteLine("\nReady to have your next command.\n");
						break;
					case "REP3":
						Console.WriteLine(new String('~', 50));

						Console.WriteLine("\nReady to have your next command.\n");
						break;
					case "*":
						Console.Clear();
						DisplayMenu();
						break;
					case "RESTART":
						coffeeMachine.Restart();
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

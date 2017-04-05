using System;

namespace SmartCafePlus
{
	interface Drink
	{

	}

	enum TypesOfIngredients
	{
		CoffeeBeans, Milk, Water, Sugar, Solt, Cream
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
				quantity = value;
			}
		}

		private TypesOfIngredients type;
		public TypesOfIngredients Type
		{
			get
			{
				return this.type;
			}
			set
			{
				type = value;
			}
		}

		public Ingredient(TypesOfIngredients type, float quantity)
		{
			this.quantity = quantity;
			this.type = type;
		}

	}


	public class CoffeeDrinks
	{
		public CoffeeDrinks()
		{
		}
	}

	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
		}
	}
}

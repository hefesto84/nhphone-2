using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Product{

	public int id {get; set;}
	public string name { get; set; }
	public string description { get; set; }
	public int quantity { get; set; }
	public float price { get; set; }
	public int categoryId { get; set; }
	public string category { get; set; }
	public string image { get; set;}
	public bool light { get; set; }
	public bool vegetarian { get; set; }
	public bool glutenfree { get; set; }
	public bool customizable { get; set; }
	public bool available24h { get; set; }
	public bool special {get; set; }
	public int customId { get; set; }
	public string ingredientType { get; set; }
	public int[] arrayIngredients {get; set;}
	public List<Product> ingredients { get; set; }
	public List<Question> questions { get; set; }

	public Product(string name = "", string description = "", int quantity = 0, float price = 0, int categoryId = 0, string category = "", string image = "", bool glutenfree = false, bool vegetarian = false, bool light = false)
	{
		this.name = name;
		this.description = description;
		this.quantity = quantity;
		this.price = price;
		this.categoryId = categoryId;
		this.category = category;
		this.image = image;
		this.customizable = false;
		this.available24h = false;
		this.ingredientType = "";
		this.special = false;
		this.ingredients = new List<Product> ();
		this.questions = new List<Question> ();
	}

	public void Fill(JSONNode data){

	}

	public int GetCustomId(){
		int cid = 0;
		foreach (Product p in ingredients) {
			cid += p.id;
		}
		return cid;
	}

	public int[] GetIngredientsById(){
		int[] ing = new int[ingredients.Count];
		int i = 0;
		foreach (Product p in ingredients) {
			ing [i] = p.id;
			i++;
		}
		return ing;
	}

	public string GetIngredients(){
		string ing = "";
		foreach (Product p in ingredients) {
			ing += p.name + ", ";
		}
		return ing;
	}
}
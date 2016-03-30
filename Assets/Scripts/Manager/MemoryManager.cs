using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MemoryManager : MonoBehaviour
{

	private Sprite[] productInformationSprites;
	private Sprite[] productSpecialSprites;
	private List<Product> cart;
	private FileStream fcart;
	private List<Product> temporalCart;

	void Awake(){
		productInformationSprites = Resources.LoadAll<Sprite> ("Icons/RoomService/spritesheet_alimentos");
		productSpecialSprites = Resources.LoadAll<Sprite> ("Icons/RoomService/spritesheet_alimentos2");
	}

	void Start ()
	{
	
	}

	void Update ()
	{
	
	}

	public Sprite GetProductInformationSprite(int index){
		return productInformationSprites[index];
	}

	public Sprite GetProductSpecialSprite(int index){
		return productSpecialSprites [index];
	}

	public void InitCart(){
		cart = new List<Product> ();
		temporalCart = new List<Product> ();
		//FileStream fcart = File.Open("cart.txt",FileMode.OpenOrCreate
	}

	public void AddProduct(Product p){
		Debug.Log ("Added to MemoryManager: " + p.name + " - " + p.ingredients.Count);
		Product entry = new Product ();
		entry = p;
		cart.Add (entry);
		temporalCart.Add (entry);
		CartToString ();
	}

	public void CartToString(){
		foreach (Product entry in temporalCart) {
			Debug.Log ("name: " + entry.name + " ingredients: " + entry.ingredients.Count);
		}
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class IOManager : MonoBehaviour {

	private List<Product> cart;

	void Start () {
		
	}

	void Update () {
	
	}

	public bool PendingCart(){
		return false;
		//return File.Exists (UnityEngine.Application.persistentDataPath + "/cart.txt");
	}

	public void CreateCart(){
		cart = new List<Product> ();

		//cart = new List<Product> ();
		//FileStream fs = File.Create (UnityEngine.Application.persistentDataPath + "/cart.txt");
		//fs.Close ();
	}

	public void SaveRandomProduct(){
		Product p = new Product ();
		p.name = "randomPRoduct";
		SaveProductToCart (p);
		//Product p = new Product ();
		//p.name = "randomProduct";
		//SaveProductToCart (p);
	}


	public void SaveProductToCart(Product product){
		/*
		FileStream fs = File.OpenWrite (UnityEngine.Application.persistentDataPath + "/cart.txt");
		BinaryFormatter bf = new BinaryFormatter ();
		bf.Serialize (fs, product);
		fs.Close ();
		*/
		//FileStream fs = File.Open(UnityEngine.Application.persistentDataPath + "/cart.txt",FileMode.OpenOrCreate);
		//BinaryFormatter bf = new BinaryFormatter ();
		//cart = (List<Product>)bf.Deserialize (fs);
		//fs.Close ();
		//Debug.Log ("Cart elements: " + cart.Count);
	}

	public void LoadProductsFromCart(){
		//FileStream fs = File.Open(UnityEngine.Application.persistentDataPath + "/cart.txt",FileMode.Open);
		//BinaryFormatter bf = new BinaryFormatter ();
		//cart = (List<Product>)bf.Deserialize (fs);
		//fs.Close ();
		//Debug.Log ("Cart elements: " + cart.Count);
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UICartController : MonoBehaviour {

	private Dictionary<int,List<Product>> m_products = new Dictionary<int, List<Product>> ();

	public int people { get; set; }
	public int lactose { get; set; }
	public int hour { get; set; }
	public int minute { get; set; }
	public string comments { get; set; }
	public bool breakfast {get;set;}

	private List<Product> products = new List<Product> ();

	void Start () {
		InitCart ();
	}

	void Update () {
	
	}
		
	public void InitCart(){
		m_products = new Dictionary<int, List<Product>> ();
	}

	public void AddProduct(Product product){
		//Debug.Log ("PRoduct quantity : " + product.quantity);
		//Debug.Log ("<color=cyan>Introducing product: " + product.GetIngredients ()+"<color>");
		if (m_products.ContainsKey (product.id)) {
			//Debug.Log ("Product is YET in the cart so we add a new product to the queue");
			if (!product.customizable) {
				//Debug.Log ("Product is not customizable, so we update the quantity");
				m_products [product.id] [0].quantity = product.quantity;
			} else {
				//Debug.Log ("Product is customiable, so we add a new product in the queue");
				m_products [product.id].Add (product);
				//Debug.Log ("PRODUCT INSERTED: " + product.GetIngredients ());
				//if (m_products [product.id].Count > 1) {
				//	Debug.Log ("name: " + m_products [product.id] [0].name + "|" + m_products [product.id] [0].GetIngredients ());
				//	Debug.Log ("name: " + m_products [product.id] [1].name + "|" + m_products [product.id] [1].GetIngredients ());
				//}
			}
		} else {
			//Debug.Log ("Product is NOT in the cart. We create the queue");
			m_products.Add (product.id, new List<Product>());
			m_products [product.id].Add (product);

		}
		//Debug.Log ("Cart updated: " + m_products [product.id].Count);
		toString ();

		Product prod = new Product ();

		prod.name = product.name;
		List<Product> ingredients = product.ingredients;
		prod.ingredients = ingredients;
		prod.name = product.name;
		prod.description = prod.GetIngredients();

		products.Add (prod);

		//foreach (Product pr in products) {
		//	Debug.Log ("name: " + pr.name + " description: " + pr.description);
		//}
	}

	public void UpdateProduct(Product product){

	}

	public void DeleteProduct(Product product){
		if (m_products.ContainsKey (product.id)) {
			if (!product.customizable) {
				m_products [product.id].RemoveAt (m_products [product.id].Count - 1);
			} else {
				
				int customId = product.GetCustomId ();
				for (int i = 0; i < m_products [product.id].Count; i++) {
					if (m_products [product.id] [i].GetCustomId () == customId) {
						m_products [product.id].RemoveAt (i);
					}
				}
			}
		}
	}

	public Dictionary<int,List<Product>> ListProducts(){
		return m_products;
	}

	public string toJson(){
		string response = "";
		return response;
	}

	public string toString(){
		string str = "";
		foreach(KeyValuePair<int,List<Product>> entry in m_products){
			int key = (int)entry.Key;
			List<Product> products = entry.Value;
			if (!products [0].customizable) {
				Debug.Log ("Key: " + key + " Quantity: " + products [0].quantity);
			} else {
				
			}

			foreach (Product e in products) {
				Debug.Log ("Key: " + key + " Name: " + e.name);
				Debug.Log ("Key: " + key + " Ingredients: " + e.GetIngredients());
			}
		}
		return str;
	}
}

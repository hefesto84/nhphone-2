using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cart{

	public int id;

	private List<Product> m_products;

	public int gluten_intolerants { get; set; }
	public int lactose_intolerants{ get; set; }
	public float total { get; set; }
	public int people { get; set; }
	public string comments { get; set; }
	public string hour { get; set; }
	public string min {get; set; }
	public bool breakfast { get; set; }

	public Cart(){
		m_products = new List<Product> ();
		breakfast = false;
	}

	public void Add(Product p){
		m_products.Add (p);
		//Debug.Log ("Añadido producto: " + p.name + "|" + p.quantity);

		/*
		bool found = false;

		for (int i = 0; i < m_products.Count; i++) {
			if (p.id == m_products [i].id) { // El producte ja esta dins

				int val1 = 0;
				foreach (Product ing in p.ingredients) {
					val1 += ing.id;
				}
				int val2 = 0;
				foreach (Product ing in m_products[i].ingredients) {
					val2 += ing.id;
				}

				if (val1 == val2) { // Els productes son exactament iguals
					m_products [i].quantity += 1;
					found = true;
				}

				m_products [i].quantity = p.quantity;
				found = true;

			}
		}

		if (!found) {
			m_products.Add (p);
		}
		*/
	}

	public void Del(Product p){
		//m_products.Remove (p);
		//total = total - p.price;
	}

	public List<Product> List(){
		return m_products;
	}

	public void Clear(){
		m_products = new List<Product> ();
		gluten_intolerants = 0;
		lactose_intolerants = 0;
		comments = "";
		total = 0f;
		people = 0;
	}
}

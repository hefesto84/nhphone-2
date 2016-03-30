using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIIngredientSelectorController : MonoBehaviour {

	/*
	public GameObject bigSelector;
	public GameObject mediumSelector;
	public GameObject smallSelector;
	*/
	public GameObject ingredientItem;

	public enum Selector { BIG, MEDIUM, SMALL }

	public WebserviceManager webserviceManager;
	public Application application;

	private GameObject m_item;
	private Product m_product;

	public GameObject[] content;

	void Start () {
	
	}

	void Update () {
	
	}

	public void InitSelector(GameObject item, Product product, Selector selector){
		Debug.Log ("Creating ingredient selector..."+product.name + "|"+product.price);

		m_item = item;
		m_product = product;
		//List<Product> ingredients = webserviceManager.GetProductsByCategory ("ingredients");
		List<Product> ingredients = webserviceManager.GetIngredientsByProduct (m_product);

		GameObject contentSelector = null;

		for (int i = 0; i < content.Length; i++) {
			content [i].transform.parent.transform.parent.gameObject.SetActive (false);
		}

		if (selector == Selector.BIG) {
			contentSelector = content [0];//bigSelector.transform.FindChild ("UIContent/Content").gameObject;
		}

		if (selector == Selector.MEDIUM) {
			contentSelector = content [1]; //mediumSelector.transform.FindChild ("UIContent/Content").gameObject;
		}

		if (selector == Selector.SMALL) {
			contentSelector = content [2]; //smallSelector.transform.FindChild ("UIContent/Content").gameObject;
		}

		foreach (Transform childTransform in contentSelector.transform)
			Destroy (childTransform.gameObject);

		foreach (Product ingredient in ingredients) {
			GameObject it = Instantiate (ingredientItem);
			it.transform.SetParent (contentSelector.transform);
			it.transform.localScale = Vector3.one;
			it.GetComponent<UIItemIngredient> ().Init (ingredient);
			Toggle to = it.transform.FindChild ("btnSelect").GetComponent<Toggle> ();
			to.onValueChanged.AddListener(delegate { SelectIngredient (it,to.isOn); } );
		}
		contentSelector.transform.parent.transform.parent.gameObject.SetActive (true);
	}

	public void CloseIngredientsSelector(){
		this.gameObject.SetActive (false);
		m_product.quantity += 1;
		Transform.FindObjectOfType<UIRoomServiceController> ().OnProductCostumized (m_product);
	}

	private void SelectIngredient(GameObject go, bool ison){
		Product ingredient = go.GetComponent<UIItemIngredient> ().GetProduct ();

		if (ison) {
			Debug.Log ("Ingredient selected.");
			m_product.ingredients.Add (ingredient);
		} else {
			for (int i = 0; i < m_product.ingredients.Count; i++) {
				if (ingredient.id == m_product.ingredients [i].id) {
					m_product.ingredients.RemoveAt (i);
					Debug.Log ("Ingredient deleted");
				}
			}
		}
	}
}

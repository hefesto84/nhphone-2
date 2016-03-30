using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UISelectBreakfastController : MonoBehaviour 
{
	public GameObject basketPanel, basketItem, basketMaskedPanel, itemsContent, itemPrefab;
	public UnityEngine.UI.Text totalLabel;
	public WebserviceManager webServiceManager;

	private List<Product> basketProducts = new List<Product>();
	private List<Product> breakfastProducts = new List<Product> ();

	public Application application;
	public UIContentController contentController;

	void OnEnable()
	{

		if (application.m_cart.List ().Count == 0) {
			basketProducts.Clear ();
			//breakfastProducts.Clear ();

		}

		breakfastProducts = webServiceManager.GetProductsByCategory ("breakfast");

		for (int i = 0; i < itemsContent.transform.childCount; i++) 
		{
			Destroy (itemsContent.transform.GetChild(i).gameObject);
		}

		for(int i = 0; i < breakfastProducts.Count; i++)
		{
			GameObject item = GameObject.Instantiate (itemPrefab);
			item.transform.SetParent (itemsContent.transform);
			item.transform.localScale = Vector3.one;
			item.transform.localPosition = Vector3.zero;
			item.GetComponent<UIProductBreakfastController> ().Init (breakfastProducts[i]);
			item.GetComponent<UIProductBreakfastController> ().SetProduct (breakfastProducts [i]);
		}
	}

	void OnDisable()
	{}

	public List<Product> getBasketProduct()
	{
		return basketProducts;
	}

	public void addBasketProduct(Product p)
	{
		basketProducts.Add (p);

	}

	public void OnBasketExit()
	{
		for(int i = 0; i < basketMaskedPanel.transform.childCount; i++)
		{
			Transform item = basketMaskedPanel.transform.GetChild(i);
			//Product p = item.GetComponent<UIItemBreakfastFinal>().GetProduct();
			Product p = item.GetComponent<UIItemRoomService>().GetProduct();
			if(p == null) continue;
			
			if(p.quantity < 1) basketProducts.Remove(p);
			
			Destroy(item.gameObject);
		}

		basketPanel.SetActive (false);
	}

	public void OnBasketClicked()
	{
		basketPanel.SetActive (true);
		float total = 0;

		Debug.Log ("BasketProducts: " + basketProducts.Count);
		for (int i = 0; i < basketProducts.Count; i++) 
		{
			GameObject item = GameObject.Instantiate(basketItem);
			item.transform.SetParent(basketMaskedPanel.transform);
			item.GetComponent<UIItemRoomService>().Init(basketProducts[i]);
			item.transform.localScale = Vector3.one;
			total += basketProducts[i].price * basketProducts[i].quantity;
		}

		totalLabel.text = "Total: " + total + "€";
	}

	public void OnFinishOrder()
	{
		Debug.Log ("Finishing the order");

		Cart cart = application.m_cart;
		foreach (Product p in basketProducts) {
			Debug.Log ("Añadimos producto: " + p.name + " - " + p.quantity);
			cart.Add (p);
		}

		application.OnCartModified (cart);
		contentController.SetScene ("CheckoutBreakfast");
	}
}

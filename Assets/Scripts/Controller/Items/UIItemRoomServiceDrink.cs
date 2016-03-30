using UnityEngine;
using System.Collections;

public class UIItemRoomServiceDrink : MonoBehaviour {

	private Product product;
	public UnityEngine.UI.Text descriptionLabel, quantityLabel, priceLabel;

	void Start () {
	
	}

	void Update () {
	
	}

	public void Init(Product p)
	{
		product = p;
		product.quantity = p.quantity;
		descriptionLabel.text = p.name;
		quantityLabel.text = product.quantity.ToString();
		priceLabel.text = product.price + " €";

	}

	public Product GetProduct()
	{
		return product;
	}

	public void AddDrink(){
		product.quantity++;
		quantityLabel.text = product.quantity.ToString();

		UIRoomServiceController roomServiceContrller = Transform.FindObjectOfType<UIRoomServiceController>();
		if(roomServiceContrller != null)
		{
			if(product.quantity < 1) return;
			Debug.Log(product.name + ": " + product.quantity + " - " + product.price);
			roomServiceContrller.addBasketProduct(product);
		}
	}

	public void SetProduct(Product product){
		descriptionLabel.text = product.name;
		priceLabel.text = product.price + " €";
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIProductBreakfastController : MonoBehaviour {

	private bool isFliped = false;
	public GameObject backLayer;
	public GameObject miniQuantity;
	public GameObject normalQuantity;

	public Text txtQuantity, txtQuantityMini, txtPrice;
	//public Text txtProductName;
	public string ProductName;
	public string ProductDescription;
	public float ProductPrice;
	private int quantity = 0;
	private Product m_product = new Product();

	public Text txtName;
	public Text txtDescription;
	public Button btnProduct;

	public void Init (Product product) {

		m_product = product;

		txtPrice.text = m_product.price + "€";

		UpdateQuantity ();
		//txtProductName.name = ProductName;
	}

	void Update () {
	
	}

	public void OnClick(GameObject source){
		
		if (source.name.Equals ("btnAddProduct")) {
			isFliped = !isFliped;
			backLayer.SetActive (isFliped);
		}

		if (source.name.Equals ("btnCancel")) {
			isFliped = !isFliped;
			backLayer.SetActive (isFliped);
		}

		if (source.name.Equals ("btnAccept")) {
			Debug.Log ("Accepted: " + quantity);
			isFliped = !isFliped;
			backLayer.SetActive (isFliped);
			UpdateQuantity ();


			UISelectBreakfastController selectBreakfastController = Transform.FindObjectOfType<UISelectBreakfastController>();
			Debug.Log ("RS: " + selectBreakfastController);
			if(selectBreakfastController != null)
			{
				if(m_product.quantity < 1) return;
				Debug.Log(m_product.name + ": " + m_product.quantity + " - " + m_product.price);
				selectBreakfastController.addBasketProduct(m_product);
			}

			/*


			UIRoomServiceController roomServiceContrller = Transform.FindObjectOfType<UIRoomServiceController>();
			Debug.Log ("RS: " + roomServiceContrller);
			if(roomServiceContrller != null)
			{
				if(m_product.quantity < 1) return;
				Debug.Log(m_product.name + ": " + m_product.quantity + " - " + m_product.price);
				roomServiceContrller.addBasketProduct(m_product);
			}

			*/

		}

		if (source.name.Equals ("btnMinus")) {
			if (quantity != 0) {
				quantity -= 1;
			}
			UpdateQuantity ();
		}

		if (source.name.Equals ("btnPlus")) {
			quantity += 1;
			UpdateQuantity ();
		}

	}

	private void UpdateQuantity()
	{
		m_product.quantity = quantity;
		txtQuantity.text = ""+m_product.quantity;
		if(txtQuantityMini != null) txtQuantityMini.text = ""+m_product.quantity;
	}

	public void SetProduct(Product product){
		Debug.Log ("Setting product: " + product.price);
		txtName.text = product.name;
		txtPrice.text = product.price + " €";

		m_product = product;
		//this.GetComponent<Image>().sprite = Resources.Load<Sprite> ("Images/Breakfast/Products/pr_"+product.id);
		btnProduct.GetComponent<Image>().sprite = Resources.Load<Sprite> ("Images/Breakfast/Products/pr_"+product.id); 

	}
}

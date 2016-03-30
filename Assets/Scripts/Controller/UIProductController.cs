using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIProductController : MonoBehaviour {

	private bool isFliped = false;
	public GameObject backLayer;
	public GameObject miniQuantity;
	public GameObject normalQuantity;
	public Image imgGlutenFree, imgVegetarian, imgLight, imgSpecial, imgInformation;

	public Text txtQuantity, txtQuantityMini, txtPrice;
	//public Text txtProductName;
	public string ProductName;
	public string ProductDescription;
	public float ProductPrice;
	private int quantity = 0;
	private Product m_product = new Product();

	public Text txtName;
	public Text txtDescription;

	public Button customizeButton;
	public Button acceptButton;

	public void Init (Product product) {

		m_product = product;
		txtPrice.text = m_product.price + "€";

		//try{
			//imgSpecial.gameObject.SetActive (m_product.special);
		//}catch{

		//}

		Debug.Log ("Questions: " + product.questions.Count);
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

		if (source.name.Equals ("btnCustomize")) {
			Debug.Log ("Accepted: " + quantity);
			Debug.Log ("Is customizable? " + m_product.customizable);

			if (m_product.customizable) {
				m_product.ingredients.Clear ();
				Transform.FindObjectOfType<Application> ().OnCustomizableProduct (this.gameObject,m_product, UIIngredientSelectorController.Selector.BIG);
				customizeButton.transform.gameObject.SetActive (false);
				acceptButton.transform.gameObject.SetActive (true);
				return;
			}
		}

		if (source.name.Equals ("btnAccept")) {

			isFliped = !isFliped;
			backLayer.SetActive (isFliped);

			Product p = new Product ();
			p = m_product;

			if (p.customizable) {

				customizeButton.transform.gameObject.SetActive (true);
				acceptButton.transform.gameObject.SetActive (false);

				string ingredients_text = "(";
				for (int i = 0; i < p.ingredients.Count; i++) {
					if (i != p.ingredients.Count - 1) {
						ingredients_text = ingredients_text + p.ingredients [i].name + ",";
					} else {
						ingredients_text = ingredients_text + p.ingredients [i].name + ")";
					}
				}

				UpdateQuantity ();
				//p.description = p.description;// + ingredients_text;
				p.quantity = 1;
			} 

			AddProductToRoomService (p);
			p.ingredients.Clear ();
		}

		if (source.name.Equals ("btnMinus")) {
			if (quantity != 0) {
				quantity -= 1;
				//Transform.FindObjectOfType<UIRoomServiceController> ().DeleteProduct ();
			}
			UpdateQuantity ();
		}

		if (source.name.Equals ("btnPlus")) {
			quantity += 1;
			//Transform.FindObjectOfType<UIRoomServiceController> ().IncreaseProduct ();
			UpdateQuantity ();
		}

	}

	private void AddProductToRoomService(Product p){

		UISelectBreakfastController selectBreakfastController = Transform.FindObjectOfType<UISelectBreakfastController>();
		if(selectBreakfastController != null)
		{
			if(p.quantity < 1) return;
			Debug.Log ("UISelectBreakfastController add");
			//Transform.FindObjectOfType<UIRoomServiceController> ().UpdateCartItemNumber ();
			selectBreakfastController.addBasketProduct(p);
		}

		UIRoomServiceController roomServiceContrller = Transform.FindObjectOfType<UIRoomServiceController>();
		if(roomServiceContrller != null)
		{
			if(p.quantity < 1) return;
			Debug.Log ("UIRoomServiceController add");
			Transform.FindObjectOfType<UIRoomServiceController> ().UpdateCartItemNumber ();
			roomServiceContrller.addBasketProduct(p);
		}

	}

	private void OnProductCustomizedHandler(Product product){
		Debug.Log ("Product Customized successfully: "+product.ingredients.Count);

	}

	public void UpdateQuantityFromCart(int quantity){
		m_product.quantity = quantity;
		txtQuantity.text = ""+m_product.quantity;
		if(txtQuantityMini != null) txtQuantityMini.text = ""+m_product.quantity;
		if (m_product.quantity != 0) {
			txtQuantityMini.transform.parent.transform.gameObject.SetActive (true);
		} else {
			txtQuantityMini.transform.parent.transform.gameObject.SetActive (false);
		}
	}

	private void UpdateQuantity()
	{
		m_product.quantity = quantity;
		txtQuantity.text = ""+m_product.quantity;
		if(txtQuantityMini != null) txtQuantityMini.text = ""+m_product.quantity;
		if (m_product.quantity != 0) {
			txtQuantityMini.transform.parent.transform.gameObject.SetActive (true);
		} else {
			txtQuantityMini.transform.parent.transform.gameObject.SetActive (false);
		}
	}

	public void SetProduct(Product product){
		txtName.text = product.name;
		txtDescription.text = product.name;
		txtPrice.text = product.price + " €";

		m_product = product;
		//this.GetComponent<Image>().sprite = Resources.Load<Sprite> ("Images/RoomService/Products/pr_img_"+product.id);
		//m_product.quantity = product.quantity;
		ReloadImage();

		quantity = m_product.quantity;
		txtQuantity.text = m_product.quantity+"";
		txtQuantityMini.text = m_product.quantity+"";
		 
		SetImageInformation (product);
		SetSpecialInformation (product);
	}

	public void SetSpecialInformation(Product product){
		imgSpecial.gameObject.SetActive (true);
		if (product.available24h && product.special) {
			imgSpecial.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductSpecialSprite (0);
		} else if (product.available24h && !product.special) {
			imgSpecial.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductSpecialSprite (1);
		} else if (!product.available24h && product.special) {
			imgSpecial.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductSpecialSprite (2);
		} else {
			imgSpecial.gameObject.SetActive (false);
		}
	}

	public void SetImageInformation(Product product){

		if (product.light && product.vegetarian && product.glutenfree) {
			imgInformation.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductInformationSprite (0);
		} else if (product.light && product.vegetarian && !product.glutenfree) {
			imgInformation.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductInformationSprite (1);
		} else if (product.light && !product.vegetarian && product.glutenfree) {
			imgInformation.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductInformationSprite (3);
		} else if (!product.light && product.vegetarian && product.glutenfree) {
			imgInformation.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductInformationSprite (2);
		} else if (product.light && !product.vegetarian && !product.glutenfree) {
			imgInformation.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductInformationSprite (6);
		} else if (!product.light && product.vegetarian && !product.glutenfree) {
			imgInformation.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductInformationSprite (4);
		} else if (!product.light && !product.vegetarian && product.glutenfree) {
			imgInformation.sprite = Transform.FindObjectOfType<MemoryManager> ().GetProductInformationSprite (5);
		} else {
			imgInformation.gameObject.SetActive (false);
		}
	}

	public void ReloadImage(){
		StartCoroutine (tLoadImage ());
	}

	IEnumerator tLoadImage(){
		WWW www = new WWW ("http://10.8.0.1:8080/NHServices/api/getImageById?imageId="+m_product.id);
		yield return www;
		this.GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
	}
}

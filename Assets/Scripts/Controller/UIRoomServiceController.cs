using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIRoomServiceController : MonoBehaviour {

	public GameObject ItemProduct;
	public GameObject ItemProductCustomizable;
	public GameObject ItemProductDrink;
	public GameObject RoomServiceContent;
	public GameObject RoomServiceContentDrinks;
	public GameObject RoomServiceMain;
	public GameObject Leftbar;
	public GameObject SectionName;
	public GameObject Breadcrumb;
	public GameObject BasketButton;
	public Text basketItemProductsNumber;
	private int basketItemProductsNumberInt = 0;

	public GameObject basketPanel, basketItem, basketMaskedPanel;
	public UnityEngine.UI.Text totalLabel;
	//public GameObject Cart;

	private List<GameObject> m_items = new List<GameObject> ();
	private List<Product> basketProducts = new List<Product>();

	public delegate void ChangeProductCategoryListener(string category);
	public event ChangeProductCategoryListener OnChangeProductCategoryListener;

	public delegate void ProductCustomizedListener(Product product);
	public event ProductCustomizedListener OnProductCustomizedListener;

	public WebserviceManager webserviceManager;
	public Application application;
	public UIContentController contentController;

	public ScrollRect scrollRect;
	public RectTransform scrollTransform;

	private List<Product> result;

	void OnEnable(){
		ReinitializeRoomService ();
		SetBreadcrumbDescription ();
		if (!Leftbar.activeInHierarchy) {
			Leftbar.SetActive (true);
		}

		if (application.m_cart == null || application.m_cart.List ().Count == 0) {
			// afegit application.m_cart == null pq petava, preguntar Dani
			basketProducts.Clear ();

		}
	}

	void OnDisable(){
		ClearProducts ();
		Leftbar.SetActive (false);
		//Cart.SetActive (false);
	}

	void Start () {
		OnChangeProductCategoryListener += ChangeProductCategoryHandler;
		OnProductCustomizedListener += OnProductCustomizedHandler;
		webserviceManager.OnRoomServiceRequestListener += OnRoomServiceRequestHandler;
	}

	void Update () {
		
	}

	public void OnFinishOrder(){
		Debug.Log ("Finishing the order: "+basketProducts.Count);

		Cart c = new Cart ();
		foreach (Product p in basketProducts) {
			c.Add (p);
		}

		application.OnCartModified (c);
		contentController.SetScene ("CheckoutRoomService");
	}

	public void OnBasketExit()
	{
		for(int i = 0; i < basketMaskedPanel.transform.childCount; i++)
		{
			Transform item = basketMaskedPanel.transform.GetChild(i);
			Product p = item.GetComponent<UIItemRoomService> ().GetProduct ();
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

		for (int i = 0; i < basketProducts.Count; i++) 
		{
			GameObject item = GameObject.Instantiate(basketItem);
			item.transform.SetParent(basketMaskedPanel.transform);
			item.GetComponent<UIItemRoomService> ().Init (basketProducts [i]);
			item.transform.localScale = Vector3.one;
			total += basketProducts[i].price * basketProducts[i].quantity;
		}
		
		totalLabel.text = "Total: " + total + "€";
	}

	public void UpdateCartItemNumber(){
		int n = 0;
		for (int i = 0; i < basketProducts.Count; i++) {
			n += basketProducts [i].quantity;
		}
		basketItemProductsNumberInt = n;
		basketItemProductsNumber.text = basketItemProductsNumberInt + "";
		basketItemProductsNumber.gameObject.SetActive (basketItemProductsNumberInt > 0);
	}

	public List<Product> getBasketProduct()
	{
		return basketProducts;
	}
	
	public void addBasketProduct(Product p)
	{
		//Product temp = p;
		Product temp = new Product();
		temp.id = p.id;
		temp.price = p.price;
		temp.customizable = p.customizable;
		temp.ingredients = p.ingredients;

		if (temp.customizable) {
			temp.name = p.name + " (" + p.GetIngredients () + " )";
		} else {
			temp.name = p.name;
		}
		temp.name = temp.name.Replace (", )", ")");
		temp.arrayIngredients = p.GetIngredientsById ();
		temp.description = p.GetIngredients ();
		temp.quantity = p.quantity;

		foreach (Product product in basketProducts) {
			if (product.id == temp.id && !product.customizable) {
				product.quantity = temp.quantity;
				return;
			}
		}
		basketProducts.Add (temp);
		UpdateCartItemNumber ();
		//basketItemProductsNumberInt += temp.quantity;
		//basketItemProductsNumber.text = basketItemProductsNumberInt + "";
		//basketItemProductsNumber.gameObject.SetActive (basketItemProductsNumberInt > 0);
	}


	private void ClearProducts(){
		m_items.Clear ();
		foreach (Transform childTransform in RoomServiceContent.transform.GetChild(0).transform)
			Destroy (childTransform.gameObject);
		foreach (Transform childTransform in RoomServiceContentDrinks.transform.GetChild(0).transform)
			Destroy (childTransform.gameObject);
		Resources.UnloadUnusedAssets ();
	}

	private void GetProductsByCategory(string category){
		ClearProducts ();
		List<Product> products = webserviceManager.GetProductsByCategory (category);

		foreach (Product p in basketProducts) {
			for (int i = 0; i < products.Count; i++) {
				if (products [i].id == p.id) {
					products [i].quantity = p.quantity;
				}
			}
		}

		if (category.Contains ("drinks")) {
			RoomServiceContentDrinks.SetActive (true);
			RoomServiceContent.SetActive (false);
		} else {
			RoomServiceContentDrinks.SetActive (false);
			RoomServiceContent.SetActive (true);
		}

		foreach (Product p in products) {
			GameObject go = null;
			if (p.categoryId == 7 || p.categoryId == 8) {
				go = Instantiate (ItemProductDrink);
				go.transform.SetParent (RoomServiceContentDrinks.transform.GetChild (0).transform);
				go.GetComponent<UIItemRoomServiceDrink> ().SetProduct (p);
				go.GetComponent<UIItemRoomServiceDrink> ().Init (p);

			} else {
				if (!p.customizable) {
					go = Instantiate (ItemProduct);
				} else {
					go = Instantiate (ItemProductCustomizable);
				}
				go.transform.SetParent(RoomServiceContent.transform.GetChild(0).transform);
				go.GetComponent<UIProductController> ().SetProduct (p);
				go.GetComponent<UIProductController> ().Init (p);

			}

			go.transform.localScale = new Vector3 (1, 1, 1);
			go.transform.localPosition = Vector3.zero;

			go.name = "Entry-"+p.id;
			m_items.Add (go);
		}

	}

	public void OnChangeProductCategory(string category){
		if (OnChangeProductCategoryListener != null) {
			OnChangeProductCategoryListener(category);
		}
	}

	private void ChangeProductCategoryHandler(string category){
		if (!RoomServiceContent.activeInHierarchy) {
			RoomServiceContent.SetActive (true);
			RoomServiceMain.SetActive (false);
			//Cart.SetActive (true);
		}

		if (!BasketButton.activeInHierarchy)
		{
			BasketButton.SetActive(true);
		}

		GetProductsByCategory (category);
	}

	private void ReinitializeRoomService(){
		RoomServiceContent.SetActive (false);
		RoomServiceMain.SetActive (true);
		Transform.FindObjectOfType<MemoryManager> ().InitCart ();
		this.result = new List<Product> ();
	}

	private void SetBreadcrumbDescription(){
		Breadcrumb.SetActive (true);
	}

	private void OnRoomServiceRequestHandler(bool result){
		contentController.SetScene ("CheckoutSuccess");
	}

	private void OnProductCustomizedHandler(Product product){
		//Debug.Log ("Product Customized successfully: "+product.ingredients.Count);
		//Debug.Log ("PRoduct price: " + product.quantity + "|" + product.price);
		//addBasketProduct (product);
	}

	public void OnProductCostumized(Product product){
		if (OnProductCustomizedListener != null) {
			OnProductCustomizedListener (product);
		}
	}

	public void ShowCart(bool show){
		BasketButton.SetActive (show);
	}
}

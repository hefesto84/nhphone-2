using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UICheckoutSuccessController : MonoBehaviour {


	public Application application;
	/*
	public WebserviceManager webserviceManager;
	public GameObject itemRoomService;
	public GameObject content;
	*/
	public Text hour,minute;
	/*
	public Text comments;
	public Text people;
	public Text lactoseLabel;
	public Text glutenLabel;
	*/
	private Cart m_cart;

	void OnEnable(){
		m_cart = application.m_cart;
		//fillProductsList ();
		hour.text = m_cart.hour;
		minute.text = m_cart.min;
	}

	void OnDisable(){
		application.m_cart.Clear ();
	}

	/*
	private void fillProductsList(){

		foreach (Product p in m_cart.List()) {
			GameObject item = GameObject.Instantiate (itemRoomService);
			item.transform.SetParent (GameObject.Find ("CheckoutSuccess/UICheckoutLeft/UICheckoutSuccessData").transform,false);
			item.transform.localPosition = Vector3.zero;
			item.transform.localScale = new Vector3 (1f, 1f, 1f);
			item.GetComponent<UIItemRoomService> ().InitSimple (p);
		}

		hour.text = m_cart.hour;
		minute.text = m_cart.min;
		comments.text = m_cart.comments;
		people.text = m_cart.people + "";

		if (m_cart.lactose_intolerants != 0) {
			lactoseLabel.transform.gameObject.SetActive (true);
			lactoseLabel.text = "Personas intolerantes a la lactosa: "+m_cart.lactose_intolerants;
		}

		if (m_cart.gluten_intolerants != 0) {
			glutenLabel.transform.gameObject.SetActive (true);
			glutenLabel.text = "Personas intolerantes al glúten: "+m_cart.gluten_intolerants;
		}

	}
	*/

	/*
	public void BackToHome(){
		GameObject parent = GameObject.Find ("CheckoutSuccess/UICheckoutLeft/UICheckoutSuccessData");
		if (parent.transform.childCount > 0) {
			for (int i = 0; i < parent.transform.childCount; i++) {
				Destroy (parent.transform.GetChild (i).gameObject);
			}
		}
		Transform.FindObjectOfType<UIContentController> ().SetScene ("Home");
	}
	*/
}

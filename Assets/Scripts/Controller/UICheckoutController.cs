using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UICheckoutController : MonoBehaviour {

	public Application application;
	public WebserviceManager webserviceManager;
	public GameObject itemRoomService;
	public GameObject itemList;
	public Text comments;
	public Text hour,minute;
	private int h, m;

	private Cart m_cart;

	void OnEnable(){
		m_cart = application.m_cart;
		comments.text = m_cart.comments;
		fillProductsList ();

		if (m_cart.breakfast) {
			h = int.Parse(m_cart.hour);
			m = int.Parse(m_cart.min);
		} else {
			var now = System.DateTime.Now;
			now.AddHours (1);
			h = now.Hour;
			m = now.Minute;
		}

		UpdateHour ();
	}

	void OnDisable(){

	}

	void Start () {
	
	}

	void Update () {
	
	}

	private void fillProductsList(){

		foreach (Transform child in itemList.transform) {
			Destroy (child.gameObject);
		}

		foreach (Product p in m_cart.List()) {
			GameObject go = Instantiate (itemRoomService);
			go.transform.SetParent (itemList.transform);
			go.transform.position = Vector3.zero;
			go.transform.localScale = new Vector3 (1f, 1f, 1f);
			go.GetComponent<UIItemRoomService> ().Init (p);
		}
	}

	public void PlusHour(){
		if (h == 23) {
			h = 0;
		} else {
			h++;
		}
	}

	public void MinusHour(){

		if (h == 0) {
			h = 23;
		} else {
			h--;
		}
	}

	public void PlusMinute(){
		if (m == 59) {
			m = 0;
		} else {
			m++;
		}
	}

	public void MinusMinute(){
		if (m == 0) {
			m = 59;
		} else {
			m--;
		}
	}

	public void UpdateHour(){
		string hs = "";
		string ms = "";

		if (h < 10) {
			hs = "0" + h;
		} else {
			hs = h + "";
		}

		if (m < 10) {
			ms = "0" + m;
		} else {
			ms = m + "";
		}

		minute.text = ms;
		hour.text = hs;
		m_cart.hour = hs;
		m_cart.min = ms;
	}

	public void SendRequest(){
		Transform.FindObjectOfType<Application> ().ShowToast ("Sending request...");
		webserviceManager.SendRoomServiceCart (m_cart);

	}

	public void OnChangeComment(){
		m_cart.comments = comments.text;
	}
}

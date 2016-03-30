using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIOrderBreakfast : MonoBehaviour 
{
	public GameObject basketPanel, finalPanel, infoBreakfastPanel, basketItem, orderFinalItem, glutenSelector, lactoseSelector, orderButton, continueButton, finalOrderButton;
	public GameObject[] objectsToDisable;
	public UIRoomServiceController roomServiceController;
	public UISelectBreakfastController selectBreakfastControler;
	public UnityEngine.UI.Text totalLabel, hourLabel, minLabel, peopleLabel, lactoseLabel, glutenLabel, commentsLabel;
	public RectTransform commentsTransform;
	private bool glutenActive = false, lactoseActive = false;
	private List<Product> productList = new List<Product> ();
	public Application application;
	public UIContentController contentController;

	public GameObject lacSelector;
	public GameObject gluSelector;
	public UnityEngine.UI.Text lacLabel;
	public UnityEngine.UI.Text gluLabel;

	void OnEnable(){
		application.m_cart.Clear ();
		lacSelector.GetComponent<UICheckboxSelectorController> ().Setup (1);
		lacSelector.GetComponent<UICheckboxSelectorController> ().Restart ();
		gluSelector.GetComponent<UICheckboxSelectorController> ().Setup (1);
		gluSelector.GetComponent<UICheckboxSelectorController> ().Restart ();
	}

	void OnDisable(){
		//application.m_cart.Clear ();
	}

	void InitBasket(List<Product> productList)
	{
		basketPanel.SetActive(true);
		infoBreakfastPanel.SetActive (false);
		continueButton.SetActive (true);
		finalOrderButton.SetActive (true);
		orderButton.SetActive (false);

		Transform itemsParent = basketPanel.transform.GetChild (0).GetChild (0);

		if(itemsParent.childCount > 0)
		{
			for(int i = 0; i < itemsParent.childCount; i++)
			{
				Destroy (itemsParent.GetChild(i).gameObject);
			}
		}

		float total = 0;
		
		for (int i = 0; i < productList.Count; i++) 
		{
			GameObject item = GameObject.Instantiate(basketItem);
			item.transform.SetParent(itemsParent);
			item.GetComponent<UIItemBreakfastFinal>().Init(productList[i]);
			item.transform.localScale = Vector3.one;
			total += productList[i].price * productList[i].quantity;
		}
		
		totalLabel.text = "Total: " + total + "€";
	}

	public void OnUpdateComments(UnityEngine.UI.Text text)
	{
		commentsLabel.text = text.text;
	}

	public void OnClockHourChanged(GameObject button)
	{
		int textToNumber;

		if (!int.TryParse(hourLabel.text, out textToNumber)) return;

		bool isUp = button.name.Contains ("Up");

		textToNumber = isUp ? textToNumber + 1 : textToNumber - 1;

		if (textToNumber > 11) textToNumber = 7;
		else if (textToNumber < 7) textToNumber = 11;

		hourLabel.text = textToNumber.ToString("00");
	}

	public void OnClockMinChanged(GameObject button)
	{
		int textToNumber;

		if (!int.TryParse(minLabel.text, out textToNumber)) return;

		bool isUp = button.name.Contains ("Up");

		textToNumber = isUp ? textToNumber + 1 : textToNumber - 1;

		if (hourLabel.text == "11")
			textToNumber = 0;
		else if (textToNumber > 59)
			textToNumber = 0;
		else if (textToNumber < 0)
			textToNumber = 59;

		minLabel.text = textToNumber.ToString("00");
	}

	public string GetBreakfastTime()
	{
		return hourLabel.text + ":" + minLabel.text;
	}

	public void OnLactoseChanged(GameObject button)
	{
		int textToNumber;

		if (!int.TryParse(lactoseLabel.text, out textToNumber)) return;

		bool isUp = button.name.Contains ("Plus");

		textToNumber = isUp ? textToNumber + 1 : textToNumber - 1;

		if (textToNumber > 99) textToNumber = 0;
		else if (textToNumber < 0) textToNumber = 99;

		lactoseLabel.text = textToNumber.ToString();
	}

	public void OnGlutenChanged(GameObject button)
	{
		int textToNumber;

		if (!int.TryParse(glutenLabel.text, out textToNumber)) return;

		bool isUp = button.name.Contains ("Plus");

		textToNumber = isUp ? textToNumber + 1 : textToNumber - 1;

		if (textToNumber > 99) textToNumber = 0;
		else if (textToNumber < 0) textToNumber = 99;

		glutenLabel.text = textToNumber.ToString();
	}

	public void OnPeopleChanged(GameObject button)
	{
		int textToNumber;

		if (!int.TryParse(peopleLabel.text, out textToNumber)) return;

		bool isUp = button.name.Contains ("Plus");

		textToNumber = isUp ? textToNumber + 1 : textToNumber - 1;

		if (textToNumber > 99) textToNumber = 1;
		else if (textToNumber < 1) textToNumber = 99;

		peopleLabel.text = textToNumber.ToString();

		lacSelector.GetComponent<UICheckboxSelectorController> ().Setup (textToNumber);
		gluSelector.GetComponent<UICheckboxSelectorController> ().Setup (textToNumber);
	}

	public int GetPeople()
	{
		int p = 0;
		if (!int.TryParse (peopleLabel.text, out p))
			return - 1;

		return p;
	}

	public void OnToggleGluten(UnityEngine.UI.Toggle toggle)
	{
		glutenActive = toggle.isOn;

		glutenSelector.SetActive (glutenActive);

		if (glutenActive && !lactoseActive)
		{
			Vector2 newSize = new Vector2 (commentsTransform.sizeDelta.x, commentsTransform.sizeDelta.y - 80);
			Vector3 newPos = new Vector3 (commentsTransform.localPosition.x, commentsTransform.localPosition.y - 15, commentsTransform.localPosition.z);

			commentsTransform.sizeDelta = newSize;
			commentsTransform.localPosition = newPos;
		}
		else if(!glutenActive && !lactoseActive)
		{
			Vector2 newSize = new Vector2 (commentsTransform.sizeDelta.x, commentsTransform.sizeDelta.y + 80);
			Vector3 newPos = new Vector3 (commentsTransform.localPosition.x, commentsTransform.localPosition.y + 15, commentsTransform.localPosition.z);

			commentsTransform.sizeDelta = newSize;
			commentsTransform.localPosition = newPos;
		}
		else if(glutenActive && lactoseActive)
		{
			Vector2 newSize = new Vector2 (commentsTransform.sizeDelta.x, commentsTransform.sizeDelta.y - 80);
			Vector3 newPos = new Vector3 (commentsTransform.localPosition.x, commentsTransform.localPosition.y - 35, commentsTransform.localPosition.z);

			commentsTransform.sizeDelta = newSize;
			commentsTransform.localPosition = newPos;
		}
		else if(!glutenActive && lactoseActive)
		{
			Vector2 newSize = new Vector2 (commentsTransform.sizeDelta.x, commentsTransform.sizeDelta.y + 80);
			Vector3 newPos = new Vector3 (commentsTransform.localPosition.x, commentsTransform.localPosition.y + 35, commentsTransform.localPosition.z);

			commentsTransform.sizeDelta = newSize;
			commentsTransform.localPosition = newPos;
		}
	}

	public void OnToggleLactose(UnityEngine.UI.Toggle toggle)
	{
		lactoseActive = toggle.isOn;

		lactoseSelector.SetActive (lactoseActive);

		if(lactoseActive && !glutenActive)
		{
			Vector2 newSize = new Vector2 (commentsTransform.sizeDelta.x, commentsTransform.sizeDelta.y - 80);
			Vector3 newPos = new Vector3 (commentsTransform.localPosition.x, commentsTransform.localPosition.y - 15, commentsTransform.localPosition.z);
			Vector3 newPosGluten = new Vector3 (glutenSelector.transform.parent.localPosition.x, glutenSelector.transform.parent.localPosition.y - 80, glutenSelector.transform.parent.localPosition.z);

			commentsTransform.sizeDelta = newSize;
			commentsTransform.localPosition = newPos;
			glutenSelector.transform.parent.localPosition = newPosGluten;
		}
		else if(!lactoseActive && !glutenActive)
		{
			Vector2 newSize = new Vector2 (commentsTransform.sizeDelta.x, commentsTransform.sizeDelta.y + 80);
			Vector3 newPos = new Vector3 (commentsTransform.localPosition.x, commentsTransform.localPosition.y + 15, commentsTransform.localPosition.z);
			Vector3 newPosGluten = new Vector3 (glutenSelector.transform.parent.localPosition.x, glutenSelector.transform.parent.localPosition.y + 80, glutenSelector.transform.parent.localPosition.z);

			commentsTransform.sizeDelta = newSize;
			commentsTransform.localPosition = newPos;
			glutenSelector.transform.parent.localPosition = newPosGluten;
		}
		else if(lactoseActive && glutenActive)
		{
			Vector2 newSize = new Vector2 (commentsTransform.sizeDelta.x, commentsTransform.sizeDelta.y - 80);
			Vector3 newPos = new Vector3 (commentsTransform.localPosition.x, commentsTransform.localPosition.y - 35, commentsTransform.localPosition.z);
			Vector3 newPosGluten = new Vector3 (glutenSelector.transform.parent.localPosition.x, glutenSelector.transform.parent.localPosition.y - 80, glutenSelector.transform.parent.localPosition.z);

			commentsTransform.sizeDelta = newSize;
			commentsTransform.localPosition = newPos;
			glutenSelector.transform.parent.localPosition = newPosGluten;
		}
		else if(!lactoseActive && glutenActive)
		{
			Vector2 newSize = new Vector2 (commentsTransform.sizeDelta.x, commentsTransform.sizeDelta.y + 80);
			Vector3 newPos = new Vector3 (commentsTransform.localPosition.x, commentsTransform.localPosition.y + 35, commentsTransform.localPosition.z);
			Vector3 newPosGluten = new Vector3 (glutenSelector.transform.parent.localPosition.x, glutenSelector.transform.parent.localPosition.y + 80, glutenSelector.transform.parent.localPosition.z);

			commentsTransform.sizeDelta = newSize;
			commentsTransform.localPosition = newPos;
			glutenSelector.transform.parent.localPosition = newPosGluten;
		}
	}

	public void StartSelectBreakfast(){

		application.m_cart = new Cart ();

		if (lacLabel.transform.gameObject.activeInHierarchy) {
			application.m_cart.lactose_intolerants = int.Parse(lacLabel.text);
		}

		if (gluLabel.transform.gameObject.activeInHierarchy) {
			application.m_cart.gluten_intolerants = int.Parse (gluLabel.text);
		}

		Debug.Log ("Gluten: " + application.m_cart.gluten_intolerants);
		Debug.Log ("Lactose: " + application.m_cart.lactose_intolerants);

		application.m_cart.people = int.Parse(peopleLabel.text);
		application.m_cart.hour = hourLabel.text;
		application.m_cart.min = minLabel.text;

		/*
		if (lactoseActive)
			application.m_cart.lactose_intolerants = int.Parse (lactoseLabel.text);

		if (glutenActive)
			application.m_cart.gluten_intolerants = int.Parse (glutenLabel.text);
		*/

		application.m_cart.comments = commentsLabel.text;
		application.m_cart.breakfast = true;
		application.OnCartModified (application.m_cart);
		contentController.SetScene ("SelectBreakfast");
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIBreadcrumbController : MonoBehaviour {

	public Text txtDescription;
	private Dictionary<string,string> breadcrumbDictionary = new Dictionary<string, string> ();
	private Dictionary<string,string> breadcrumbTranslationId = new Dictionary<string, string> ();
	private string parent;
	private bool isConfigured = false;
	public static string parentID;
	public static string descriptionID;

	void OnEnable(){
		configureBreadcrumb ();
	}

	void Start () {
		
	}

	void Update () {
	
	}

	public void OnClick(){
		GameObject.Find ("UIContent").GetComponent<UIContentController> ().SetScene (parent);
	}

	public void SetDescription(string description){
		if (!isConfigured) {
			configureBreadcrumb ();
		}
		parent = breadcrumbDictionary [description];
		parentID = breadcrumbTranslationId [parent];
		descriptionID = breadcrumbTranslationId [description];
		txtDescription.text = Application.translationManager.GetTranslation (parentID,Application.m_cultureinfo) + " | <color=#A31F34> " + Application.translationManager.GetTranslation (descriptionID,Application.m_cultureinfo) + "</color>";
	}

	private void configureBreadcrumb(){
		if (!isConfigured) {
			breadcrumbDictionary.Add ("Home", "Home");
			breadcrumbDictionary.Add ("Restaurant", "Services");
			breadcrumbDictionary.Add ("Gym", "Services");
			breadcrumbDictionary.Add ("Spa", "Services");
			breadcrumbDictionary.Add ("CocktailBar", "Services");
			breadcrumbDictionary.Add ("Breakfast", "Services");
			breadcrumbDictionary.Add ("Minibar", "Services");
			breadcrumbDictionary.Add ("SwimmingPool", "Services");
			breadcrumbDictionary.Add ("OtherServices", "Services");
			breadcrumbDictionary.Add ("WIFI", "Services");
			breadcrumbDictionary.Add ("Useful", "Services");
			breadcrumbDictionary.Add ("TVChannels", "Services");
			breadcrumbDictionary.Add ("Meetings", "Services");
			breadcrumbDictionary.Add ("Safety", "Services");
			breadcrumbDictionary.Add ("Environment", "Services");
			breadcrumbDictionary.Add ("Calls", "Home");
			breadcrumbDictionary.Add ("Front Desk Call", "Home");
			breadcrumbDictionary.Add ("RoomService", "Home");
			breadcrumbDictionary.Add ("Services", "Home");
			breadcrumbDictionary.Add ("OrderBreakfast", "Home");
			breadcrumbDictionary.Add ("WakeUp", "Home");
			breadcrumbDictionary.Add ("City Info", "Home");
			breadcrumbDictionary.Add ("Press", "Home");
			breadcrumbDictionary.Add ("Poll", "Home");
			breadcrumbDictionary.Add ("NH Rewards", "Home");
			breadcrumbDictionary.Add ("Checkout", "OrderBreakfast");
			breadcrumbDictionary.Add ("CheckoutSuccess", "Home");
			breadcrumbDictionary.Add ("CheckoutRoomService", "RoomService");
			breadcrumbDictionary.Add ("CheckoutBreakfast","SelectBreakfast");
			breadcrumbDictionary.Add ("SelectBreakfast", "OrderBreakfast");

			breadcrumbTranslationId.Add ("Home", "t9999");  // fataria el text (inici, etc...)
			breadcrumbTranslationId.Add ("Restaurant", "t0010");
			breadcrumbTranslationId.Add ("Gym", "t0011");
			breadcrumbTranslationId.Add ("Spa", "t0012");
			breadcrumbTranslationId.Add ("CocktailBar", "t0010");
			breadcrumbTranslationId.Add ("Breakfast", "t0166");
			breadcrumbTranslationId.Add ("Minibar", "t0167");
			breadcrumbTranslationId.Add ("SwimmingPool", "t0013");
			breadcrumbTranslationId.Add ("OtherServices", "t0014");
			breadcrumbTranslationId.Add ("WIFI", "t0015");
			breadcrumbTranslationId.Add ("Useful", "t9998");
			breadcrumbTranslationId.Add ("TVChannels", "t0017");
			breadcrumbTranslationId.Add ("Meetings", "t0018");
			breadcrumbTranslationId.Add ("Safety", "t0019");
			breadcrumbTranslationId.Add ("Environment", "t9997");
			breadcrumbTranslationId.Add ("Calls", "t0001");
			breadcrumbTranslationId.Add ("Front Desk Call", "t9996");
			breadcrumbTranslationId.Add ("RoomService", "t0043");
			breadcrumbTranslationId.Add ("Services", "t0004");
			breadcrumbTranslationId.Add ("OrderBreakfast", "t0005");
			breadcrumbTranslationId.Add ("WakeUp", "t0006");
			breadcrumbTranslationId.Add ("City Info", "t0007");
			breadcrumbTranslationId.Add ("Press", "t9995");
			breadcrumbTranslationId.Add ("Poll", "t9994");
			breadcrumbTranslationId.Add ("NH Rewards", "t9993");
			breadcrumbTranslationId.Add ("Checkout", "t9992");
			breadcrumbTranslationId.Add ("CheckoutSuccess", "t9991");
			breadcrumbTranslationId.Add ("CheckoutRoomService", "t9990");
			breadcrumbTranslationId.Add ("CheckoutBreakfast","t9989");
			breadcrumbTranslationId.Add ("SelectBreakfast", "t9988");

			isConfigured = true;
		}
	}
}

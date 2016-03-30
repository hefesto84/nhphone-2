using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UISectionNameController : MonoBehaviour {

	public Text sectionName;

	private Dictionary<string,string> sectionDictionary = new Dictionary<string, string> ();
	private bool isConfigured = false;
	public static string sectionID;

	void OnEnable(){
		configureSection ();
	}

	void Start () {

	}

	void Update () {

	}

	public void SetDescription(string description){
		if (!isConfigured) {
			configureSection ();
		}
		sectionID = sectionDictionary [description];
		sectionName.text = Application.translationManager.GetTranslation (sectionID,Application.m_cultureinfo);
	}

	private void configureSection(){
		if (!isConfigured) {
			sectionDictionary.Add ("Home", "t9999");  // fataria el text (inici, etc...)
			sectionDictionary.Add ("Restaurant", "t0010");
			sectionDictionary.Add ("Gym", "t0011");
			sectionDictionary.Add ("Spa", "t0012");
			sectionDictionary.Add ("CocktailBar", "t0010");
			sectionDictionary.Add ("Breakfast", "t0166");
			sectionDictionary.Add ("Minibar", "t0167");
			sectionDictionary.Add ("SwimmingPool", "t0013");
			sectionDictionary.Add ("OtherServices", "t0014");
			sectionDictionary.Add ("WIFI", "t0015");
			sectionDictionary.Add ("Useful", "t9998");
			sectionDictionary.Add ("TVChannels", "t0017");
			sectionDictionary.Add ("Meetings", "t0018");
			sectionDictionary.Add ("Safety", "t0019");
			sectionDictionary.Add ("Environment", "t9997");
			sectionDictionary.Add ("Calls", "t0001");
			sectionDictionary.Add ("Front Desk Call", "t9996");
			sectionDictionary.Add ("RoomService", "t0043");
			sectionDictionary.Add ("Services", "t0004");
			sectionDictionary.Add ("OrderBreakfast", "t0005");
			sectionDictionary.Add ("WakeUp", "t0006");
			sectionDictionary.Add ("City Info", "t0007");
			sectionDictionary.Add ("Press", "t9995");
			sectionDictionary.Add ("Poll", "t9994");
			sectionDictionary.Add ("NH Rewards", "t9993");
			sectionDictionary.Add ("Checkout", "t9992");
			sectionDictionary.Add ("CheckoutSuccess", "t9991");
			sectionDictionary.Add ("CheckoutRoomService", "t9990");
			sectionDictionary.Add ("CheckoutBreakfast","t9989");
			sectionDictionary.Add ("SelectBreakfast", "t9988");

			isConfigured = true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class PopupController : MonoBehaviour {

	public GameObject UIPopup;
	public GameObject UIPopupLanguage;

	void OnEnable(){
		UIPopupLanguage.SetActive (false);
	}

	void Start () {
	
	}

	void Update () {
	
	}

	public void ShowLanguagePopup(){
		UIPopup.SetActive (true);
		UIPopupLanguage.SetActive (true);
	}

	public void ClosePopup(){
		UIPopupLanguage.SetActive (false);
		UIPopup.SetActive (false);
	}
}

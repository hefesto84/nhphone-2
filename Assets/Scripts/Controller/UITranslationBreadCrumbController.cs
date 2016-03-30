using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITranslationBreadCrumbController : MonoBehaviour {

	//public string id;
	private Text text;

	void OnEnable(){
		text = this.GetComponent<Text> ();
		//text.text = Application.translationManager.GetTranslation (id, Application.m_cultureinfo);
	}

	void Start(){
		GameObject.Find("UI").GetComponent<Application>().OnCultureInfoChangedListener += OnCultureInfoChangedHandler;
	}

	private void OnCultureInfoChangedHandler(string cultureinfo){
		try {
			text.text = Application.translationManager.GetTranslation (UIBreadcrumbController.parentID,Application.m_cultureinfo) + " | <color=#A31F34> " + Application.translationManager.GetTranslation (UIBreadcrumbController.descriptionID,Application.m_cultureinfo) + "</color>";
		} catch {
			text.text = "Undefined Text";
		}
	}
}


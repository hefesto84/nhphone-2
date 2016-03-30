using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITranslationSectionController : MonoBehaviour {

	private Text text;

	void OnEnable(){
		text = this.GetComponent<Text> ();
	}

	void Start(){
		GameObject.Find("UI").GetComponent<Application>().OnCultureInfoChangedListener += OnCultureInfoChangedHandler;
	}

	private void OnCultureInfoChangedHandler(string cultureinfo){
		try {
			text.text = Application.translationManager.GetTranslation (UISectionNameController.sectionID, cultureinfo);
		} catch {
			text.text = "Undefined Text";
		}
	}
}

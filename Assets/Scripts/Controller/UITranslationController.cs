using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITranslationController : MonoBehaviour {

	public string id;
	private Text text;

	void OnEnable(){
		text = this.GetComponent<Text> ();
		text.text = Application.translationManager.GetTranslation (id, Application.m_cultureinfo);
	}

	void Start(){
		GameObject.Find("UI").GetComponent<Application>().OnCultureInfoChangedListener += OnCultureInfoChangedHandler;
	}

	private void OnCultureInfoChangedHandler(string cultureinfo){
		try {
			text.text = Application.translationManager.GetTranslation (id, cultureinfo);
		} catch {
			text.text = "Undefined Text";
		}
	}
}

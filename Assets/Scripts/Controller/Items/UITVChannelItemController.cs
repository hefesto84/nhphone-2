using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class UITVChannelItemController : MonoBehaviour {

	public Image icon;
	public Text number;
	public Text name;
	public Image disabled;

	private string lang;
	//private CanvasGroup canvas;

	void Start () {
		Transform.FindObjectOfType<UITVChannelsController> ().OnLanguageChangeListener += OnLanguageChangeHandler;
		//canvas = GetComponent<CanvasGroup> ();
	}

	void Update () {

	}

	public void Init(Sprite icon, int number, string name, string lang){
		this.number.text = number+"";
		this.name.text = name;
		this.lang = lang;
		this.icon.sprite = icon;
	}

	public void OnLanguageChangeHandler(string lang){
		if (this.lang == lang || lang == "ALL") {
			disabled.transform.gameObject.SetActive (false);
			//name.transform.gameObject.SetActive (true);
		} else {
			disabled.transform.gameObject.SetActive (true);
			//name.transform.gameObject.SetActive (false);
		}
	}
}

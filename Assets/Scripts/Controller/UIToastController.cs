using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class UIToastController : MonoBehaviour {

	public Text message;

	private CanvasGroup canvasGrp;

	void OnEnable(){
		Invoke ("Close", 3.5f);
		canvasGrp = GetComponent<CanvasGroup> ();
		this.gameObject.SetActive (true);
		canvasGrp.DOFade (1f, 1f);
	}

	void Start () {
	
	}

	void Update () {
	
	}

	public void Show(string text){
		message.text = text;
	}

	private void Inactive(){
		this.gameObject.SetActive (false);
	}

	private void Close(){
		canvasGrp.DOFade (0f, 1f).OnComplete (Inactive);
	}


}

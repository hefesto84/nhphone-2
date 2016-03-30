using UnityEngine;
using System.Collections;

public class UIPressController : MonoBehaviour {

	public GameObject Webview;

	void Start () {
	
	}

	void Update () {
	
	}

	public void OnClick(string url){
		Webview.SetActive (true);
		Webview.GetComponent<UIWebviewController> ().OpenUrl (url);
	}
}

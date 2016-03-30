using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICallsController : MonoBehaviour {

	public Text Destination;
	public UIContentController mContentController;

	void Start () {
	
	}

	void Update () {
	
	}

	public void OnClickDestination(string number){
		mContentController.InitCall (number);
	}

	public void OnClickNumber(GameObject number){
		Destination.text = Destination.text + number.name.Substring(3);
	}

	public void OnClickCall(){
		mContentController.InitCall (Destination.text);
	}

	public void OnClearNumber(){
		Destination.text = "";
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectableController : MonoBehaviour {

	private bool enabled = false;
	private Toggle button; 

	void Start () {
		button = this.GetComponent<Toggle> ();
	}

	void Update () {
	
	}

	public void Click(){
		Debug.Log ("Selected: " + button.isOn);
		enabled = button.isOn;
	}
}

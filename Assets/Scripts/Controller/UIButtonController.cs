using UnityEngine;
using System.Collections;

public class UIButtonController : MonoBehaviour {

	public string id;
	public int phone;
	public GameObject parent;
	public GameObject destination;
	public ButtonBehaviour buttonBehaviour;
	private Application m_application;

	public enum ButtonBehaviour { 
		BTN_DIALER,
		BTN_ACTION,
		BTN_OPEN
	}

	void Start () {
		m_application = GameObject.Find ("UI").GetComponent<Application> ();
	}

	void Update () {
	
	}

	public void OnClick(){
		if (buttonBehaviour == ButtonBehaviour.BTN_DIALER) {
			Debug.Log ("Calling to: " + phone);
		}
		/*if (buttonBehaviour == ButtonBehaviour.BTN_OPEN) {
			m_application.OnChangeSection(parent,destination);
		}*/
	}
}

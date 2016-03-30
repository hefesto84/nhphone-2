using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHoldController : MonoBehaviour {

	public BackendManager backendManager;
	public Text roomNumber;

	void OnEnable(){
		roomNumber.text = backendManager.Load ().options ["Room"];
	}

	void Start () {
	
	}

	void Update () {
	
	}
}

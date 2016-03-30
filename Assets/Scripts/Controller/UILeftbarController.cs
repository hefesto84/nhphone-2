using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILeftbarController : MonoBehaviour {

	public GameObject RoomService;
	private string currentCategory = "";

	void OnEnable(){
		RoomService = GameObject.Find ("UI/UIContent/RoomService");
		//RoomService.GetComponent<UIRoomServiceController> ().OnChangeProductCategoryListener += ChangeProductCategoryHandler;
	}

	void Start () {
	
	}

	void Update () {
	
	}

	public void OnClick(GameObject target){
		string category = target.GetComponent<UIIdentifier> ().uniqueId;
		Debug.Log ("Category: " + category + " RoomService: "+RoomService);
		RoomService.GetComponent<UIRoomServiceController> ().OnChangeProductCategory (category);
	}

	/*
	private void ChangeProductCategoryHandler(string category){
		for (int i = 0; i < this.gameObject.transform.childCount; i++) {
			if (!this.gameObject.transform.GetChild (i).GetComponent<UIIdentifier> ().uniqueId.Equals (category)) {
				this.gameObject.transform.GetChild (i).GetComponent<Toggle> ().isOn = false;
			}
		}
	}
	*/
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIItemLeftbar : MonoBehaviour {

	private Toggle toggle;
	private UIRoomServiceController serviceController;
	private UIIdentifier identifier;

	void Awake(){
		
	}

	void Start () {
		serviceController = Transform.FindObjectOfType<UIRoomServiceController> ();
		toggle = GetComponent<Toggle> ();
		identifier = GetComponent<UIIdentifier> ();
		serviceController.OnChangeProductCategoryListener += ChangeProductCategoryHandler;
	}

	void Update () {
	
	}

	private void ChangeProductCategoryHandler(string category){
		Debug.Log ("Category: " + category + " UniqueId: " + identifier.uniqueId);
		if(category.Equals(identifier.uniqueId)){
			toggle.isOn = true;
		}else{
			toggle.isOn = false;
		}
	}

}

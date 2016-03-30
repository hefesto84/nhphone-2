using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILoadingController : MonoBehaviour {

	public Image LoadingImage;
	private bool isRotating = true;

	void OnEnable(){
		isRotating = true;
	}

	void OnDisable(){
		isRotating = false;
	}

	void Start () {
	
	}

	void Update () {
		if (isRotating) {
			LoadingImage.transform.Rotate(0,0,-350*Time.deltaTime);
		}
	}

}

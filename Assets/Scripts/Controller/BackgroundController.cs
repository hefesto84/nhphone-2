using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	public Sprite[] backgrounds;
	public int enabledBackground;

	void Start () {
		if (backgrounds.Length != 0) {
			this.gameObject.GetComponent<Image> ().sprite = backgrounds [enabledBackground	];
		} else {
			Debug.LogError ("No backgrounds defined!");
		}
	}

	void Update () {

	}

	public void ChangeBackground(int background_id){
		if (background_id == -1) {
			this.gameObject.GetComponent<Image> ().enabled = false;
		} else {
			this.gameObject.GetComponent<Image> ().enabled = true;
			enabledBackground = background_id;
			this.gameObject.GetComponent<Image> ().sprite = backgrounds [enabledBackground];
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBatteryWidget : MonoBehaviour {

	public Text batteryLevel;
	public Image imageLevel;

	void Start () {
		imageLevel.GetComponent<RectTransform> ().sizeDelta = new Vector2 (20f,16.3f);
	}

	void Update () {
	
	}

	public void UpdateBatteryLevel(int level){
		batteryLevel.text = level + " %";
		if (level < 15) {
			imageLevel.color = Color.red;
		} else {
			imageLevel.color = Color.white;
		}
		imageLevel.GetComponent<RectTransform> ().sizeDelta = new Vector2 ((level*65f)/100.0f,16.3f);
	}
}

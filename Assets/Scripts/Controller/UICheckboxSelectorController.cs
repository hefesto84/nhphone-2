using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICheckboxSelectorController : MonoBehaviour {

	public GameObject selector;
	public GameObject toggle;
	public Text content;
	public int value {get; set; }

	private int max = 0;

	void OnEnable(){
		toggle.SetActive (true);
		selector.SetActive (false);
	}

	void Start () {
		
	}

	void Update () {
	
	}

	public void Setup(int max){
		this.max = max;
		this.value = max;
		this.content.text = max + "";
	}

	public void Restart(){
		toggle.GetComponent<Toggle> ().isOn = false;
	}

	public void OnToggle(){
		if (toggle.GetComponent<Toggle> ().isOn) {
			selector.SetActive (true);
			toggle.SetActive (true);
			value = 1;
			content.text = value + "";
		} else {
			selector.SetActive (false);
		}
	}

	public void OnClick(GameObject o){
		if (o.name.Equals ("btnMinus")) {
			if (value == 1) {
				value = 0;
				selector.SetActive (false);
				toggle.GetComponent<Toggle> ().isOn = false;
				toggle.SetActive (true);

				return;
			}
			value--;
			content.text = value + "";
		}

		if (o.name.Equals ("btnPlus")) {
			if (value == max) {
				return;
			}
			value++;
			content.text = value + "";
		}


	}
}

using UnityEngine;
using System.Collections;

public class UISplashController : MonoBehaviour {

	private float val = 0f;
	public GameObject LoadingBar;

	void Start () {
		InvokeRepeating ("UpdateLoadingBar", 0f, 0.1f);
	}

	void Update () {
		
	}

	public void FinishSplash(){
		val = 100f;
		CancelInvoke ("UpdateLoadingBar");
		RectTransform rt = LoadingBar.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (2048, 48);
		GameObject.Find ("UI").GetComponent<UIContentController> ().SetScene ("Home");
	}

	private void UpdateLoadingBar(){
		if (val < 0.95f) {
			val = val + 0.05f;
			RectTransform rt = LoadingBar.GetComponent<RectTransform> ();
			rt.sizeDelta = new Vector2 (2048 * val, 48);
		}
	}
}

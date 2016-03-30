using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICallProcessController : MonoBehaviour {

	public Text Destination;
	public Text Calltime;
	public SipManager mSipManager;
	private string parent;

	private bool isOnCall = false;
	private float calltimeSeconds = 0f;

	void OnEnable(){
		CancelInvoke ("UpdateCallTimer");
		Calltime.text = Mathf.Floor (0 / 60).ToString ("00") + " min " + Mathf.Floor (0 % 60).ToString ("00");
	}

	void Start () {
	
	}

	void Update () {
		
	}

	public void AcceptCall(string origin, string parent){
		Destination.text = origin;
		this.parent = parent;
		Transform.FindObjectOfType<PluginsController> ().AcceptCall ();
	}

	public void InitCall(string destination, string parent){
		Destination.text = destination;
		this.parent = parent;
		Transform.FindObjectOfType<PluginsController> ().MakeCall (destination);
	}

	public void OnDialerClick(){
		
	}

	public void OnMuteClick(){
		Transform.FindObjectOfType<PluginsController> ().Mute ();
	}

	public void OnCallButton(){
		if (isOnCall) {
			Transform.FindObjectOfType<PluginsController>().CancelCall();
			GameObject.Find ("UIContent").GetComponent<UIContentController> ().SetScene (parent);
			isOnCall = false;
		} else {
			Transform.FindObjectOfType<PluginsController> ().AcceptCall ();
			isOnCall = true;
		}

	}

	public void OnCallStablished(){
		Debug.Log ("Call stablished");
		isOnCall = true;
		StartCallTimer ();
	}

	public void OnCallFinished(){
		Debug.Log ("Call finished");
		GameObject.Find ("UIContent").GetComponent<UIContentController> ().SetScene (parent);
		isOnCall = false;
		CancelInvoke ("UpdateCallTimer");
	}

	private void StartCallTimer(){
		calltimeSeconds = 0f;
		InvokeRepeating ("UpdateCallTimer", 1, 1);
	}

	private void UpdateCallTimer(){
		calltimeSeconds += 1f;
		Calltime.text = Mathf.Floor (calltimeSeconds / 60).ToString ("00") + " min " + Mathf.Floor (calltimeSeconds % 60).ToString ("00") + " s";
	}
}

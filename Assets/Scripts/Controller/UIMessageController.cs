using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMessageController : MonoBehaviour {

	public delegate void DebugMessageListener(string message);
	public event DebugMessageListener OnDebugMessageListener;

	private Text messageLabel;

	void Start () {
		messageLabel = this.GetComponent<Text> ();
		OnDebugMessageListener += OnDebugMessageHandler;
	}


	void Update () {
	
	}

	public void OnDebugMessage(string message){
		if (OnDebugMessageListener != null) {
			OnDebugMessageListener (message);
		}
	}

	private void OnDebugMessageHandler(string message){
		messageLabel.text = message;
	}
}

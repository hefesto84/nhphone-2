using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SipManager : MonoBehaviour {

	private bool isInitialized = false;
	private AndroidJavaClass mUnityJavaClass;
	private AndroidJavaObject mUnityJavaObject;
	private UIContentController mContentController;

	public Text txtServer;
	public Text txtUsername;
	public Text txtPassword;
	//public GameObject UI;

	void Start () {
		//mContentController = UI.GetComponent<UIContentController>();
	}

	void Update () {
	
	}

	public void InitializeSipManager(){
		mUnityJavaClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		mUnityJavaObject = mUnityJavaClass.GetStatic<AndroidJavaObject> ("currentActivity");
		mUnityJavaObject.Call ("InitSipManager", txtServer.text, txtUsername.text, txtPassword.text);
		isInitialized = true;
	}

	public void RegisterSipManager(){
		if (isInitialized) {
			mUnityJavaObject.Call ("Register");
		}
	}

	public void CancelCall(){
		//if (isInitialized) {
			mUnityJavaObject.Call ("CancelCall");
		//}
	}

	public void AcceptCall(){
		//if (isInitialized) {
			mUnityJavaObject.Call ("AcceptCall");
		//}
	}

	public void MuteCall(){
		if (isInitialized) {
			// TODO Call mute call action
		}
	}

	public void MakeCall(string destination){
		//if (isInitialized) {
			mUnityJavaObject.Call ("MakeCall", destination);
		//}
	}

	/* Library listeners */
	//public void OnIncomingCall(string username){
		
	//}

	public void OnRegistered(string username){
		
	}

	public void OnInitialized(){
		
	}

	public void OnMuted(bool muted){

	}

	public void OnCancelledCall(string contact){
		
	}

	public void OnAcceptedCall(string contact){
		
	}

	public void OnMessage(string message){
		
	}

}

using UnityEngine;
using System.Collections;

public class PluginsController : MonoBehaviour {

	private AndroidJavaClass unity;
	private AndroidJavaObject currentActivity;
	private bool m_initialized = false;
	public GameObject PDFViewer;

	void Start () {
		#if !UNITY_EDITOR
		unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
		#endif
	}

	void Update () {
	
	}

	public void InitializeVoipService(string username, string password, string ip){
		#if !UNITY_EDITOR
		currentActivity.Call ("InitSipManager", ip, username, password);
		#else
		OnInitialized("Asterisk Initialized Editor Mode: "+username+password+ip);
		#endif
	}

	public void RegisterVoipService(){
		#if !UNITY_EDITOR
		currentActivity.Call ("Register");
		#else
		OnRegistered("Asterisk registered Editor Mode");
		#endif
	}

	public void UnregisterVoipService(string username, string password, string ip){

	}

	public void Back(){
		#if !UNITY_EDITOR
		currentActivity.Call ("Back");
		#endif

		/*
		if (PDFViewer.activeInHierarchy || ) {
			PDFViewer.SetActive (false);
		} else {
			currentActivity.Call ("Back");
		}
		*/
	}

	public void PDF(string path){
		PDFViewer.SetActive(true);
		PDFViewer.GetComponent<UIPDFViewer> ().OpenPDF (path);
	}

	public void OpenUrl(string url){
		Debug.Log ("OpenURL: " + url);
		#if !UNITY_EDITOR
		currentActivity.Call ("LoadWebview",url);
		#endif
	}

	public void OpenPoll(){
		string poll_url = Application.translationManager.GetPoll (Application.m_cultureinfo);
		Debug.Log ("Opening poll: " + poll_url);
		OpenUrl (poll_url);
	}

	public void MakeCall(string number){
		#if !UNITY_EDITOR
		currentActivity.Call ("MakeCall", number);
		#endif
	}

	public void AcceptCall(){
		#if !UNITY_EDITOR
		currentActivity.Call ("AcceptCall");
		#endif
	}

	public void CancelCall(){
		#if !UNITY_EDITOR
		currentActivity.Call ("CancelCall");
		#endif
	}

	public void Mute(){
		#if !UNITY_EDITOR
		currentActivity.Call ("Mute");
		#endif
	}

	public string GetIMEI(){
		string imei = "";
		imei = SystemInfo.deviceUniqueIdentifier;
		//#if !UNITY_EDITOR
		//currentActivity.Call("GetIMEI");
		//#else
		//imei = SystemInfo.deviceUniqueIdentifier;
		//#endif
		return imei;
	}

	public void OnInitialized(string data){
		Debug.Log ("OnInitialized: " + data);
		Transform.FindObjectOfType<Application> ().OnAsteriskInitialized ();
	}

	public void OnRegistered(string identifier){
		Transform.FindObjectOfType<Application> ().OnAsteriskRegistered ();
	}

	void OnSipMessage(string message){

		if (message.Contains ("SipManagerInitialized")) {
			m_initialized = true;

		}

	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class OpenWeatherManager : MonoBehaviour {

	public string APIKey;
	public string City;
	private Application m_application;

	void Start () {
		m_application = GameObject.Find ("UI").GetComponent<Application> ();
	}

	void Update () {
		
	}

	public void UpdateWeatherData(){
		StartCoroutine ("getWeatherData");
	}

	IEnumerator getWeatherData(){
		//Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Retrieving data from: http://api.openweathermap.org/data/2.5/forecast/city?id=" + City + "&APPID=" + APIKey + "&units=metric");
		WWW www = new WWW ("http://api.openweathermap.org/data/2.5/forecast/city?id="+City+"&APPID="+APIKey+"&units=metric");
		//Debug.Log ("URL: " + "http://api.openweathermap.org/data/2.5/forecast/city?id=" + City + "&APPID=" + APIKey + "&units=metric");
		yield return www;
		//Debug.Log ("Retrieved data: " + www.text);
		JSONNode root = JSON.Parse (www.text);
		Weather weather = new Weather (root);
		m_application.OnWeatherInformation (weather);
	}
}

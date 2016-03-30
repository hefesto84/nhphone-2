using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBackendController : MonoBehaviour {

	public InputField asteriskIP;
	public InputField asteriskUsername;
	public InputField asteriskPassword;
	public InputField webserviceIP;
	public InputField roomNumber;
	public InputField defaultLanguage;
	public InputField openWeatherAPI;
	public InputField openWeatherCity;
	private Configuration m_configuration;

	void Start () {
	
	}

	void Update () {
	
	}

	void OnEnable(){
		m_configuration = GameObject.Find ("UI").GetComponent<BackendManager> ().Load ();
		fillData ();
	}

	private void fillData(){
		asteriskIP.text = m_configuration.options ["AsteriskIP"];
		asteriskUsername.text = m_configuration.options ["AsteriskUsername"];
		asteriskPassword.text = m_configuration.options ["AsteriskPassword"];
		openWeatherAPI.text = m_configuration.options ["OpenWeatherAPIKEY"];
		openWeatherCity.text = m_configuration.options ["OpenWeatherCityID"];
		roomNumber.text = m_configuration.options ["Room"];
	}

	public void OnClick(){
		m_configuration.options ["AsteriskIP"] = asteriskIP.text;
		m_configuration.options ["AsteriskUsername"] = asteriskUsername.text;
		m_configuration.options ["AsteriskPassword"] = asteriskPassword.text;
		m_configuration.options ["OpenWeatherAPIKEY"] = openWeatherAPI.text;
		m_configuration.options ["OpenWeatherCityID"] = openWeatherCity.text;
		m_configuration.options ["Room"] = roomNumber.text;

		GameObject.Find ("UI").GetComponent<BackendManager> ().Save (m_configuration);
		m_configuration = GameObject.Find ("UI").GetComponent<BackendManager> ().Load ();
	}
}

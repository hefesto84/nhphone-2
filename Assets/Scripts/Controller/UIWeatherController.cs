using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIWeatherController : MonoBehaviour {

	private Weather m_weather;
	private string m_cultureinfo;
	public Text CurrentTemperature;
	public Image CurrentIcon;
	public Text CurrentDate;
	//public List<GameObject> WeatherEntries = new List<GameObject> (4);
	public GameObject[] Entries;
	public Sprite[] WeatherSprites;
	private Dictionary<string,Sprite> m_sprites = new Dictionary<string, Sprite>();

	void OnEnable(){
		m_weather = GameObject.Find ("UI").GetComponent<Application> ().GetWeather ();
		m_cultureinfo = GameObject.Find ("UI").GetComponent<Application> ().GetCultureInfo ();
		prepareSprites ();
		setWeatherData ();
	}

	void Start () {
		GameObject.Find("UI").GetComponent<Application>().OnCultureInfoChangedListener += OnCultureInfoChangedHandler;
	}

	void Update () {
	
	}

	private void prepareSprites(){
		if (m_sprites.Count == 0) {
			for (int i = 0; i < WeatherSprites.Length; i++) {
				m_sprites.Add (WeatherSprites [i].name, WeatherSprites [i]);
			}
		}
	}

	private void setWeatherData(){
		var now = System.DateTime.Now;
		var culture = new System.Globalization.CultureInfo(GameObject.Find ("UI").GetComponent<Application> ().GetCultureInfo ());

		CurrentTemperature.text = Mathf.RoundToInt(m_weather.GetData () [0].temperature) + " ºC";
	
		CurrentIcon.sprite = m_sprites [m_weather.GetData () [0].icon.Replace("n","d")];

		CurrentDate.text = culture.DateTimeFormat.GetDayName (now.DayOfWeek) + " " + now.Day + ", " + culture.DateTimeFormat.GetMonthName (now.Month);

		for (int i = 0; i<Entries.Length; i++){
			int max, min;
			max = Mathf.RoundToInt (m_weather.GetData () [i].temperature_max);
			min = Mathf.RoundToInt (m_weather.GetData () [i].temperature_min);
			Entries[i].transform.FindChild("WeatherItemDayInfo").transform.GetChild(0).GetComponent<Text> ().text = culture.DateTimeFormat.GetDayName (now.DayOfWeek);
			Entries[i].transform.FindChild ("WeatherItemDayInfo").transform.GetChild (1).GetComponent<Text> ().text = max + "ºC / " + min + "ºC";
			Entries[i].transform.FindChild ("WeatherItemDayIcon").transform.GetChild (0).GetComponent<Image> ().sprite = m_sprites [m_weather.GetData () [i].icon.Replace("n","d")];
			now = now.AddDays (1);
		}

	}
		
	private void OnCultureInfoChangedHandler(string cultureinfo){
		setWeatherData ();
	}

}

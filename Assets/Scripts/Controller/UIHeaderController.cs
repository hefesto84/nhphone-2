using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHeaderController : MonoBehaviour {

	public Text hour;
	public Text date;
	public Text holdHour;
	public Text holdDate;
	private string m_cultureinfo;

	void Start () {
		m_cultureinfo = GameObject.Find ("UI").GetComponent<Application> ().GetCultureInfo ();
		GameObject.Find("UI").GetComponent<Application>().OnCultureInfoChangedListener += OnCultureInfoChangedHandler;
	}

	void Update () {
		formatDatetime ();
	}

	private void formatDatetime(){
		
		var culture = new System.Globalization.CultureInfo(m_cultureinfo);
		var now = System.DateTime.Now;
		var hor = now.Hour;
		var minutes = now.Minute;
		var day = now.Day;

		var month = culture.DateTimeFormat.GetMonthName (now.Month);
		var dayofweek = culture.DateTimeFormat.GetDayName (now.DayOfWeek);
		hour.text = now.ToString("HH") + ":" + now.ToString ("mm");
		date.text = dayofweek.Substring (0, 3) + ". " + day + " " + month.Substring(0,3);
		holdDate.text = date.text;
		holdHour.text = hour.text;

	}

	private void OnCultureInfoChangedHandler(string cultureinfo){
		m_cultureinfo = cultureinfo;
	}

}

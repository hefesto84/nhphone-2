using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIWakeUpController : MonoBehaviour 
{
    public UnityEngine.UI.Text alarmHourLabel, alarmMinLabel;
	public WebserviceManager webserviceManager;
	public BackendManager backendManager;
	public GameObject alarmItem;
	public Transform itemsParent;

	private bool notify = false;
	private bool DEBUGMODE = false;

	private List<Alarm> alarmList = new List<Alarm> ();
	private IDictionary data = new Dictionary<string, object>();

	void Start(){
		Transform.FindObjectOfType<WebserviceManager> ().OnAlarmSavedListener += OnAlarmSavedHandler;
	}

	public IDictionary GetData()
	{
		return data;
	}

	public void SetData(IDictionary data)
	{
		this.data = data;
	}

    public void OnAlarmHourChanged(GameObject button)
    {
        int textToNumber;

        if (!int.TryParse(alarmHourLabel.text, out textToNumber)) return;

		bool isUp = button.name.Contains ("Up");

        textToNumber = isUp ? textToNumber + 1 : textToNumber - 1;

        if (textToNumber > 23) textToNumber = 0;
        else if (textToNumber < 0) textToNumber = 23;

        alarmHourLabel.text = textToNumber.ToString("00");
    }

    public void OnAlarmMinChanged(GameObject button)
    {
        int textToNumber;

        if (!int.TryParse(alarmMinLabel.text, out textToNumber)) return;

		bool isUp = button.name.Contains ("Up");

        textToNumber = isUp ? textToNumber + 1 : textToNumber - 1;

        if (textToNumber > 59) textToNumber = 0;
        else if (textToNumber < 0) textToNumber = 59;

        alarmMinLabel.text = textToNumber.ToString("00");
    }

	public void OnSaveAlarm()
	{
		int h, m;

		if (!int.TryParse (alarmHourLabel.text, out h) || !int.TryParse (alarmMinLabel.text, out m))
			return;

		Alarm alarm = new Alarm (h, m, notify);

		int room = 0;
		int.TryParse (backendManager.Load ().options ["Room"], out room);
		Debug.Log ("Sending alarm from room " + room);
		alarm.room = room;
		webserviceManager.OnAlarmCreated (alarm);

		Transform.FindObjectOfType<Application> ().ShowToast ("Alarma configurada para las " + alarm.hour + ":" + alarm.min);


	}

	private void OnAlarmSavedHandler(Alarm alarm){
		Debug.Log ("Webservice has been updated with an alarm:");

		var culture = new System.Globalization.CultureInfo(Application.m_cultureinfo);
		var now = System.DateTime.Now;
		if (!alarm.today) {
			now = now.AddDays (1);
		}
		var hor = now.Hour;
		var minutes = now.Minute;
		var day = now.Day;
		var year = now.Year;

		var month = culture.DateTimeFormat.GetMonthName (now.Month);
		var dayofweek = culture.DateTimeFormat.GetDayName (now.DayOfWeek);

		alarm.dayofweek = dayofweek;
		alarm.fulldate = day + " " + month + " " + year;

		alarmList.Add (alarm);

		for(int i = 0; i < itemsParent.childCount; i++)
		{
			Destroy (itemsParent.GetChild(i).gameObject);
		}

		for(int i = 0; i < alarmList.Count; i++)
		{
			GameObject item = GameObject.Instantiate (alarmItem);
			item.transform.parent = itemsParent;
			item.transform.localScale = Vector3.one;
			item.transform.localPosition = Vector3.zero;

			ItemWakeUpController itemController = item.GetComponent<ItemWakeUpController> ();
			itemController.Init (alarmList[i]);
		}




		//hour.text = now.ToString("HH") + ":" + now.ToString ("mm");
		//date.text = dayofweek.Substring (0, 3) + ". " + day + " " + month.Substring(0,3);
		//holdDate.text = date.text;
		//holdHour.text = hour.text;
	}

	public List<Alarm> GetAlarmList()
	{
		return alarmList;
	}

	public void SetAlarmList(List<Alarm> alarms)
	{
		alarmList = alarms;
	}
}

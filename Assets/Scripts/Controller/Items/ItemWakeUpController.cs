using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemWakeUpController : MonoBehaviour 
{
	public UnityEngine.UI.Text hourLabel, dayLabel, fullDateLabel;

	private Alarm alarm;
	private UIWakeUpController wakeupController;

	public void Init(Alarm a)
	{
		alarm = a;
		hourLabel.text = alarm.hour.ToString("00") + ":" + alarm.min.ToString("00");
		dayLabel.text = alarm.dayofweek;
		fullDateLabel.text = alarm.fulldate;
		wakeupController = GameObject.Find ("WakeUp").GetComponent<UIWakeUpController> ();

	}

	public void OnDeleteAlarm()
	{
		List<Alarm> alarms = wakeupController.GetAlarmList ();

		if (alarms.Contains (alarm)) 
		{
			alarms.Remove (alarm);
			wakeupController.SetAlarmList (alarms);
		}

		Destroy (this.gameObject);

		UpdateData (wakeupController.GetData());
	}

	public void OnToggleAlarm(UnityEngine.UI.Toggle toggle)
	{
		alarm.activated = toggle.isOn;

		GameObject image = toggle.transform.FindChild ("Image").gameObject;
		GameObject label = toggle.transform.FindChild ("Label").gameObject;
		GameObject icon = toggle.transform.FindChild ("Icon").gameObject;
		GameObject background = toggle.transform.FindChild ("Background").gameObject;

		float xPos = toggle.isOn ? 60 : -60;

		image.transform.localPosition = new Vector2 (xPos, image.transform.localPosition.y);
		background.GetComponent<UnityEngine.UI.Image>().color = toggle.isOn ? new Color32(157, 30, 50, 255) : new Color32(90, 90, 90, 255);
		icon.GetComponent<UnityEngine.UI.Image>().color = toggle.isOn ? new Color32(255, 255, 255, 255) : new Color32(0, 0, 0, 80);
		label.GetComponent<UnityEngine.UI.Text> ().text = toggle.isOn ? "On" : "Off";

		UpdateData (wakeupController.GetData());
	}

	public void UpdateData(IDictionary data)
	{
		if (data.Contains ("alarms"))
			data.Remove ("alarms");

		data.Add ("alarms", wakeupController.GetAlarmList());

		wakeupController.SetData (data);
	}
}

using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class Alarm
{
	public int hour, min;
	public bool notify, activated;
	public int room;
	public bool today { get; set; }
	public string dayofweek { get; set; }
	public string fulldate { get; set; }

	public Alarm(int hour = 0, int min = 0, bool notify = false, bool activated = true)
	{
		this.hour = hour;
		this.min = min;
		this.notify = notify;
		this.activated = activated;
		this.today = false;
		this.dayofweek = "";
		this.fulldate = "";
	}
}
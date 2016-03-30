using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Weather{

	private List<WeatherEntry> entries;

	public Weather(JSONNode data){
		parseData (data);
	}

	public List<WeatherEntry> GetData(){
		return entries;
	}

	private void parseData(JSONNode data){
		entries = new List<WeatherEntry> ();

		float temperature;
		float temperature_min;
		float temperature_max;
		string icon;

		temperature = data ["list"] [0] ["main"] ["temp"].AsFloat;

		string lastDate = data ["list"] [0] ["dt_txt"];
		lastDate = lastDate.Substring (0, 11);

		Debug.Log ("temperatura actual: " + temperature + " a "+lastDate);

		List<string> days = new List<string> ();

		Dictionary<string,float> max_temp = new Dictionary<string,float> ();
		Dictionary<string,float> min_temp = new Dictionary<string,float> ();
		Dictionary<string,string> icons = new Dictionary<string,string> ();

		string currentDate = "";
		foreach (JSONNode entry in data["list"].Childs) {
			string date = entry ["dt_txt"];
			date = date.Substring (0, 11);
			if (!date.Equals (currentDate)) {
				currentDate = date;
				days.Add (currentDate);
			}
		}

		//foreach (string day in days) {

		//}

		lastDate = "";

		foreach (JSONNode entry in data["list"].Childs) {
			string date = entry ["dt_txt"];
			date = date.Substring (0, 11);
			if (!lastDate.Equals (date)) {
				lastDate = date;
				max_temp.Add (date, entry ["main"] ["temp_max"].AsFloat);
				min_temp.Add (date, entry ["main"] ["temp_min"].AsFloat);
				icons.Add (date, entry ["weather"] [0] ["icon"]);

			} else {
				if (max_temp [lastDate] < entry ["main"] ["temp_max"].AsFloat) {
					max_temp [lastDate] = entry ["main"] ["temp_max"].AsFloat;
				}
				if (min_temp [lastDate] > entry ["main"] ["temp_min"].AsFloat) {
					min_temp [lastDate] = entry ["main"] ["temp_min"].AsFloat;
				}
			}
		}

		foreach (KeyValuePair<string,float> kvp in max_temp) {
			WeatherEntry w = new WeatherEntry();
			w.icon = icons [kvp.Key];
			w.temperature = max_temp [kvp.Key];
			w.temperature_max = max_temp [kvp.Key];
			w.temperature_min = min_temp [kvp.Key];
			//Debug.Log ("MAX: " + w.temperature_max + " MIN: " + w.temperature_min + " Icon: " + w.icon); 
			entries.Add (w);
		}

		//	string date = entry ["dt_txt"];
		//	date = date.Substring (0, 11);

		//	if (!lastDate.Equals (date)) {
		//		temperature_min = entry ["main"] ["temp_min"].AsFloat;
		//	}
			//string date = entry ["dt_txt"];
			//temperature_max = entry ["main"] ["temp_max"].AsFloat;

			/*
			if (date.Contains ("12:00:00")) {
				temperature = entry ["main"] ["temp"].AsFloat;
				temperature_min = entry ["main"] ["temp_min"].AsFloat;
				temperature_max = entry ["main"] ["temp_max"].AsFloat;
				icon = entry ["weather"] [0] ["icon"];

				WeatherEntry w = new WeatherEntry ();
				w.icon = icon;
				w.temperature = temperature;
				w.temperature_min = temperature_min;
				w.temperature_max = temperature_max;

				entries.Add (w);
			}
			*/
		
		//}

	}


}

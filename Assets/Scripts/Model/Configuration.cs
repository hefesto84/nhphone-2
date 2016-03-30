using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Configuration{

	public Dictionary<string,string> options = new Dictionary<string, string>();

	public Configuration(JSONNode data){
		options.Add ("OpenWeatherAPIKEY", data ["OpenWeatherAPIKEY"]);
		options.Add ("OpenWeatherCityID", data ["OpenWeatherCityID"]);
		options.Add ("AsteriskIP", data ["AsteriskIP"]);
		options.Add ("AsteriskUsername", data ["AsteriskUsername"]);
		options.Add ("AsteriskPassword", data ["AsteriskPassword"]);
		options.Add ("Room", data ["Room"]);
	}
}

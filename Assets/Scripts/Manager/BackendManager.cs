using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using System.Collections;

public class BackendManager : MonoBehaviour {

	public GameObject UIBackend;
	private Configuration m_configuration;


	void Start () {
		m_configuration = Load ();
	}

	void Update () {
	
	}

	public void OpenBackend(){
		UIBackend.SetActive (true);
	}

	public void CloseBackend(){
		UIBackend.SetActive (false);
	}

	public void Save(Configuration configuration){
		string output = "{\"OpenWeatherAPIKEY\": \""+configuration.options["OpenWeatherAPIKEY"]+"\",\"OpenWeatherCityID\": \""+configuration.options["OpenWeatherCityID"]+"\",\"AsteriskIP\": \""+configuration.options["AsteriskIP"]+"\",\"AsteriskUsername\":\""+configuration.options["AsteriskUsername"]+"\",\"AsteriskPassword\":\""+configuration.options["AsteriskPassword"]+"\",\"Room\":"+configuration.options["Room"]+"}";
		File.WriteAllText (UnityEngine.Application.persistentDataPath + "/config.dat", output);
	
	}

	public Configuration Load(){
		bool fileExists = File.Exists (UnityEngine.Application.persistentDataPath + "/config.dat");
		if (fileExists) {
			StreamReader sr = File.OpenText (UnityEngine.Application.persistentDataPath + "/config.dat");
			string data = sr.ReadToEnd ();
			JSONNode root = JSON.Parse (data);
			m_configuration = new Configuration (root);
			sr.Close ();
		} else {
			string output = "{\"OpenWeatherAPIKEY\": \"8de228784b8e7b80ff483c9304fbb898\",\"OpenWeatherCityID\": \"3128759\",\"AsteriskIP\": \"192.168.1.42\",\"AsteriskUsername\":\"0000\",\"AsteriskPassword\":\"000000\",\"Room\":6666}";
			File.WriteAllText (UnityEngine.Application.persistentDataPath + "/config.dat", output);
			return Load ();
		}
		return m_configuration;
	}

	public void Reset(){
		// TODO: Factory reset
	}


}

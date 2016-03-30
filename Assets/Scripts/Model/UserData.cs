using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using System;

[Serializable]
public class UserData{

	public List<Alarm> alarms { get; set; }

	public UserData(){

	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (UnityEngine.Application.persistentDataPath + "/alarms.dat", FileMode.Open);
		bf.Serialize (file, this);
		file.Close ();
	}
}


using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class Answer
{
	public int id {get; set; }
	public string content { get; set; }

	public Answer(int id = 0, string content = ""){
		this.id = id;
		this.content = content;
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Question
{
	public List<Answer> answers { get; set; }
	public string content { get; set; }

	public Question(string content = ""){
		answers = new List<Answer> ();
		this.content = content;
	}
}


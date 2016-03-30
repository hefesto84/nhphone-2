using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestingFunctions : MonoBehaviour {

	public Image imageItem;

	void Start () {
	
	}

	void Update () {
	
	}

	public void ReloadImage(){
		StartCoroutine (tLoadImage ());
	}

	IEnumerator tLoadImage(){
		WWW www = new WWW ("http://10.8.0.1:8080/NHServices/api/getImageById?imageId=1");
		yield return www;
		imageItem.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f, 0.5f));
	}
}

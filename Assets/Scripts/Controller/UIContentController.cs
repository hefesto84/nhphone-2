using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIContentController : MonoBehaviour {

	public GameObject Application;
	private Application m_application;
	private GameObject m_currentScene = null;
	private Dictionary<string,GameObject> m_pages = new Dictionary<string, GameObject>();

	void OnEnable(){
		
	}

	void Start () {
		getContents ();
		getApplication ();
	}

	void Update () {
	
	}

	public void InitCall(string uri){
		string parent = m_currentScene.name;
		SetScene ("CallProcess");
		m_currentScene.GetComponent<UICallProcessController> ().InitCall (uri,parent);
	}

	public void SetScene(string name_scene){
		hideCurrentScene ();
		m_currentScene = m_pages [name_scene];
		m_application.ConfigureScene (m_currentScene.GetComponent<UIConfiguration>(),name_scene);
		m_application.OnChangeSection (name_scene);
		m_currentScene.SetActive (true);
	}

    public string GetScene()
    {
        return m_currentScene.name;
    }

	/* Hide last scene */
	private void hideCurrentScene(){
		if (m_currentScene != null) {
			m_currentScene.SetActive (false);
		} 
	}

	/* Get all contents */
	private void getContents(){
		foreach (Transform content in this.gameObject.transform) {
			m_pages.Add (content.name, content.gameObject);
		}
	}

	/* Get the application script */
	private void getApplication(){
		m_application = Application.GetComponent<Application> ();
	}
}

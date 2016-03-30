using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class UITVChannelsController : MonoBehaviour {

	public GameObject content;
	public GameObject item;
	public GameObject filterHeader;
	public GameObject filterContent;
	public Text headerLabel;

	public delegate void LanguageChangeListener(string lang);
	public event LanguageChangeListener OnLanguageChangeListener;

	public Dictionary<string,string> languageMap = new Dictionary<string, string>();

	private Sprite[] sprites;

	void Awake(){
		sprites = Resources.LoadAll<Sprite> ("Icons/TV Channels/icons");
		Debug.Log ("Sprites: " + sprites.Length);
	}

	void Start () {

		languageMap.Add ("ALL", "Todos los idiomas");
		languageMap.Add ("ES", "Español");
		languageMap.Add ("CA", "Català");
		languageMap.Add ("EN", "English");
		languageMap.Add ("FR", "Français");
		languageMap.Add ("DE", "Deutsch");
		languageMap.Add ("PT", "Portugues");
		languageMap.Add ("IT", "Italiano");
		languageMap.Add ("NL", "Dutch");

		JSONNode channels = JSON.Parse (((TextAsset)Resources.Load ("JSON/channels", typeof(TextAsset))).text);
		foreach (JSONNode channel in channels["channels"].Childs) {
			GameObject item = GameObject.Instantiate (this.item);
			item.transform.SetParent (content.transform);
			item.transform.localScale = Vector3.one;
			int spriteIndex = channel ["id"].AsInt-1;
			item.GetComponent<UITVChannelItemController> ().Init (sprites[spriteIndex],channel ["id"].AsInt, channel ["name"], channel ["lang"]);
		}
		OnLanguageChangeListener += OnLanguageChangeHandler;
		GameObject.Find("UI").GetComponent<Application>().OnCultureInfoChangedListener += OnCultureInfoChangedHandler;
	}

	void Update () {
		
	}

	public void OnLanguageChange(string lang){
		if (OnLanguageChangeListener != null) {
			OnLanguageChangeListener (lang);
		}
	}

	public void OpenFilter(){
		if (filterContent.activeInHierarchy) {
			filterContent.SetActive (false);
		} else {
			filterContent.SetActive (true);
		}
	}

	public void FilterBy(GameObject section){
		OpenFilter ();
		string lang = section.name.Remove (0, 3);
		OnLanguageChange (lang);
	}

	private void OnLanguageChangeHandler(string lang){
		headerLabel.text = languageMap [lang];
	}

	private void OnCultureInfoChangedHandler(string cultureinfo){
		Debug.Log ("TODO: Translate Todos los idiomas!!!");
	}
}

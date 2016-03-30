using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class TranslationManager{

	private Dictionary<string,JSONNode> translations = new Dictionary<string, JSONNode>();

	private JSONNode en;
	private JSONNode es;
	private JSONNode de;
	private JSONNode nl;
	private JSONNode it;
	private JSONNode fr;
	private JSONNode ca;
	private JSONNode pt;

	private Dictionary<string,string> cultures = new Dictionary<string, string>();

	private Dictionary<string,string> polls = new Dictionary<string,string> ();

	public TranslationManager(){

		loadTranslations ();
		loadPolls ();

		translations.Add ("ca-ES", ca);
		translations.Add ("en-GB", en);
		translations.Add ("es-ES", es);
		translations.Add ("fr-FR", fr);
		translations.Add ("it-IT", it);
		translations.Add ("de-DE", de);
		translations.Add ("pt-PT", pt);
		translations.Add ("nl-NL", nl);

		cultures.Add ("ca-ES", "Català");
		cultures.Add ("en-GB", "English");
		cultures.Add ("es-ES", "Español");
		cultures.Add ("pt-PT", "Portugues");
		cultures.Add ("nl-NL", "Nederlands");
		cultures.Add ("it-IT", "Italiano");
		cultures.Add ("de-DE", "Deutsch");
		cultures.Add ("fr-FR", "Français");

	}

	public string GetPoll(string cultureinfo){
		string val = polls[cultureinfo];
		if (val!=null) {
			return val;
		}
		return polls["en-GB"];
	}

	public string GetTranslation(string id,string cultureinfo){
		string val = translations[cultureinfo][id];
		if (val!=null) {
			return val;
		}
		return translations["en-GB"][id];
	}

	public string GetFullLanguage(string culture){
		return cultures [culture];
	}

	private void loadTranslations(){
		ca = JSON.Parse (((TextAsset)Resources.Load ("Translations/ca/translations", typeof(TextAsset))).text);
		en = JSON.Parse (((TextAsset)Resources.Load ("Translations/en/translations", typeof(TextAsset))).text);
		es = JSON.Parse (((TextAsset)Resources.Load ("Translations/es/translations", typeof(TextAsset))).text);
		it = JSON.Parse (((TextAsset)Resources.Load ("Translations/it/translations", typeof(TextAsset))).text);
		de = JSON.Parse (((TextAsset)Resources.Load ("Translations/de/translations", typeof(TextAsset))).text);
		nl = JSON.Parse (((TextAsset)Resources.Load ("Translations/nl/translations", typeof(TextAsset))).text);
		fr = JSON.Parse (((TextAsset)Resources.Load ("Translations/fr/translations", typeof(TextAsset))).text);
		pt = JSON.Parse (((TextAsset)Resources.Load ("Translations/pt/translations", typeof(TextAsset))).text);

	}

	private void loadPolls(){
		polls.Add ("ca-ES", (JSON.Parse (((TextAsset)Resources.Load ("Translations/ca/data", typeof(TextAsset))).text)["poll"]));
		polls.Add ("en-GB", (JSON.Parse (((TextAsset)Resources.Load ("Translations/en/data", typeof(TextAsset))).text)["poll"]));
		polls.Add ("es-ES", (JSON.Parse (((TextAsset)Resources.Load ("Translations/es/data", typeof(TextAsset))).text)["poll"]));
		polls.Add ("fr-FR", (JSON.Parse (((TextAsset)Resources.Load ("Translations/fr/data", typeof(TextAsset))).text)["poll"]));
		polls.Add ("it-IT", (JSON.Parse (((TextAsset)Resources.Load ("Translations/it/data", typeof(TextAsset))).text)["poll"]));
		polls.Add ("de-DE", (JSON.Parse (((TextAsset)Resources.Load ("Translations/de/data", typeof(TextAsset))).text)["poll"]));
		polls.Add ("pt-PT", (JSON.Parse (((TextAsset)Resources.Load ("Translations/pt/data", typeof(TextAsset))).text)["poll"]));
		polls.Add ("nl-NL", (JSON.Parse (((TextAsset)Resources.Load ("Translations/nl/data", typeof(TextAsset))).text)["poll"]));

	}
}

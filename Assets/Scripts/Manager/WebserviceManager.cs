using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using System;
using System.Text;

public class WebserviceManager : MonoBehaviour{

	public string ChannelsURI;
	public string TranslationsURI;
	public string ProductsURI;
	public string PhoneShorcutsURI;
	public string RoomServiceRequestURI;
	private string NetworkIP;
	private string NetworkPort;

	public delegate void AsteriskConfigurationListener(string text);
	public event AsteriskConfigurationListener OnAsteriskConfigurationListener;
	public delegate void LoadedTranslationsListener(string text);
	public event LoadedTranslationsListener OnLoadedTranslationsListener;
	public delegate void LoadedProductsListener(string text);
	public event LoadedProductsListener OnLoadedProductsListener;

	public delegate void LoadedPhoneShortcutsListener(string text);
	public event LoadedPhoneShortcutsListener OnLoadedPhoneShorcutsListener;

	public delegate void RoomServiceRequestListener(bool result);
	public event RoomServiceRequestListener OnRoomServiceRequestListener;
	public delegate void AlarmCreatedListener(Alarm alarm);
	public event AlarmCreatedListener OnAlarmCreatedListener;
	public delegate void AlarmSavedListener(Alarm alarm);
	public event AlarmSavedListener OnAlarmSavedListener;

	public Application application;

	private string base64RoomServiceRequest = "";
	private List<Product> m_products = new List<Product> ();

	private Dictionary<string,List<Product>> products = new Dictionary<string, List<Product>>();

	public bool offlineMode;
	private string config_url;

	void Start(){
		OnLoadedProductsListener += OnLoadedProductsHandler;
		OnAsteriskConfigurationListener += OnAsteriskConfigurationHandler;
		OnAlarmCreatedListener += OnAlarmCreatedHandler;
	}

	void Update(){

	}

	public void UpdateData(){
		StartCoroutine ("updateData");
	}

	private void ExtractIP(string data){
		JSONNode root = JSON.Parse (data);
		NetworkIP = root ["ip"];
		NetworkPort = root ["port"];
		Configuration conf = Transform.FindObjectOfType<BackendManager> ().Load ();
		conf.options ["AsteriskIP"] = NetworkIP;
		Transform.FindObjectOfType<BackendManager> ().Save (conf);
	}

	private void ExtractInfo(string data){
		JSONNode root = JSON.Parse (data);
		Configuration conf = Transform.FindObjectOfType<BackendManager> ().Load ();

		conf.options ["Room"] = root["user"]["room"];
		conf.options ["AsteriskUsername"] = root ["user"] ["user"];
		conf.options ["AsteriskPassword"] = root ["user"] ["password"];

		Debug.Log ("Room configured: " + conf.options ["Room"]);
		Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Room: " + conf.options ["Room"] + "configured.");
		Transform.FindObjectOfType<BackendManager> ().Save (conf);
	}

	IEnumerator updateData(){
		Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Updating information from webservice");

		#if UNITY_EDITOR
		config_url = "http://www.frozenbullets.com/devel.json";
		#else
		config_url = "http://www.frozenbullets.com/devel.json";
		#endif

		Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Updating data from: " + config_url);

		WWW www = new WWW (config_url);
		yield return www;
		ExtractIP (www.text);
	

		string imei = Transform.FindObjectOfType<PluginsController> ().GetIMEI ();
		www = new WWW ("http://"+NetworkIP+":"+NetworkPort+"/NHServices/api/getUserInformation?identificador="+imei);
		Debug.Log ("http://" + NetworkIP + ":" + NetworkPort + "/NHServices/api/getUserInformation?identificador=" + imei);
		Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Retrieving configuration");
		yield return www;
		ExtractInfo (www.text);


		www = new WWW ("http://"+NetworkIP+":"+NetworkPort+"/NHServices/api/listRoomService?lang="+application.GetCultureInfo().Substring(0,2)+"&name=NH%20Collection%20Barcelona%20Constanza");
		Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Retrieving data from: "+ NetworkIP + ":" + NetworkPort);;
		yield return www; 
		OnLoadedProducts (www.text);

	}

	public List<Channel> GetChannels(){
		List<Channel> channels = new List<Channel> ();
		return channels;
	}

	public void OnAsteriskConfiguration(string text){
		if (OnAsteriskConfigurationListener != null) {
			OnAsteriskConfigurationListener (text);
		}
	}

	public void OnLoadedProducts(string text){
		if (OnLoadedProductsListener != null) {
			OnLoadedProductsListener (text);
		}
	}

	public void OnRoomServiceRequest(bool result){
		if (OnRoomServiceRequestListener != null) {
			OnRoomServiceRequestListener (result);
		}
	}

	public void OnAlarmCreated(Alarm alarm){
		if (OnAlarmCreatedListener != null) {
			OnAlarmCreatedListener (alarm);
		}
	}

	public void OnAlarmSaved(Alarm alarm){
		if (OnAlarmSavedListener != null) {
			OnAlarmSavedListener (alarm);
		}
	}

	private void OnAsteriskConfigurationHandler(string text){
		JSONNode root = JSON.Parse (text);
		Configuration conf = Transform.FindObjectOfType<BackendManager> ().Load ();
		conf.options ["Room"] = root ["identificador"];
		Transform.FindObjectOfType<BackendManager> ().Save (conf);
	}

	private void OnLoadedProductsHandler(string text){
		if (!offlineMode) {
			File.WriteAllText (UnityEngine.Application.persistentDataPath + "/products.txt", text);
		}
		Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Products updated");
		ReloadProducts ();
		Transform.FindObjectOfType<Application> ().InitializeOnAsterisk ();
		Transform.FindObjectOfType<Application> ().RegisterAsterisk ();
	}

	private void OnAlarmCreatedHandler(Alarm alarm){
		StartCoroutine (sendAlarmRequest(alarm));
	}
		
	public void SendRoomServiceCart(Cart cart){
		
		byte[] bytesToEncode = Encoding.UTF8.GetBytes (Utils.ToJson(cart));
		base64RoomServiceRequest = Convert.ToBase64String (bytesToEncode);

		StartCoroutine (sendRequest(cart.breakfast));

	}

	IEnumerator sendAlarmRequest(Alarm alarm){
		string str = "http://"+NetworkIP+":"+NetworkPort+"/NHServices/api/wakeUp?room=" + alarm.room + "&time=" + alarm.hour+"%3A"+alarm.min + "&lang="+Application.m_cultureinfo;
		Debug.Log ("Alarm query: " + str);
		WWW www = new WWW (str);
		yield return www;
		JSONNode root = JSON.Parse (www.text);
		Debug.Log ("IS TODAY? : " + root ["today"]);
		alarm.today = root ["today"].AsBool;
		OnAlarmSaved (alarm);
	}

	IEnumerator sendRequest(bool breakfast){

		string url = "";

		if (breakfast) {
			url = "http://"+NetworkIP+":"+NetworkPort+"/NHServices/api/breakfastService?query=" + base64RoomServiceRequest;
		} else {
			url = "http://"+NetworkIP+":"+NetworkPort+"/NHServices/api/roomService?query=" + base64RoomServiceRequest;
			//url = "http://"+NetworkIP+":"+NetworkPort+"/NHServices/api/roomService?lang="+application.GetCultureInfo().Substring(0,2)+"&name=NH%20Collection%20Barcelona%20Constanza";
		}


		Debug.Log ("Sending request: " + url);
		WWW www = new WWW (url);
		yield return www;
		Debug.Log ("Finished request: ");
		OnRoomServiceRequest (true);
		Transform.FindObjectOfType<UIContentController> ().SetScene ("CheckoutSuccess");
	}

	IEnumerator sendBreakfastRequest(){

		string url = "http://"+NetworkIP+":"+NetworkPort+"/NHServices/api/breakfastService?query=" + base64RoomServiceRequest;
		Debug.Log ("Sending request: " + url);
		WWW www = new WWW (url);
		yield return www;
		Debug.Log ("Finished request");
		OnRoomServiceRequest (true);
		Transform.FindObjectOfType<UIContentController> ().SetScene ("CheckoutSuccess");
		//contentController.SetScene ("CheckoutSuccess");
	}

	public void ReloadProducts(){
		StreamReader sr = File.OpenText (UnityEngine.Application.persistentDataPath+"/products.txt");
		string content = sr.ReadToEnd ();
		JSONNode root = JSON.Parse (content);
	
		List<Product> drinks_alcohol = new List<Product> ();
		List<Product> drinks_noalcohol = new List<Product> ();
		List<Product> mains = new List<Product> ();
		List<Product> starters = new List<Product> ();
		List<Product> deserts = new List<Product> ();
		List<Product> salads = new List<Product> ();
		List<Product> rolls = new List<Product> ();
		List<Product> pasta = new List<Product> ();
		List<Product> breakfast = new List<Product> ();
		List<Product> ingredients = new List<Product> ();

		foreach (JSONNode entry in root["list"].Childs) {
			Product p = new Product ();
			p.id = entry ["id"].AsInt;
			p.name = entry ["titulo"];
			p.price = entry ["precio"].AsFloat;
			p.categoryId =  entry ["categoria"].AsInt;

			// Comprovar si tenim preguntes en aquest producte

			bool question = entry ["question"].AsBool;
			if (question) {
				foreach(JSONNode quest in entry["questionText"].Childs){
					Question q = new Question (quest ["pregunta"]);
					foreach (JSONNode answer in quest["respuesta"].Childs) {
						Answer a = new Answer (answer ["id"].AsInt, answer ["respuesta"]);
						q.answers.Add (a);
					}
					p.questions.Add (q);
				}
			}

			if (entry ["tipo"].AsInt == 2) {
				if (entry ["categoria"].AsInt == 7) {
					drinks_noalcohol.Add (p);
				}
				if (entry ["categoria"].AsInt == 8) {
					drinks_alcohol.Add (p);
				}
			}

			if (entry ["tipo"].AsInt == 1) {
				
				p.description = entry ["subtitulo"];
				p.glutenfree = entry ["sin_gluten"].AsBool;
				p.vegetarian = entry ["vegetariano"].AsBool;
				p.light = entry ["ligero_calorias"].AsBool;
				p.categoryId = entry ["categoria"].AsInt;
				p.customizable = entry ["customizable"].AsBool;
				p.special = entry ["plato_especial"].AsBool;
				p.available24h = entry ["disponible_24h"].AsBool;
				p.ingredientType = entry ["ingredient"];
				Debug.Log ("TYPE: " + p.ingredientType);
				int category = entry ["categoria"].AsInt;

				switch (category) {
				case 1:
					salads.Add (p);
					break;
				case 2:
					pasta.Add (p);
					break;
				case 3:
					deserts.Add (p);
					break;
				case 4:
					starters.Add (p);
					break;
				case 5:
					rolls.Add (p);
					break;
				case 6:
					mains.Add (p);
					break;
				}
			}

			if (entry ["tipo"].AsInt == 3) {
				p.glutenfree = entry ["sin_gluten"].AsBool;
				p.vegetarian = entry ["vegetariano"].AsBool;
				p.light = entry ["ligero_calorias"].AsBool;
				ingredients.Add (p);
			}

			if (entry ["tipo"].AsInt == 4) {
				p.description = entry ["subtitulo"];
				p.glutenfree = entry ["sin_gluten"].AsBool;
				p.vegetarian = entry ["vegetariano"].AsBool;
				p.light = entry ["ligero_calorias"].AsBool;
				p.categoryId = entry ["categoria"].AsInt;
				p.customizable = entry ["customizable"].AsBool;
				breakfast.Add (p);
			}
		
		}

		products.Add ("drinks_alcohol", drinks_alcohol);
		products.Add ("drinks_noalcohol", drinks_noalcohol);
		products.Add ("salads", salads);
		products.Add ("deserts", deserts);
		products.Add ("mains", mains);
		products.Add ("starters", starters);
		products.Add ("rolls", rolls);
		products.Add ("pasta", pasta);
		products.Add ("breakfast", breakfast);
		products.Add ("ingredients", ingredients);

	}

	public List<Product> GetProductsByCategory(string category){
		Debug.Log ("Getting products for: " + category);
		return products [category];
	}

	public List<Product> GetIngredientsByProduct(Product product){
		
		Debug.Log ("Getting ingredients for a given product and category: "+product.categoryId);
		List<Product> ingredients = new List<Product> ();
		foreach (Product ingredient in products["ingredients"]) {
			if(ingredient.ingredientType.Equals("salsa")){
				ingredients.Add(ingredient); 
			}
		}

		return ingredients;
	}
}

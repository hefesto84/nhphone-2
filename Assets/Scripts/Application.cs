using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Application : MonoBehaviour {

	// Culture info: http://timtrott.co.uk/culture-codes/

	public GameObject UIHeader;
	public GameObject UIBreadcrumb;
	public GameObject UIBreadcrumbWhite;
	public GameObject UIFooter;
	public GameObject UILeftbar;
	public GameObject UISectionName;
	public GameObject UIHotelName;
	public GameObject UIContent;
	public GameObject UIHold;
	public GameObject UISplash;
	public GameObject UILegend;
	public GameObject UILanguage;
	public GameObject UIIngredientsSelector;
	public GameObject UIToast;

	public Text batteryLevel;

	public int secondsToBlock;
	public float timer;
	public string timerFormatted;

	private BackgroundController m_backgroundController;

	private Weather m_weather;
	public static string m_cultureinfo = "en-GB";

	public delegate void ChangeSectionListener(string section);
	public event ChangeSectionListener OnChangeSectionListener;

	public delegate void WeatherInformationListener(Weather weather);
	public event WeatherInformationListener OnWeatherInformationListener;

	public delegate void CultureInfoChangedListener(string cultureinfo);
	public event CultureInfoChangedListener OnCultureInfoChangedListener;

	public delegate void CallButtonListener(string uri);
	public event CallButtonListener OnCallButtonListener;

	/* Asterisk Registration */
	public delegate void AsteriskInitializedListener ();
	public event AsteriskInitializedListener OnAsteriskInitializedListener;

	public delegate void AsteriskRegisteredListener ();
	public event AsteriskRegisteredListener OnAsteriskRegisteredListener;
	/* End of Asterisk Registratio */

	public delegate void ApplicationReadyListener ();
	public event ApplicationReadyListener OnApplicationReadyListener;

	public delegate void CartModifiedListener(Cart cart);
	public event CartModifiedListener OnCartModifiedListener;

	public delegate void CustomizableProductListener(GameObject item, Product product, UIIngredientSelectorController.Selector selector);
	public event CustomizableProductListener OnCustomizableProductListener;

	private Configuration m_configuration;

	public Cart m_cart { get; set; }

	public static TranslationManager translationManager;

	private float lastTouchTime = 0f;
	private bool registeredOnAsterisk = false;

	void Awake(){
		UISplash.SetActive (true);
		m_configuration = GetComponent<BackendManager> ().Load ();
	}

	void Start () {
		
		m_backgroundController = this.gameObject.GetComponent<BackgroundController> ();



		OnChangeSectionListener += OnChangeSectionHandler;
		OnWeatherInformationListener += OnWeatherInformationHandler;
		OnCultureInfoChangedListener += OnCultureInfoChangedHandler;
		OnApplicationReadyListener += OnApplicationReadyHandler;
		OnCartModifiedListener += OnCartModifiedHandler;
		OnCustomizableProductListener += OnCustomizableProductHandler;

		OnAsteriskInitializedListener += OnAsteriskInitializedHandler;
		OnAsteriskRegisteredListener += OnAsteriskRegisteredHandler;

		lastTouchTime = Time.time;

		InvokeRepeating ("UpdateWeatherInformation", 1, 3600);

		InitializeApplication();

	}

	void Update () {

		#if UNITY_EDITOR 
		if(Input.GetMouseButtonDown (0) || Input.anyKey){
				CloseHoldScreen();
				lastTouchTime = Time.time;
		}
		#else
			if(Input.touchCount > 0){
				CloseHoldScreen();
				lastTouchTime = Time.time;
			}
		#endif

		if (Time.time - lastTouchTime > secondsToBlock) {
			OpenHoldScreen ();
		}
	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GetComponent<PluginsController> ().Back ();
		}

	}

	public void ConfigureScene(UIConfiguration configuration, string name_scene){
		
		UIBreadcrumb.SetActive (configuration.Breadcrumb);
		UIBreadcrumbWhite.SetActive (configuration.BreadcrumbWhite);
		UIHotelName.SetActive (configuration.HotelName);
		UIFooter.SetActive (configuration.Footer);
		UILeftbar.SetActive (configuration.Leftbar);
		UISectionName.SetActive (configuration.SectionName);

		if (configuration.NHBackground) {
			m_backgroundController.ChangeBackground (0);
		}

		if (configuration.WeatherBackground) {
			m_backgroundController.ChangeBackground (1);
		}

		if (configuration.WhiteBackground) {
			m_backgroundController.ChangeBackground (-1);
		}

		Resources.UnloadUnusedAssets ();
	}

	private void UpdateWeatherInformation(){
		this.GetComponent<OpenWeatherManager> ().UpdateWeatherData ();
	}

	private void InitializeApplication(){
		translationManager = new TranslationManager ();
		this.GetComponent<WebserviceManager> ().UpdateData ();
	}

	public void InitializeOnAsterisk(){
		Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Initializing Asterisk Connection");
		Debug.Log("Initializing Asterisk Connection");
		Configuration conf = Transform.FindObjectOfType<BackendManager> ().Load ();
		Transform.FindObjectOfType<PluginsController> ().InitializeVoipService (conf.options["Room"], conf.options["AsteriskPassword"], conf.options["AsteriskIP"]);
	}

	public void RegisterAsterisk(){
		Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Registering on Asterisk");
		Debug.Log("Registering on Asterisk");
		Transform.FindObjectOfType<PluginsController> ().RegisterVoipService ();
	}

	public void OnChangeSection(string section){
		if (OnChangeSectionListener != null) {
			OnChangeSectionListener (section);
		}
	}

	public void OnAsteriskInitialized(){
		if (OnAsteriskInitializedListener != null) {
			OnAsteriskInitializedListener ();
		}
	}

	public void OnAsteriskRegistered(){
		if (OnAsteriskRegisteredListener != null) {
			OnAsteriskRegisteredListener ();
		}
	}

	public void OnWeatherInformation(Weather weather){
		if (OnWeatherInformationListener != null) {
			OnWeatherInformationListener (weather);
		}
	}

	public void OnCultureInfoChanged(string cultureinfo){
		if (OnCultureInfoChangedListener != null) {
			OnCultureInfoChangedListener (cultureinfo);
		}
	}

	public void OnCallButton(string uri){
		if (OnCallButtonListener != null) {
			OnCallButtonListener (uri);
		}
	}

	public void OnApplicationReady(){
		if (OnApplicationReadyListener != null) {
			OnApplicationReadyListener ();
		}
	}

	public void OnCartModified(Cart cart){
		if (OnCartModifiedListener != null) {
			OnCartModifiedListener (cart);
		}
	}

	public void OnCustomizableProduct(GameObject item, Product product, UIIngredientSelectorController.Selector selector){
		if (OnCustomizableProductListener != null) {
			OnCustomizableProductListener (item, product, selector);
		}
	}

	private void OnApplicationReadyHandler(){
		//Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Application ready.");

		// ojo possible causa del retorn a home en anglès

		OnCultureInfoChanged ("en-GB");
		UIContent.GetComponent<UIContentController> ().SetScene ("Home");
		StartCoroutine ("wait");
		//InitializeOnAsterisk ();
	}
	 
	IEnumerator wait(){
		yield return new WaitForSeconds(2);
		UISplash.SetActive (false);
	}

	private void OnChangeSectionHandler(string section){
		if (section.Equals ("CallProcess") || section.Equals("Weather")) {

		} else {
			//UISectionName.transform.FindChild ("txtUISectionName").GetComponent<Text> ().text = section;
			UISectionName.GetComponent<UISectionNameController> ().SetDescription (section);
			UIBreadcrumb.GetComponent<UIBreadcrumbController> ().SetDescription (section);
		}
	}

	private void OnWeatherInformationHandler(Weather weather){
		//Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Weather information updated");
		m_weather = weather;

	}

	private void OnAsteriskInitializedHandler(){
		if (UISplash.activeInHierarchy) {
			Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Asterisk Initialized");
		}
		Debug.Log ("Asterisk Initialized");
		RegisterAsterisk ();
	}

	private void OnAsteriskRegisteredHandler(){
		if (UISplash.activeInHierarchy) {
			Transform.FindObjectOfType<UIMessageController> ().OnDebugMessage ("Asterisk Registered");
		}
		Debug.Log ("Asterisk Registered");
		registeredOnAsterisk = true;
		GameObject.Find ("UI").GetComponent<Application> ().OnApplicationReady ();
	}

	private void OnCultureInfoChangedHandler(string cultureinfo){
		Debug.Log ("Culture info changed: " + cultureinfo);
		m_cultureinfo = cultureinfo;
		UIHeader.gameObject.transform.FindChild ("btnChangeLanguage/txtCurrentLanguage").GetComponent<Text> ().text = Application.translationManager.GetFullLanguage(m_cultureinfo);
		GetComponent<PopupController> ().ClosePopup ();
		Transform.FindObjectOfType<PluginsController> ().GetIMEI ();
	}

	private void OnCallButtonHandler(string uri){
		Debug.Log ("Calling to: " + uri);
		UIContent.GetComponent<UIContentController>().SetScene("CallProcess");
	}

	private void OnCartModifiedHandler(Cart cart){
		Debug.Log ("Modifying cart...");
		m_cart = cart;
	}

	private void OnCustomizableProductHandler(GameObject item, Product product, UIIngredientSelectorController.Selector selector){
		UIIngredientsSelector.SetActive (true);
		UIIngredientsSelector.GetComponent<UIIngredientSelectorController> ().InitSelector (item,product,selector);
	}

	public Weather GetWeather(){
		return m_weather;
	}

	public string GetCultureInfo(){
		return m_cultureinfo;
	}

	public void UpdateDataFromWebservice(){
		TextAsset products = (TextAsset)Resources.Load ("JSON/products", typeof(TextAsset));
	}

	public void OnInitialized(){
		OnAsteriskInitialized ();
	}

	public void OnRegistered(){
		OnAsteriskRegistered ();
	}

	public void OnIMEI(string imei){
		Debug.Log ("IMEI: " + imei);
	}

	public void OnBatteryLevel(string level){
		foreach (UIBatteryWidget batteryWidget in Transform.FindObjectsOfType<UIBatteryWidget>()) {
			batteryWidget.UpdateBatteryLevel (int.Parse (level));
		}
	}

	public void OnIncomingCall(string uri){
		GameObject.Find ("UIContent").GetComponent<UIContentController> ().InitCall (uri);
	}

	public void OnCancelledCall(string uri){
		Debug.Log ("On Cancelled Call: " + uri);
		GameObject.Find ("UIContent/CallProcess").GetComponent<UICallProcessController> ().OnCallFinished ();
	}

	public void OnAcceptedCall(string uri){
		Debug.Log ("On Accepted Call: " + uri);
		GameObject.Find ("UIContent/CallProcess").GetComponent<UICallProcessController> ().OnCallStablished ();
	}

	public void CloseHoldScreen(){
		if (UIHold.activeSelf) {
			UIHold.SetActive (false);
			lastTouchTime = 0f;
		}
	}

	public void OpenHoldScreen(){
		#if !UNITY_EDITOR
		UIHold.SetActive (true);
		#endif
	}

	public void ShowLegend(bool show){
		UILegend.SetActive (show);
	}

	public void ShowLanguage(bool show){
		UILanguage.SetActive (show);
	}

	public void ConfigureCart(bool isBreakfast){
		m_cart = new Cart ();
		m_cart.breakfast = isBreakfast;
	}

	public void ShowToast(string message){
		UIToast.SetActive (true);
		UIToast.GetComponent<UIToastController> ().Show (message);
	}
}

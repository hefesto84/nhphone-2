using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class UIPDFViewer : MonoBehaviour {


	private string path;
	private int pages;
	private int currentPage = 1;
	private string m_path = "";

	public Image sprite;
	public GameObject PDFViewer;
	public GameObject UISwiperHelper;

	void OnEnable(){
		this.GetComponent<CanvasGroup> ().alpha = 1;
		UISwiperHelper.transform.gameObject.SetActive (true);
		currentPage = 1;
		ChangePage ();

	}

	void OnDisable(){
		this.GetComponent<CanvasGroup> ().alpha = 0;
	}

	void Start () {

	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			NextPage ();
		}

		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			PreviousPage ();
		}
	}

	private void ChangePage(){
		sprite.sprite = Resources.LoadAsync<Sprite> (m_path + "-" + currentPage).asset as Sprite;
	}

	public void NextPage(){
		
		if (currentPage < 36) {
			currentPage += 1;
		}
		ChangePage ();

	}

	public void PreviousPage(){
		if (currentPage > 1) {
			currentPage -= 1;
		}
		ChangePage ();
	}

	public void OpenPDF(string path){
		
		if (path.Contains ("Cocktails")) {
			pages = 24;
		}

		if (path.Contains ("Restaurant")) {
			pages = 36;
		}

		if (path.Contains ("Wine")) {
			pages = 25;

		}
		m_path = path;
		ChangePage ();
	}

	public void Close(){
		
		if (PDFViewer.activeInHierarchy) {
			PDFViewer.SetActive (false);
		}

	}
}

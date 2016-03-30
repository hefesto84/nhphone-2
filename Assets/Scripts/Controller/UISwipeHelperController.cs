using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class UISwipeHelperController : MonoBehaviour
{

	public Image hand;
	public Image arrow;
	private CanvasGroup canvasGrp;

	void OnEnable(){
		Invoke ("Close", 3.5f);
		canvasGrp = GetComponent<CanvasGroup> ();
		this.gameObject.SetActive (true);
		canvasGrp.DOFade (1f, 1f);
		StartAnimation ();
	}

	void OnDisable(){
		CancelInvoke ();	
	}

	void Start ()
	{
		
	}

	void Update ()
	{
	
	}

	private void StartAnimation(){
		hand.transform.DOLocalMoveX (66f, 1f, false).OnComplete (BackAnimation);
		Vector3 rotation = hand.transform.localEulerAngles;
		rotation.z = 0f;
		arrow.transform.localEulerAngles = rotation;
	}

	private void BackAnimation(){
		hand.transform.DOLocalMoveX (-33f, 1f, false).OnComplete (StartAnimation);
		Vector3 rotation = hand.transform.localEulerAngles;
		rotation.z = 180f;
		arrow.transform.localEulerAngles = rotation;
	}

	private void Inactive(){
		this.gameObject.SetActive (false);
	}

	private void Close(){
		canvasGrp.DOFade (0f, 1f).OnComplete (Inactive);
	}
}


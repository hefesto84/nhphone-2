using UnityEngine;
using System.Collections;

public class UIItemPressController : MonoBehaviour {

	public void OnClick(GameObject go)
    {
		UnityEngine.Application.OpenURL (go.GetComponent<UIItemPress>().URL);
	}
}

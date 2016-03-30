using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIItemIngredient : MonoBehaviour {

	public Text productName;
	private Product m_product;
	private bool selected = false;
	public Button btnSelector;

	void Start () {
	
	}

	void Update () {
	
	}

	public void Init(Product product){
		m_product = product;
		productName.text = product.name;
	}

	public Product GetProduct(){
		return m_product;
	}

	public void OnClick(){
		if (!selected) {
			selected = true;
			UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (null);
			Debug.Log ("Selected");
		}
	}
}

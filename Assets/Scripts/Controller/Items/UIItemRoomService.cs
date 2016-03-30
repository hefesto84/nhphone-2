using UnityEngine;
using System.Collections;

public class UIItemRoomService : MonoBehaviour {

	private Product product;
	public UnityEngine.UI.Text descriptionLabel, quantityLabel, totalLabel;
	public UnityEngine.UI.Button minusButton, plusButton; 

	void Start () {
	
	}

	void Update () {
	
	}

	public void Init(Product p)
	{
		product = p;
		descriptionLabel.text = p.name;
		SetPrice ();
		//totalLabel.text = (p.quantity * p.price) + "€";
		quantityLabel.text = p.quantity.ToString();


	}

	private void SetPrice(){
		try{
			totalLabel.text = (product.quantity * product.price) + "€";
		}catch{

		}
	}

	public void InitSimple(Product p){
		product = p;
		if (p.customizable) {
			descriptionLabel.text = p.name + p.description;
		} else {
			descriptionLabel.text = p.name;
		}
		quantityLabel.text = p.quantity.ToString();

	}

	public void OnClick(GameObject button)
	{
		if(button.name == "btnMinus")
		{
			product.quantity = (product.quantity > 0) ? product.quantity - 1 : 0;
			quantityLabel.text = product.quantity.ToString();
			SetPrice ();
			if (product.quantity == 0) {
				this.gameObject.SetActive (false);

			}
			//totalLabel.text = (product.quantity * product.price) + "€";
		}

		if(button.name == "btnPlus")
		{
			product.quantity++;
			quantityLabel.text = product.quantity.ToString();
			SetPrice ();
			//totalLabel.text = (product.quantity * product.price) + "€";
		}
		Debug.Log ("Changing: Entry-" + product.id);
		GameObject.Find ("Entry-" + product.id).GetComponent<UIProductController> ().UpdateQuantityFromCart (product.quantity);

	}

	public Product GetProduct()
	{
		return product;
	}
}

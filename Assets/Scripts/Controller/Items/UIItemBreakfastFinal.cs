using UnityEngine;
using System.Collections;

public class UIItemBreakfastFinal : MonoBehaviour 
{
	public UnityEngine.UI.Text descriptionLabel, quantityLabel, totalLabel;
	public UnityEngine.UI.Button minusButton, plusButton; 

	private Product product;

	public void Init(Product p)
	{
		descriptionLabel.text = p.name;
		totalLabel.text = (p.quantity * p.price) + "€";
		quantityLabel.text = p.quantity.ToString();

		product = p;
	}

	public void OnClick(GameObject button)
	{
		if(button.name == "btnMinus")
		{
			product.quantity = (product.quantity > 0) ? product.quantity - 1 : 0;
			quantityLabel.text = product.quantity.ToString();
			totalLabel.text = (product.quantity * product.price) + "€";
		}

		if(button.name == "btnPlus")
		{
			product.quantity++;
			quantityLabel.text = product.quantity.ToString();
			totalLabel.text = (product.quantity * product.price) + "€";
		}
	}

	public Product GetProduct()
	{
		return product;
	}
}

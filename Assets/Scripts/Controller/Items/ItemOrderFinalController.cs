using UnityEngine;
using System.Collections;

public class ItemOrderFinalController : MonoBehaviour 
{
	public UnityEngine.UI.Text productName, productQuantity;

	public void Init(Product product)
	{
		productName.text = product.name;
		productQuantity.text = product.quantity.ToString();
	}
}

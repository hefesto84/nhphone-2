using UnityEngine;
using System.Collections;
using System;

public class Utils : MonoBehaviour {

	public BackendManager mBackendManager;

	void Start () {
		Configuration c = mBackendManager.Load ();
		Debug.Log ("C: " + c.options ["Room"]);
	}

	void Update () {
	
	}

	public static string ToJson(Cart cart){
		string response = "";

		string header = "{\"products\":[";
		string data = "";

		foreach (Product p in cart.List()) {

			string ingr = "";
			for (int i = 0; i < p.arrayIngredients.Length; i++) {
				ingr = ingr + p.arrayIngredients [i] + "#";
			}
			if (ingr.Length > 2) {
				ingr = ingr.Substring (0, ingr.Length-1);
			}
			data = data + "{\"id\":" + p.id + ",\"quantity\":" + p.quantity + ",\"ingredients\":\""+ingr+"\"},";

		}

		data = data.Remove (data.Length - 1);
		data = data + "],";

		string room = Transform.FindObjectOfType<BackendManager> ().Load ().options ["Room"];
		string comments = cart.comments + "\r\n"+"Intolerantes lactosa: "+cart.lactose_intolerants+"\r\n"+"Intolerantes glúten: "+cart.gluten_intolerants;
		string footer = "\"hour\":\""+cart.hour+"\",\"min\":\""+cart.min+"\",\"room\":"+room+",\"comments\":\""+comments+"\", \"people\":"+cart.people+"}";

		response = header + data + footer;
		return response;
	}
}

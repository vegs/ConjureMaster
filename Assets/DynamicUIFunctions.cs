using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DynamicUIFunctions : MonoBehaviour {
	GameObject ss;

	void Awake(){
		ss = GameObject.Find ("StockSlider");
		ss.transform.GetComponentInChildren<Text>().text = ss.GetComponent<Slider>().value.ToString();
	}

	public void OnStockSliderValueChange(){
		ss.transform.GetComponentInChildren<Text>().text = ss.GetComponent<Slider>().value.ToString();
			
	}
}

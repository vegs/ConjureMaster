using UnityEngine;
using System.Collections;

public class ItemData{

	private int _itemID;
	private string _itemType;


	public void Pickup(){
		Debug.Log ("Pick up item");
	}

	public void Throw(){
		Debug.Log ("Throw item");
	}


	/*
	 * 
	 * 	Constructor
	 * 
	 */
	
	public ItemData (){

	}

	
	/*
	 * 
	 * 	Getters/Setters
	 * 
	 */
	public int itemID{
		get { return _itemID; }
		set { _itemID = value; }
	}

	public string itemType{
		get { return _itemType; }
		set { _itemType = value; }
	}
}

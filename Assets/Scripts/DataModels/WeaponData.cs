using UnityEngine;
using System.Collections;

public class WeaponData : ItemData {

	private float _strModifier;
	private Vector3 _dirModifier;
	private float _dmgModifier;
	private string _weaponType;


	/*
	 * 
	 * 	Constructor
	 * 
	 */
	public WeaponData(){

	}
	

	/*
	 * 
	 * 	Getters/Setters
	 * 
	 */
	public float strModifier{
		get { return _strModifier; }
		set { _strModifier = value; }
	}

	public Vector3 dirModifier{
		get { return _dirModifier; }
		set { _dirModifier = value; }
	}
	public float dmgModifier{
		get { return _dmgModifier; }
		set { _dmgModifier = value; }
	}
	public string weaponType{
		get { return _weaponType; }
		set { _weaponType = value; }
	}

}

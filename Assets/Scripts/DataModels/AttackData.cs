using UnityEngine;
using System.Collections.Generic;

public class AttackData {
	public string attackName = "";
	public Vector3 dirModifier = Vector3.forward;
	public float dmgModifier = 1f;
	public float forceModifier = 1f;
	public float preHitDelay = 0.5f;
	public float postHitDelay = 0.5f;
	public float hitDuration = 0.5f;
	public bool hasPlayerControl = false;
	public bool isAOE = false;
	public GameObject hitBox = null;
	public string fxName = null;
//	cooldown??

//	
//	/*
//	 * 	Constructor
//	 */
//	public AttackData(){
//		
//	}
//
//	/*
//	 * 	Getters/Setters
//	 */
//	public int attackID{
//		get { return _attackID; }
//		set { _attackID = value; }
//	}
//
//	public string attackName{
//		get { return _attackName; }
//		set { _attackName = value; }
//	}
//	
//	public float strModifier{
//		get { return _strModifier; }
//		set { _strModifier = value; }
//	}
//	
//	public Vector3 dirModifier{
//		get { return _dirModifier; }
//		set { _dirModifier = value; }
//	}
//	public float dmgModifier{
//		get { return _dmgModifier; }
//		set { _dmgModifier = value; }
//	}
//
//	public GameObject hitBox{
//		get { return _hitBox; }
//		set { _hitBox = value; }
//	}
}

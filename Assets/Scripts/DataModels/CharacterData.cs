using UnityEngine;
using System.Collections.Generic;

public class CharacterData {

	/// The characters name
	public string characterName = "BasicCharacter";

	/// The characters ID
	public int characterID = 0;

	/// List of the characters attacks
	public List<AttackChain> _attackMoves=new List<AttackChain>();
	
	/// The characters damage strength.
	public float attackPower = 1f;

	/// The characters knockback strength.
	public float attackForce = 1f;

	/// The characters damage resistance. (Set to 100 = 100% damage reduction)
	public float dmgReduction = 0;

	/// The characters knockback resistance. (Set to 100 = 100% knockback reduction)
	public float knockbackReduction = 0;



	
	
	/*
	 * 
	 * 	Constructor
	 * 
	 */
//	public CharacterData(){
//		
//	}

	public void AddAttackChain (AttackChain attackChain){
		_attackMoves.Add(attackChain);
		//Debug.Log (attack.hitBox);
		//_attackMoves.ForEach(attackMove => Debug.Log(attackMove.attackName));
		//Debug.Log (_characterName + "Attack *"+_attacks.ForEach+"* added to "+_characterName+"'s skillset");
	}

	//public AttackData GetAttack (int index){
//		if ((index + 1 >= 0 ) && (index - 1 <= _attackMoves.FindLastIndex)){
//			return _attackMoves[index];
//		}else{
//			Debug.LogError (" Wtf no index pls???! ");
//		}
//		return _attackMoves[index];
//		_attackMoves.Add(attack);
//		_attackMoves.ForEach(attackMove => Debug.Log(attackMove.attackName));
//		//Debug.Log (_characterName + "Attack *"+_attacks.ForEach+"* added to "+_characterName+"'s skillset");
//	}
	
	/*
	 * 
	 * 	Getters/Setters
	 * 
	 */
//	public string characterName{
//		get { return _characterName; }
//		set { _characterName = value; }
//	}
//	
//	public int characterID{
//		get { return _characterID; }
//		set { _characterID = value; }
//	}
//	public List<AttackData> attackMoves{
//		get { return _attackMoves; }
//		set { _attackMoves = value; }
//	}
}

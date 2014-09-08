using UnityEngine;
using System.Collections;

public class TestWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
		WeaponData myWeapon = new WeaponData ();
		myWeapon.itemID = 0;
		myWeapon.itemType = "bat";
		
		myWeapon.Pickup ();
		myWeapon.Throw ();

		Debug.Log (myWeapon.itemType);

		CharacterData testCharacter = new CharacterData ();
		AttackData attack1 = new AttackData ();
		attack1.attackName = "super awesome attack";

		AttackData attack2 = new AttackData ();
		attack2.attackName = "mega super awesome attack";

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

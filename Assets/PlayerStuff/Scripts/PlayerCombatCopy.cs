//using UnityEngine;
//using System.Collections.Generic;
//
//public class PlayerCombatCopy : MonoBehaviour {
//	
//	CharacterController cc;
//	Animator anim;
//
//	GameObject currentAttackHitBox = null;
//	public AttackData currentAttack = null;
//	public AttackChain currentAttackChain = null;
//	public bool isUsingAttack = false;
//	public bool isInAttackChain = false;
//	int attackID = 0;
//	float currentAttackTime = 0;
//	public CharacterData thisChar = null;
//	List<AttackData> attackMoves;
//	List<AttackChain> attackChains = new List<AttackChain>();
//	bool weaponEquipped = false;
//	List<GameObject> attackHitBoxes = new List<GameObject>();
//
//
//	void Start () {
//		thisChar = GetComponent<Character> ().myCharacter;
//		attackChains = thisChar._attackMoves;
//		cc = GetComponent<CharacterController> ();
//		anim = GetComponent<Animator> ();
//
//
//		// Instantiates and transforms the characters hitboxes and parents them to the player.
//		foreach (AttackChain attackChain in attackChains) {
//			foreach (AttackData attack in attackChain.subAttacks) {
//				GameObject thisAttackHitBox = (GameObject) Instantiate(attack.hitBox);
//
//				thisAttackHitBox.SetActive(false);
//				thisAttackHitBox.transform.rotation = this.transform.rotation;
//				thisAttackHitBox.transform.position = this.transform.position 
//					+ this.transform.forward * thisAttackHitBox.transform.position.z 
//						+ this.transform.up * thisAttackHitBox.transform.position.y;
//				
//				thisAttackHitBox.transform.parent = this.transform;
//
//				attackChain.attackChainHitBoxes.Add(thisAttackHitBox);
//			}
//		}
//	}
//	
//	// Attack 1
//	void Update () {
//
//		if (isInAttackChain && currentAttackChain != null) {
//			currentAttack = currentAttackChain.subAttacks[attackID];
//
//			if (isUsingAttack  && currentAttack != null){
//				//Debug.Log("hitbox: " + attackHitBoxes[currentAttack.attackID]);
//				
//				currentAttackTime += Time.deltaTime;
//				
//				//Attack has ended
//				if (currentAttackTime > currentAttack.preHitDelay + currentAttack.hitDuration + currentAttack.postHitDelay ){ 
//					if(attackID >= currentAttackChain.subAttacks.Count - 1){
//						isUsingAttack = false;
//						isInAttackChain = false;
//						currentAttackChain = null;
//					}else{
//						attackID++;
//					}
//					currentAttackTime = 0;
//					if (currentAttackHitBox.activeSelf){
//						currentAttackHitBox.SetActive (false);
//					}
//					currentAttack = null;
//				}
//				else{
//					//Attack is dangerous. Activate the hitbox
//					if ((currentAttackTime > currentAttack.preHitDelay) && (currentAttackTime < currentAttack.preHitDelay + currentAttack.hitDuration)) {
//						if (!currentAttackHitBox.activeSelf){
//							currentAttackHitBox.SetActive (true);
//						}
//						//					CalculateTarget(currentAttackHitBox);
//						
//					}
//					//Attack in pre/post hit state
//					else{
//						if (currentAttackHitBox.activeSelf){
//							currentAttackHitBox.SetActive (false);
//						}				
//					}
//				}
//				
//			}
//
//		}
//
//		//No weapon. use regualr abilities
//		if (!weaponEquipped && !isUsingAttack){
//			//Special is activated
//			if(Input.GetButton("Special")){
//				//Ground attacks
//				if (cc.isGrounded && cc != null){
//
//				}
//				//Airborne attacks
//				else{
//					
//				}
//			//Regular attacks
//			}else{
//				//Ground attacks
//				if (cc.isGrounded && cc != null){
//					//Attack 1
//					if(Input.GetButton("WeakAttack") ) {
//						
//						if (Input.GetButton("Horizontal")){
////							isUsingAttack = true;
////							currentAttack = attackMoves[1];
////							currentAttackHitBox = attackHitBoxes[currentAttack.attackID];
////							
////							Debug.Log ("Using " + currentAttack.attackName);
//						}else{
//							isInAttackChain = true;
//							isUsingAttack = true;
//							currentAttackChain = attackChains[0];
//							attackID = 0;
//
////							currentAttackHitBox = attackHitBoxes[currentAttack.attackID];
//							//currentAttackHitBox = currentAttack.hitBox;
//
//							Debug.Log ("Using " + currentAttack.attackName);
//						}
//					}
//					//Attack 2
////					else if(Input.GetButton("StrongAttack")) {
////
////						if (Input.GetButton("Horizontal")){
////							isUsingAttack = true;
////							currentAttack = attackMoves[4];
////							currentAttackHitBox = attackHitBoxes[currentAttack.attackID];
////							
////							Debug.Log ("Using " + currentAttack.attackName);
////						}else{
////
////							isUsingAttack = true;
////							currentAttack = attackMoves[0];
////							currentAttackHitBox = attackHitBoxes[currentAttack.attackID];
////							
////							Debug.Log ("Using " + currentAttack.attackName);
////						}
////					}
//				}
//				//Airborne attacks
//				else{
//				
//				}
//			}
//		}
//		//What happens when we use a weapon??
//		else{
//
//		}
//	}
//
//	void FixedUpdate(){
//		if (isUsingAttack) {
//			anim.SetBool("IsAttacking", true);
//			anim.SetInteger("AttackID", currentAttack.attackID);
//		} else {
//			anim.SetBool("IsAttacking", false);
//		}
//	}
//}

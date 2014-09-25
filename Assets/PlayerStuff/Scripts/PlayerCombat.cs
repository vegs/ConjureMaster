using UnityEngine;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour {
	
	CharacterController cc;
	Animator anim;
	
	GameObject currentAttackHitBox = null;
	public AttackData currentAttack = null;
	public AttackChain currentAttackChain = null;
	public bool isUsingAttack = false;
	public bool isInAttackChain = false;
	public bool isDangerous = false;
	int attackID = 0;
	float currentAttackTime = 0;
	public CharacterData thisChar = null;
	//List<AttackData> attackMoves;
	List<AttackChain> attackChains = new List<AttackChain>();
	bool weaponEquipped = false;
	List<GameObject> attackHitBoxes = new List<GameObject>();
	FXManager fx = null;
	bool fxIsOn=false;
	
	void Start () {

		thisChar = GetComponent<Character> ().myCharacter;
		attackChains = thisChar._attackMoves;
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();
		fx = GameObject.FindObjectOfType<FXManager> ();
		if (anim == null) {
			Debug.LogError("Can not find an animator!");		
		}
		
		
		// Instantiates and transforms the characters hitboxes and parents them to the player.
		foreach (AttackChain attackChain in attackChains) {
			foreach (AttackData attack in attackChain.subAttacks) {
				GameObject thisAttackHitBox = (GameObject) Instantiate(attack.hitBox);
				
				thisAttackHitBox.SetActive(false);
				thisAttackHitBox.transform.rotation = this.transform.rotation;
				thisAttackHitBox.transform.position = this.transform.position 
					+ this.transform.forward * thisAttackHitBox.transform.position.z 
						+ this.transform.up * thisAttackHitBox.transform.position.y;
				
				thisAttackHitBox.transform.parent = this.transform;
				
				attackChain.attackChainHitBoxes.Add(thisAttackHitBox);
			}
		}
	}
	
	// Attack 1
	void Update () {
		
		if (isInAttackChain && currentAttackChain != null) {
			currentAttack = currentAttackChain.subAttacks[attackID];
			currentAttackHitBox = currentAttackChain.attackChainHitBoxes[attackID];
			
			if (isUsingAttack  && currentAttack != null){
				//Debug.Log("hitbox: " + attackHitBoxes[currentAttack.attackID]);
				
				currentAttackTime += Time.deltaTime;
				
				//Attack has ended
				if (currentAttackTime > currentAttack.preHitDelay + currentAttack.hitDuration + currentAttack.postHitDelay ){ 
					if(attackID >= currentAttackChain.subAttacks.Count - 1){
						isUsingAttack = false;
						isInAttackChain = false;
						currentAttackChain = null;
						currentAttack = null;
					}else{
						attackID++;
					}
					currentAttackTime = 0;
					fxIsOn=false;
					if (currentAttackHitBox.activeSelf){
						currentAttackHitBox.SetActive (false);
					}

				}
				else{
					//Attack is dangerous. Activate the hitbox
					if ((currentAttackTime > currentAttack.preHitDelay) && (currentAttackTime < currentAttack.preHitDelay + currentAttack.hitDuration)) {
						if (!currentAttackHitBox.activeSelf){
							currentAttackHitBox.SetActive (true);
						}
						if (!fxIsOn && currentAttack.fxName != null && fx != null) {
							//fx.GetComponent<PhotonView>().RPC ("PlayFX", PhotonTargets.All, currentAttack.visualFX, this.transform.position);
							fx.GetComponent<PhotonView>().RPC ("PlayFX", PhotonTargets.All, currentAttack.fxName,this.transform.position, this.transform.position);
//							fx.PlayFX (currentAttack.visualFX, this.transform.position);
							fxIsOn = true;
						}
						isDangerous=true;
						//					CalculateTarget(currentAttackHitBox);
						
					}
					//Attack in pre/post hit state
					else{
						if (currentAttackHitBox.activeSelf){
							currentAttackHitBox.SetActive (false);
						}
						isDangerous=false;
					}
				}
				
			}
			
		}
		
		//No weapon. use regualr abilities
		if (!weaponEquipped && !isUsingAttack){
			//Special is activated
			if(Input.GetButton("Special")){
				//Ground attacks
				if (cc.isGrounded && cc != null){
					
				}
				//Airborne attacks
				else{
					
				}
				//Regular attacks
			}else{
				//Ground attacks
				if (cc.isGrounded && cc != null){
					//Weak attacks (LMB)
					if(Input.GetButton("WeakAttack") ) {
						if (Input.GetButton("Vertical")){
							InitiateAttackChain(1);
						}
						else if (Input.GetButton("Horizontal")){
							InitiateAttackChain(2);
						}else{
							InitiateAttackChain(0);
						}
					}
					//Strong attacks (RMB)
					else if(Input.GetButton("StrongAttack")) {
					
						if (Input.GetButton("Horizontal")){
							InitiateAttackChain(5);
						}else{
							InitiateAttackChain(4);
						}
					}
				}
				//Airborne attacks
				else{
					
				}
			}
		}
		//What happens when we use a weapon??
		else{
			
		}
	}
	
	void FixedUpdate(){
		if (isUsingAttack && currentAttack != null) {
			anim.SetBool("IsAttacking", true);
			anim.SetInteger("AttackChainID", currentAttackChain.id);
			anim.SetInteger("AttackID", attackID);

		} else {
			anim.SetBool("IsAttacking", false);
		}

	}



	void InitiateAttackChain (int id){
		isInAttackChain = true;
		isUsingAttack = true;
		currentAttackChain = attackChains[id];
		attackID = 0;
	}

}

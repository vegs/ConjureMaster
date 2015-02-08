using UnityEngine;
using System.Collections;

public class DefaultCharacter : CharacterData {
	HitBoxes hbx = GameObject.FindObjectOfType<HitBoxes>();
	


	public DefaultCharacter(){

		characterID = 0;
		characterName = "DefaultCharacter";

		attackPower = 20f;
		attackForce = 20f;

		dmgReduction = 0;
		knockbackReduction = 0;


		/*
		 * WEAK ATTACKS: LEFT MOUSE BUTTON (LMB) 
		 */

		// + nothing/s : Weak attack
		BasicAttack weakAttack = new BasicAttack ();	    
		weakAttack.attackName = "Weak Attack";
		weakAttack.dirModifier = new Vector3 (0, 0, 1);
		weakAttack.dmgModifier = 1f;
		weakAttack.forceModifier= 0.01f;
		weakAttack.preHitDelay = 0.12f;
		weakAttack.postHitDelay = 0.12f;
		weakAttack.hitDuration = 0.1f;
		weakAttack.hasPlayerControl = false;
		weakAttack.isAOE = false;
		weakAttack.hitBox = hbx.HitBox_Front_Big;
		weakAttack.fxName = null;	
			


		AttackChain weakAttackChain = new AttackChain (0);
		weakAttackChain.AddAttack (weakAttack);
		weakAttackChain.AddAttack (weakAttack);
		AddAttackChain (weakAttackChain);


		
		// + w : Weak sliding tackle attack
		MovementAttack weakSlidingAttack = new MovementAttack ();
		weakSlidingAttack.dirModifier = new Vector3 (0, 1, 2);
		weakSlidingAttack.dmgModifier = 0.5f;
		weakSlidingAttack.forceModifier = 0.5f;
		weakSlidingAttack.preHitDelay = 0;
		weakSlidingAttack.postHitDelay = 0.1f;
		weakSlidingAttack.hitDuration = 0.5f;
		weakSlidingAttack.hasPlayerControl = false;
		weakSlidingAttack.isAOE = false;
		weakSlidingAttack.hitBox = hbx.HitBox_Front_Big;
		weakSlidingAttack.fxName = null;
		weakSlidingAttack.moveDir = new Vector3 (0, 0, 1);
		weakSlidingAttack.moveSpeed = 30f;
		weakSlidingAttack.moveDist = 10f;
		
		AttackChain weakSlidingAttackChain = new AttackChain (1);
		weakSlidingAttackChain.AddAttack (weakSlidingAttack);
		AddAttackChain (weakSlidingAttackChain);

		// + a/d : Weak AOE attack
		BasicAttack weakAOEAttack = new BasicAttack ();	    
		weakAOEAttack.attackName = "Weak AOE Attack";
		weakAOEAttack.dirModifier = new Vector3 (0, 0, 1);
		weakAOEAttack.dmgModifier = 1f;
		weakAOEAttack.forceModifier= 0.1f;
		weakAOEAttack.preHitDelay = 0.1f;
		weakAOEAttack.postHitDelay = 0.1f;
		weakAOEAttack.hitDuration = 0.5f;
		weakAOEAttack.hasPlayerControl = true;
		weakAOEAttack.isAOE = true;
		weakAOEAttack.hitBox = hbx.HitBox_AOE_Big;
		weakAOEAttack.fxName = "AOESwishFX";	


		AttackChain weakAOEAttackChain = new AttackChain (2);
		weakAOEAttackChain.AddAttack (weakAOEAttack);
		AddAttackChain (weakAOEAttackChain);



		// airborne : Weak airborne AOE attack
		BasicAttack weakAOEAttack_Air = new BasicAttack ();	    
		weakAOEAttack_Air.attackName = "Weak Airborne AOE Attack";
		weakAOEAttack_Air.dirModifier = new Vector3 (0, 0, 1);
		weakAOEAttack_Air.dmgModifier = 1f;
		weakAOEAttack_Air.forceModifier= 1f;
		weakAOEAttack_Air.preHitDelay = 0.1f;
		weakAOEAttack_Air.postHitDelay = 0.1f;
		weakAOEAttack_Air.hitDuration = 0.5f;
		weakAOEAttack_Air.hasPlayerControl = true;
		weakAOEAttack_Air.isAOE = true;
		weakAOEAttack_Air.hitBox = hbx.HitBox_AOE_Air;
		weakAOEAttack_Air.fxName = null;
		
		AttackChain weakAOEAttack_AirChain = new AttackChain (3);
		weakAOEAttack_AirChain.AddAttack (weakAOEAttack_Air);
		AddAttackChain (weakAOEAttack_AirChain);



		/*
		 * STRONG ATTACKS: RIGHT MOUSE BUTTON (RMB) 
		 */

		// + nothing/s : Smash
		BasicAttack fwdSmash = new BasicAttack ();	    
		fwdSmash.attackName = "Forward Smash";
		fwdSmash.dirModifier = new Vector3 (0, 1, 2);
		fwdSmash.dmgModifier = 1f;
		fwdSmash.forceModifier= 1f;
		fwdSmash.preHitDelay = 0.3f;
		fwdSmash.postHitDelay = 0.3f;
		fwdSmash.hitDuration = 0.2f;
		fwdSmash.hasPlayerControl = false;
		fwdSmash.isAOE = false;
		fwdSmash.hitBox = hbx.HitBox_Front_Big;
		fwdSmash.fxName = null;	

		AttackChain fwdSmashChain = new AttackChain (4);
		fwdSmashChain.AddAttack (fwdSmash);
		AddAttackChain (fwdSmashChain);

		
		// + w : Strong charge attack
		

		// + a/d : Strong AOE attack
		BasicAttack strongAOEAttack = new BasicAttack ();	    
		strongAOEAttack.attackName = "Strong AOE Attack";
		strongAOEAttack.dirModifier = new Vector3 (0, 0, 1);
		strongAOEAttack.dmgModifier = 1f;
		strongAOEAttack.forceModifier= 1f;
		strongAOEAttack.preHitDelay = 0.5f;
		strongAOEAttack.postHitDelay = 0.4f;
		strongAOEAttack.hitDuration = 0.3f;
		strongAOEAttack.hasPlayerControl = false;
		strongAOEAttack.isAOE = true;
		strongAOEAttack.hitBox = hbx.HitBox_AOE_Big;
		strongAOEAttack.fxName = "AOESmokeFX";	

		AttackChain strongAOEAttackChain = new AttackChain (5);
		strongAOEAttackChain.AddAttack (strongAOEAttack);
		AddAttackChain (strongAOEAttackChain);


		// airborne : Strong airborne attack
		MovementAttack strongSlidingAttack_Air = new MovementAttack ();
		strongSlidingAttack_Air.dirModifier = new Vector3 (0, 1, 2);
		strongSlidingAttack_Air.dmgModifier = 0.5f;
		strongSlidingAttack_Air.forceModifier = 0.5f;
		strongSlidingAttack_Air.preHitDelay = 0;
		strongSlidingAttack_Air.postHitDelay = 0.1f;
		strongSlidingAttack_Air.hitDuration = 0.5f;
		strongSlidingAttack_Air.hasPlayerControl = false;
		strongSlidingAttack_Air.isAOE = false;
		strongSlidingAttack_Air.hitBox = hbx.HitBox_Front_Big;
		strongSlidingAttack_Air.fxName = null;
		strongSlidingAttack_Air.moveDir = new Vector3 (9, 9, 9); //Direction (9,9,9) signifies along "current Mouselook"
		strongSlidingAttack_Air.moveSpeed = 30f;
		strongSlidingAttack_Air.moveDist = 10f;
		
		AttackChain strongSlidingAttack_AirChain = new AttackChain (6);
		strongSlidingAttack_AirChain.AddAttack (strongSlidingAttack_Air);
		AddAttackChain (strongSlidingAttack_AirChain);


		/*
		 * DODGING: CONTROL KEY (CTRL) 
		 */
		
		
		// + nothing/w : Directional block
		
		
		// + s : Dodge backwards
		
		
		// + a/d : Dodge sideways
		
		
		// airborne : Air dodge



		/*
		 * SPECIALS: SHIFT KEY (SHIFT) 
		 */




		//Basic Attack
//		AttackData basic = new AttackData ();
//		basic.attackID = 0;
//		basic.attackName = "BasicAttack";
//		basic.forceModifier = 1f;
//		basic.dirModifier = new Vector3 (0, 5, 10);//.forward;
//		basic.dmgModifier = 1f;
//		basic.hitBox = hbx.HitBox_Front_Big;
//		basic.preHitDelay = 0.2f;
//		basic.hitDuration = 0.1f;
//		basic.postHitDelay = 0.2f; 
//		AddAttack (basic);
//
//		//Uppercut Attack
//		AttackData uppercut = new AttackData ();
//		uppercut.attackID = 1;
//		uppercut.attackName = "Uppercut";
//		uppercut.forceModifier = 0.5f;
//		uppercut.dirModifier = Vector3.up;
//		uppercut.dmgModifier = 3f;
//		uppercut.hitBox = hbx.HitBox_Front_High_Medium;
//		AddAttack (uppercut);
//
//		//Forward Smash Attack
//		AttackData fwdSmash = new AttackData ();
//		fwdSmash.attackID = 2;
//		fwdSmash.attackName = "ForwardSmash";
//		fwdSmash.forceModifier = 5f;
//		fwdSmash.dirModifier = Vector3.forward;
//		fwdSmash.dmgModifier = 3f;
//		fwdSmash.hitBox = hbx.HitBox_Front_Big;
//		AddAttack (fwdSmash);





	}
	
}

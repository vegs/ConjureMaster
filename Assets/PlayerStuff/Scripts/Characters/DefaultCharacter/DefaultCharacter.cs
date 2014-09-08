using UnityEngine;
using System.Collections;

public class DefaultCharacter : CharacterData {

	HitBoxes hbx = GameObject.FindObjectOfType<HitBoxes>();
	FXPrefabs fx = GameObject.FindObjectOfType<FXPrefabs>();
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
		BasicAttack weakAttack = new BasicAttack (	    
			"Weak Attack",		    		//string attackName
			new Vector3 (0, 0, 1),			//Vector3 dirModifier
			1f,		    					//float dmgModifier
			0.01f,		    				//float forceModifier
			0.12f,		    				//float preHitDelay
			0.12f,		    				//float postHitDelay
			0.1f,		    				//float hitDuration
			false,		    				//bool hasPlayerControl
			false,		    				//bool isAOE
			hbx.HitBox_Front_Big,			//GameObject hitBox
			null);							//GameObject visualFX
			


		AttackChain weakAttackChain = new AttackChain (0);
		weakAttackChain.AddAttack (weakAttack);
		weakAttackChain.AddAttack (weakAttack);
		AddAttackChain (weakAttackChain);


		
		// + w : Weak sliding tackle attack


		// + a/d : Weak AOE attack
		BasicAttack weakAOEAttack = new BasicAttack(    
			"Weak AOE Attack",		    	//string attackName
			new Vector3 (0, 0, 1),			//Vector3 dirModifier
			1f,		    					//float dmgModifier
			0.5f,		    				//float forceModifier
			0.1f,		    				//float preHitDelay
			0.1f,		    				//float postHitDelay
			0.41f,		    				//float hitDuration
			true,		    				//bool hasPlayerControl
			true,		    				//bool isAOE
			hbx.HitBox_AOE_Big,				//GameObject hitBox
			null);							//GameObject visualFX
		
		AttackChain weakAOEAttackChain = new AttackChain (1);
		weakAOEAttackChain.AddAttack (weakAOEAttack);
		AddAttackChain (weakAOEAttackChain);



		// airborne : Weak airborne AOE attack
		BasicAttack weakAOEAttack_Air = new BasicAttack(	    
			"Weak Airborne AOE Attack",		//string attackName
			new Vector3 (0, 0, 1),			//Vector3 dirModifier
			1f,		    					//float dmgModifier
			0.5f,		    				//float forceModifier
			0.2f,		    				//float preHitDelay
			0.2f,		    				//float postHitDelay
			0.1f,		    				//float hitDuration
			true,		    				//bool hasPlayerControl
			true,		    				//bool isAOE
			hbx.HitBox_AOE_Air,				//GameObject hitBox
		    null);							//GameObject visualFX
		
		AttackChain weakAOEAttack_AirChain = new AttackChain (2);
		weakAOEAttack_AirChain.AddAttack (weakAOEAttack_Air);
		AddAttackChain (weakAOEAttack_AirChain);



		/*
		 * STRONG ATTACKS: RIGHT MOUSE BUTTON (RMB) 
		 */

		// + nothing/s : Smash
		BasicAttack fwdSmash = new BasicAttack (	    
			"Weak Attack",		    		//string attackName
			new Vector3 (0, 1, 2),			//Vector3 dirModifier
			1f,		    					//float dmgModifier
			1f,		    					//float forceModifier
			0.3f,		    				//float preHitDelay
			0.3f,		    				//float postHitDelay
			0.2f,		    				//float hitDuration
			false,		    				//bool hasPlayerControl
			false,		    				//bool isAOE
			hbx.HitBox_Front_Big,			//GameObject hitBox
			null);							//GameObject visualFX
		
		AttackChain fwdSmashChain = new AttackChain (3);
		fwdSmashChain.AddAttack (fwdSmash);
		AddAttackChain (fwdSmashChain);

		
		// + w : Strong charge attack
		

		// + a/d : Strong AOE attack
		BasicAttack strongAOEAttack = new BasicAttack(	    
			"Strong AOE Attack",		//string attackName
			new Vector3 (0, 0, 1),			//Vector3 dirModifier
			1f,		    					//float dmgModifier
			1f,			    				//float forceModifier
			0.5f,		    				//float preHitDelay
			0.4f,		    				//float postHitDelay
			0.3f,		    				//float hitDuration
			false,		    				//bool hasPlayerControl
			true,		    				//bool isAOE
			hbx.HitBox_AOE_Big,				//GameObject hitBox
		    fx.AOE_Slam);					//GameObject visualFX
		
		AttackChain strongAOEAttackChain = new AttackChain (4);
		strongAOEAttackChain.AddAttack (strongAOEAttack);
		AddAttackChain (strongAOEAttackChain);


		// airborne : Strong airborne attack



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

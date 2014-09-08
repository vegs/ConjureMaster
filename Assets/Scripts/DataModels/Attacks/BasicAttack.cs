using UnityEngine;
using System.Collections;

public class BasicAttack : AttackData {



	public BasicAttack (
	             string attackName,
	             Vector3 dirModifier,
	             float dmgModifier,
	             float forceModifier,
	             float preHitDelay,
	             float postHitDelay,
	             float hitDuration,
	             bool hasPlayerControl,
	             bool isAOE,
	             GameObject hitBox,
				 GameObject visualFX)
	{
		this.attackName = attackName;
		this.dirModifier = dirModifier; 
		this.dmgModifier = dmgModifier;
		this.forceModifier = forceModifier;
		this.preHitDelay = preHitDelay;
		this.postHitDelay = postHitDelay;
		this.hitDuration = hitDuration;
		this.hasPlayerControl = hasPlayerControl;
		this.isAOE = isAOE;
		this.hitBox = hitBox;
		this.visualFX = visualFX;
	}
}

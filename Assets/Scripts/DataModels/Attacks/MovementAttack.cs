using UnityEngine;
using System.Collections;

public class MovementAttack : AttackData {

	Vector3 moveDir;
	float moveSpeed;
	float moveDist;

	public MovementAttack (
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
	             Vector3 moveDir,
	             float moveSpeed,
	             float moveDist)
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
		this.moveDir = moveDir;
		this.moveSpeed = moveSpeed;
		this.moveDist = moveDist;
	}
}

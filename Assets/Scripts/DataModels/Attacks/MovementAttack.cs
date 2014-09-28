using UnityEngine;
using System.Collections;

public class MovementAttack : AttackData {

	public Vector3 moveDir = new Vector3 (0, 0, 1) ;
	public float moveSpeed = 20f;
	public float moveDist = 10f;

	public MovementAttack (){

	}

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
				 string fxName,
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
		this.fxName = fxName;
		this.moveDir = moveDir;
		this.moveSpeed = moveSpeed;
		this.moveDist = moveDist;
	}
}

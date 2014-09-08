using UnityEngine;
using System.Collections.Generic;

public class AttackChain {
	public int id;
	public List<AttackData> subAttacks = new List<AttackData>();
	public List<GameObject> attackChainHitBoxes = new List<GameObject>();

	public AttackChain(int id){
		this.id = id;
	}

	public void AddAttack (AttackData attack){
		subAttacks.Add(attack);
	}
}

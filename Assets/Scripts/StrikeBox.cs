using UnityEngine;
using System.Collections;

public class StrikeBox : MonoBehaviour {
	PlayerMovement pm = null;
	PhotonView pv = null;
	BotMovement bm = null; 
	PlayerState ps = null;
	Health h = null;

	void OnTriggerEnter(Collider collision) {

		pv = collision.GetComponent<PhotonView>();

		if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bot") && pv != null ){
			//Debug.Log ("Hit comething!" + collision.name);	

			//pm = collision.GetComponent<PlayerMovement> ();
			//bm = collision.GetComponent<BotMovement> ();
			ps = collision.GetComponent<PlayerState> ();
			h = collision.GetComponent<Health> ();
			//player.enabled = true;
			//CharacterController nm = collision.GetComponent<CharacterController> ();
			//Vector3 direction = this.transform.forward + (this.transform.up) * 0.5f; //this is supposed to be the direction in attack data
			PlayerCombat myChar = GetComponentInParent<PlayerCombat> ();
			Character enemyChar = collision.GetComponent<Character> ();
			//float attack = 10f;//myChar.currentAttack;

		

			//Debug.Log (direction);//* myChar.currentAttack.dirModifier.z;//transform.rotation * myChar.currentAttack.dirModifier; 
			float attackForce = myChar.thisChar.attackForce * myChar.currentAttack.forceModifier; 
			float attackDamage = myChar.thisChar.attackPower * myChar.currentAttack.dmgModifier;
			//not my player
			if (!collision.GetComponent<PhotonView>().isMine || collision.GetComponent<PhotonView>().isSceneView || collision.gameObject.tag == "Bot"){
				if ( ps != null ) {
					Vector3 direction = Vector3.zero;
					if ( myChar.currentAttack.isAOE ){
						direction = (enemyChar.transform.position - this.transform.position).normalized;
					} else {
						direction = this.transform.rotation * myChar.currentAttack.dirModifier; //GetComponentInParent<Transform>().forward;
					}

					Debug.Log (direction);
					//ps.Hit (direction, attackForce, this.transform.position);
					//h.TakeDamage(attackDamage);
					pv.RPC ("Hit", PhotonTargets.AllBuffered, direction, attackForce, this.transform.position);
					pv.RPC ("TakeDamage", PhotonTargets.AllBuffered, attackDamage);
					//player.Hit( direction, strength);
				}
			}

		}
		else{
			Debug.Log ( "Not a player" );
		}

	}
}



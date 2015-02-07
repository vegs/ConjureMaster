using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Health : MonoBehaviour {

	public float damagePercent = 0;
	float currentHitPoints;
	NetworkManager_GAME nm;



	void Awake () {
		nm = GameObject.FindObjectOfType<NetworkManager_GAME>();
	}

	void Update()
	{

	}
	
	[RPC]
	public void TakeDamage(float attackStr) {
		damagePercent += attackStr;


		Debug.Log ("Damage" + damagePercent);
	}

	[RPC]
	public void Die() {
		if( GetComponent<PhotonView>().instantiationId==0 ) {
			Destroy(gameObject);
		}
		else {
			if( GetComponent<PhotonView>().isMine ) {
				if( gameObject.tag == "Player" ) {		// This is my actual PLAYER object, then initiate the respawn process

					//nm.standbyCamera.SetActive(true);
					//nm.mainCamera.SetActive(false);
					nm.LoseLife();

					if ((int)PhotonNetwork.player.customProperties["Lives"] > 0){
						nm.respawnTimer = 3f;
					}else{
						nm.Defeat();
					}

					nm.justDied = true;
					nm.deathWatch = 2f;
					nm.DeathPos = this.transform.position;
					nm.DeathRot = this.transform.rotation;
					nm.DeathVel = this.GetComponent<PlayerState> ().velocity;
				}

				PhotonNetwork.Destroy(gameObject);
			}
		}
	}
}
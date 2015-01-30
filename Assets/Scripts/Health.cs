using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float damagePercent = 0;
	float currentHitPoints;
	public int lives;
	NetworkManager_GAME nm;



	void Awake () {
		nm = GameObject.FindObjectOfType<NetworkManager_GAME>();

		if (PhotonNetwork.room != null) {
			lives = nm.lives;
		}
	}
	void OnJoinedRoom(){
		lives = (int)PhotonNetwork.room.customProperties["Lives"];
	}

	void Update()
	{
		PhotonNetwork.player.customProperties ["Damage"] = damagePercent;
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
					nm.lives = nm.lives - 1;
					Debug.Log("Lives: "+nm.lives);
					if (nm.lives > 0){
						nm.respawnTimer = 3f;
					}

					nm.justDied = true;
					nm.deathWatch = 1f;
					nm.DeathPos = this.transform.position;
				}

				PhotonNetwork.Destroy(gameObject);
			}
		}
	}
}
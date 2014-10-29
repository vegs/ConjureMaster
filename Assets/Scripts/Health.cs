using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	//public float hitPoints = 100f;
	public float damagePercent = 0;
	float currentHitPoints;
//
//	public string userName = "Please enter username";
//	private GUIText[] labelContent;

	
	// Use this for initialization
	void Start () {
		//currentHitPoints = hitPoints;
	}
	
	[RPC]
	public void TakeDamage(float attackStr) {
		damagePercent += attackStr;

		Debug.Log ("Damage" + damagePercent);
//		if(currentHitPoints <= 0) {
//			Die();
//		}
	}
	
	void OnGUI() {
		if( GetComponent<PhotonView>().isMine && gameObject.tag == "Player" ) {
			if( GUI.Button(new Rect (Screen.width-100, 0, 100, 40), "Suicide!") ) {
				Die ();
			}

//			userName = GUI.TextField(new Rect(10, 10, 200, 20), userName, 25);
//
//			if( GUI.Button(new Rect (10, 30, 150, 30), "Enter username") ) {
//
//				Debug.LogError("CalledButton");
//
//				// THIS DOES NOT WORK!!!!!
//				labelContent = this.GetComponentsInChildren<GUIText>();
//				labelContent[0].text = "t";
//				//labelContent.text = userName;
//				// TIL HIT!
//			}


		}
	}
	[RPC]
	public void Die() {
		if( GetComponent<PhotonView>().instantiationId==0 ) {
			Destroy(gameObject);
		}
		else {
			if( GetComponent<PhotonView>().isMine ) {
				if( gameObject.tag == "Player" ) {		// This is my actual PLAYER object, then initiate the respawn process
					NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();
					
					nm.standbyCamera.SetActive(true);
					nm.mainCamera.SetActive(false);

					nm.respawnTimer = 3f;
				}
				
				PhotonNetwork.Destroy(gameObject);
			}
		}
	}
}
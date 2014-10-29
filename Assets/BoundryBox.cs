using UnityEngine;
using System.Collections;

public class BoundryBox : MonoBehaviour {

	PlayerMovement pm = null;
	PhotonView pv = null;
	BotMovement bm = null; 
	PlayerState ps = null;

	void OnTriggerExit(Collider collision) {
		pv = collision.GetComponent<PhotonView>();

		if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bot") && pv != null) {
			pv.RPC ("Die", PhotonTargets.AllBuffered);
		}
	}
}
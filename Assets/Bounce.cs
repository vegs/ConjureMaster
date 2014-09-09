using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {
	PlayerState ps;

	void Start () {
		ps = GetComponent<PlayerState>();
	}
	// Use this for initialization
	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.gameObject.tag == "Terrain"){
			Debug.DrawRay(hit.collider.transform.position -new Vector3(10,10,10) , hit.normal ,Color.yellow) ;
		}
		Debug.DrawRay(gameObject.transform.position , hit.normal ,Color.yellow) ;
		Debug.Log (hit.normal);
		if (hit.normal.y < 0.5f) {
			ps.Reflect(hit.normal, 0.75f);
		}
		//Debug.Log (hit.gameObject.tag);
		//}
		//hit.gameObject.
	}
}

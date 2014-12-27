using UnityEngine;
using System.Collections;

public class WallCheck : MonoBehaviour {
	PlayerState ps;
	public bool wallHug = false;
	//enum dir {none, forward, backwards, left, right};
	public int wallDir = 0; 

	void Start () {
		ps = this.GetComponentInParent<PlayerState>();
	}
	// Use this for initialization
	void OnTriggerStay(Collider collider){
		if (collider.gameObject.tag == "Terrain") {
			wallHug=true;

			Ray rayF = new Ray(this.transform.position, this.transform.forward);
			Ray rayB = new Ray(this.transform.position, -this.transform.forward);
			Ray rayL = new Ray(this.transform.position, -this.transform.right);
			Ray rayR = new Ray(this.transform.position, this.transform.right);

			RaycastHit hit;
			if(collider.Raycast(rayF, out hit, 10f)){
				ps.PushBack (hit.normal);
				wallDir=1;
			}else if(collider.Raycast(rayB, out hit, 10f)){
				ps.PushBack (hit.normal);
				wallDir=2;
			}else if(collider.Raycast(rayL, out hit, 10f)){
				ps.PushBack (hit.normal);
				wallDir=3;
			}else if(collider.Raycast(rayR, out hit, 10f)){
				ps.PushBack (hit.normal);
				wallDir=4;
			}

		} else {
			wallHug=false;
			//wallDir=0;
		}
	}
}

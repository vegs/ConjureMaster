using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float moveSpeed = 10.0f;
	float wallCheckDist=1.6f;
	Vector3 corr=new Vector3(0,2,0);

	Vector3 direction = Vector3.zero;  //forward/back left/right
	
	private Vector3 _velocity = Vector3.zero;
	public Vector3 velocity{
		get { return _velocity; }
	}

	WallCheck wc;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim=GetComponent<Animator>();
		wc = GetComponentInChildren<WallCheck> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 hAxis = new Vector3 (Input.GetAxis ("Horizontal"),0,0);
		Vector3 vAxis = new Vector3 (0, 0, Input.GetAxis ("Vertical"));
		
		RaycastHit hit;
			
		//Debug.DrawRay(this.transform.position+corr, this.transform.forward*wallCheckDist, Color.red);

		if(Physics.Raycast(this.transform.position+corr, this.transform.forward, out hit, wallCheckDist) && hit.collider.tag == "Terrain"){
			if (vAxis.z>0) vAxis.z = 0;
		}
		if(Physics.Raycast(this.transform.position+corr, -this.transform.forward, out hit, wallCheckDist) && hit.collider.tag == "Terrain"){
			if (vAxis.z<0) vAxis.z = 0;
		}
		if(Physics.Raycast(this.transform.position+corr, -this.transform.right, out hit, wallCheckDist) && hit.collider.tag == "Terrain"){
			if (hAxis.x>0) vAxis.z = 0;
		}
		if(Physics.Raycast(this.transform.position+corr, this.transform.forward, out hit, wallCheckDist) && hit.collider.tag == "Terrain"){
			if (hAxis.x<0) vAxis.z = 0;
		}

		direction =  ( transform.rotation * (hAxis+vAxis) );

		if( direction.magnitude > 1f){
			direction = direction.normalized; 
		}

		_velocity = direction * moveSpeed;

		anim.SetFloat ("Speed", direction.magnitude);

	}
}

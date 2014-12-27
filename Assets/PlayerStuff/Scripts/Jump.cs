using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {
	Animator anim;
	PlayerState ps;
	CharacterController cc;
	PlayerMovement pm;
	WallCheck wc;

	bool canJumpAir = true;
	bool falling = true; //falling off a ldge without jumping 
	float airTime = 0.0f;

	public float jumpSpeed = 10.0f;
	public float jumpDelay = 0.5f;

	Collider someCollider = null;
		

	// Use this for initialization
	void Start () {
		anim=GetComponent<Animator>();
		ps=GetComponent<PlayerState>();
		cc=GetComponent<CharacterController>();
		pm = GetComponent<PlayerMovement>();
		wc = GetComponentInChildren<WallCheck> ();
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = new Ray(gameObject.transform.position+new Vector3(0,1,0), Vector3.down);
		//Ray fallCheckRay = new Ray (gameObject.transform.position, -transform.up);
//		if (Physics.Raycast (fallCheckRay, 1.5f)) {
//			anim.SetBool ("IsSlope", true);
//		} else {
//			anim.SetBool ("IsSlope", false);
//		} 
		RaycastHit hit;
		Debug.DrawRay(gameObject.transform.position+new Vector3(0,1,0), Vector3.down*2.5f, Color.magenta);
		//Physics.Raycast (transform.position, -transform.up, 1
//		Debug.Log (Physics.Raycast(ray, out hit, 2.5f) && hit.collider.tag!="Player");
		if ( cc.isGrounded && wc.wallHug == false || Physics.Raycast(ray, out hit, 2.5f) && hit.collider.tag!="Player") {
			airTime=0;
			canJumpAir=true;
			falling=true;
			anim.SetBool ("IsSlope", true);
			
			if ( Input.GetButtonDown ("Jump") ) {
				ps.Jump(jumpSpeed);
				falling=false;
			}
		}

		else {
			airTime += Time.deltaTime;
			anim.SetBool ("IsSlope", false);
			
			if ( Input.GetButtonDown ("Jump") && canJumpAir && (falling || airTime > jumpDelay)) {
				canJumpAir=false;
				ps.Jump(jumpSpeed);
				anim.SetBool("DoubleJump", true);

			}
		}
	}

	void FixedUpdate(){



		if (ps.velocity.y < 0) {
			anim.SetBool ("Falling", true);
			
			if ( cc.isGrounded) {
				anim.SetBool("Grounded", true);
				anim.SetBool ("Jump", false);
				anim.SetBool("DoubleJump", false);
				//ps.velocity.y = Physics.gravity.y * Time.deltaTime;
			} else {
				anim.SetBool("Grounded", false);
				
			}
		} else {
			if ( ps.velocity.y > jumpSpeed*0.5f ){
				anim.SetBool ("Jump", true);
			}
			anim.SetBool ("Falling", false);

			if (!cc.isGrounded) {
				anim.SetBool("Grounded", false);
			}
		}
	}
}

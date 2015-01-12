using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {

	CharacterController cc;
	Animator anim;
	PlayerMovement pm;
	PlayerCombat pc;
	MouseLook ml;
	PhotonView pv;
	GamePhysics phys = new GamePhysics();
	enum dir {none, forward, backwards, left, right};

	public float flinchThreshold = 5f;
	public float flinch_knockoutTime = 0.5f;

	private float knockoutTime = 0;
	private bool stunned = false;
	private bool playerControl = true;
	private Vector3 _velocity = Vector3.zero;
	public Vector3 velocity{
		get { return _velocity; }
	}


	Vector3 dist = Vector3.zero;


	Vector3 moveVelocity = Vector3.zero;
	Vector3 jumpVelocity = Vector3.zero;
	Vector3 hitVelocity = Vector3.zero;
	Vector3 addedVelocity = Vector3.zero;

	FXManager fx = null;


	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		pm = GetComponent<PlayerMovement>();
		pc = GetComponent<PlayerCombat>();
		ml = GetComponent<MouseLook> ();
		pv = GetComponent<PhotonView> ();
		fx = GameObject.FindObjectOfType<FXManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	if(pv.isMine){
		if(ml != null) ml.playerControl = playerControl;

		knockoutTime -= Time.deltaTime;

		if (knockoutTime <= 0) {
			playerControl=true;
			knockoutTime=0;
		} else {
			playerControl = false;
		}

		if (pc.isUsingAttack && pc.currentAttack != null && !pc.currentAttack.hasPlayerControl) {
			playerControl = false;	
		} else {
			playerControl = true;		
		}
		if (pc.isUsingAttack && pc.currentAttack is MovementAttack) {
			addedVelocity=Vector3.zero;
		}
	}
	}

	// FixedUpdate is called once per physics loop
	// Do all movement and physics here
	void FixedUpdate(){
	if(pv.isMine){
		dist = Vector3.zero;

		 
		if (playerControl && pm != null){
			moveVelocity = pm.velocity;
			addedVelocity = phys.AddControlFriction(addedVelocity);
		} else {
			moveVelocity = Vector3.zero;
		}
	


		if (pc.isUsingAttack && pc.currentAttack is MovementAttack && pc.isDangerous) {
			MovementAttack currentAttack=(MovementAttack)pc.currentAttack;
			_velocity = this.transform.rotation * currentAttack.moveDir.normalized * currentAttack.moveSpeed;
		} else {
			_velocity = moveVelocity + addedVelocity;

			if (cc.isGrounded) {
				if (_velocity.y < 0) {
					addedVelocity.y = (Physics.gravity.y * Time.deltaTime);
					knockoutTime = 0;
				}
				addedVelocity = phys.AddFriction (addedVelocity);

			} else { 
				addedVelocity = phys.AddGravity (addedVelocity);
				addedVelocity = phys.AddDrag (addedVelocity);
			}
		}
		dist = _velocity * Time.deltaTime;

//		Debug.Log (dist*100);
		cc.Move (dist);


		/*
		 * Setting Animation Variables
		 */
		if (knockoutTime > 0) {
			anim.SetBool ("IsHit", true);		
		} else {
			anim.SetBool ("IsHit", false);
		}
		//Flinch

	}
	}

	[RPC]
	public void Hit(Vector3 hitDir, float hitForce, Vector3 attackerPos){

		if (pc != null) {
			pc.Interrupt();
		}

		hitForce = hitForce * ( 1 + GetComponent<Health>().damagePercent/100);
		hitVelocity = hitForce * hitDir.normalized;
		if (hitForce < flinchThreshold) {
			//flinch animation
			anim.SetBool("Flinch", true);
			AnimatorStateInfo flinchAnim =  anim.GetCurrentAnimatorStateInfo(0);
			anim.Play(flinchAnim.nameHash,0, 0); 
			knockoutTime = flinch_knockoutTime; //flinch knockout time

			//display flinch hit-stars
			fx.GetComponent<PhotonView>().RPC ("PlayFX", PhotonTargets.All, "FlinchHit" ,this.transform.position, attackerPos);

		} else {
			anim.SetBool("Flinch", false);
			addedVelocity += hitVelocity;

			// Knock Back angle to determine which knockback-animation to play
			float kncBckTheta = Mathf.Atan2(hitDir.x,hitDir.z);
			anim.SetFloat("KnckBckTheta", kncBckTheta);

			knockoutTime = 1f; //should depend on the attack force ideally

			//display hit-star
			fx.GetComponent<PhotonView>().RPC ("PlayFX", PhotonTargets.All, "NormalHit" ,this.transform.position, attackerPos);

			//display trailing smoke
			fx.GetComponent<PhotonView>().RPC ("PlayFX", PhotonTargets.All, "TrailingSmoke" ,this.transform.position, this.transform.position);

		}

		Debug.Log ("Added: " + hitVelocity);


	}

	public void Jump(float jmpSpeed){
		if(playerControl){
			addedVelocity.y = jmpSpeed;
		}
	}

	public void PushBack(Vector3 normal){

		addedVelocity.x = normal.x *5f;
		addedVelocity.z = normal.z *5f;

	}

	public void Reflect(Vector3 normal, float velocityMultiplyer){
		_velocity = velocityMultiplyer * (_velocity - 2 * Vector3.Dot(_velocity, normal) * normal);
		addedVelocity = velocityMultiplyer * (addedVelocity - 2 * Vector3.Dot(addedVelocity, normal) * normal);

		float kncBckTheta = Mathf.Atan2(_velocity.x,_velocity.z);
		anim.SetFloat("KnckBckTheta", kncBckTheta);

		if(anim.GetBool ("IsHit") && !anim.GetBool("Flinch")){

			//display hit-stars when bouncing off terrain
			float normalMag = Vector3.Dot(_velocity, normal);
			if(normalMag < 8 && normalMag > 2) {
				fx.GetComponent<PhotonView>().RPC ("PlayFX", PhotonTargets.All, "FlinchHit" ,this.transform.position, this.transform.position);
			} else if (normalMag > 8) {
				fx.GetComponent<PhotonView>().RPC ("PlayFX", PhotonTargets.All, "NormalHit" ,this.transform.position, this.transform.position);
			}


		}
	}


}
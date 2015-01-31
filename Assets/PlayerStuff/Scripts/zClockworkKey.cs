using UnityEngine;
using System.Collections;

public class zClockworkKey : MonoBehaviour {

	Animator anim;

	int fastSpeed = 500;
	int medSpeed = 300;
	int slowSpeed = 100;


	// The process can probably be more efficient using hashes instead!
	// So look at this at later stage - when optimising
	//////////////////////////////////////////////////////////////////
//	int Run_hash = Animator.StringToHash("Run");
//	int Jump_hash = Animator.StringToHash("Jump");
//	int DblJump_hash = Animator.StringToHash("DoubleJump");
//	int KnckBcn_hash = Animator.StringToHash("KnockBackb");
//	int AtWeak_hash = Animator.StringToHash("Weak");
//	int AtWeak2_hash = Animator.StringToHash("Weak2");
//	int AtWSlide_hash = Animator.StringToHash("WeakSlidingAttack");
//	int AtWAOE_hash = Animator.StringToHash("WeakAOE");
//	int AtSmash_hash = Animator.StringToHash("SmashFwd");
//	int AtSAOE_hash = Animator.StringToHash("StrongAOE");

	// Use this for initialization
	void Start () {

		if (transform.parent != null /*&& (transform.parent.parent.parent.parent.parent.parent.parent.tag == "Player")*/){
			Debug.Log("zKey found its parent!");
			anim = transform.parent.parent.parent.parent.parent.parent.parent.GetComponent<Animator>();	
		}
		else{
			Debug.LogError("zKey failed to find parent");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(anim != null){

		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

		if (stateInfo.IsName("Run")||
		    stateInfo.IsName("Jump")||
		    stateInfo.IsName("DoubleJump")||
		    stateInfo.IsName("KnockBackb")||
		    stateInfo.IsName("Weak")||
		    stateInfo.IsName("Weak2")||
		    stateInfo.IsName("WeakSlidingAttack")||
		    stateInfo.IsName("WeakAOE")){

			transform.Rotate (0,0,Time.deltaTime*medSpeed);
		}
		else if (stateInfo.IsName("SmashFwd")||		    
		         stateInfo.IsName("StrongAOE")){

			transform.Rotate (0,0,Time.deltaTime*fastSpeed);
		}
		else {
		transform.Rotate (0,0,Time.deltaTime*slowSpeed);
		}

		}
	}
}

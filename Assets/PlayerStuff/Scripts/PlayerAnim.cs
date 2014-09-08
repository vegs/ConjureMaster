//using UnityEngine;
//using System.Collections;
//
//public class PlayerAnim : MonoBehaviour {
//	public Animator anim;
//	int idleStateHash=Animator.StringToHash("Base Layer.Idle");
//	int attackStateHash=Animator.StringToHash("Base Layer.BasicAttack");
//	float delay;
//	// Use this for initialization
//	void Start () {
//		anim=GetComponent<Animator>();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo (0);
//
//		if (stateInfo.nameHash == idleStateHash) {
//			anim.SetBool("attack",false);
//			//Debug.Log ("Test");
//		}
//
//		if (Input.GetButtonDown ("Fire1")) 
//		{
//			Debug.Log ("FIRE");
//			anim.SetBool("attack",true);
//			//delay=Time.time+1;
//		}
//	}
//}

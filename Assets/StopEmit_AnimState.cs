using UnityEngine;
using System.Collections;

public class StopEmit_AnimState : MonoBehaviour {

	public int DelayFrames = 5;

	Animator anim;
	bool SetAnim = false;
	ParticleSystem ParSys;
	float timer = 0;

	// Use this for initialization
	void Start () {
		ParSys = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

		timer = timer + Time.deltaTime;

			if(!SetAnim){
				anim = transform.parent.parent.GetComponent<Animator>();
				SetAnim = true;
			}

			if (SetAnim){

				AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			
				if (!stateInfo.IsName("KnockBackb") && !anim.IsInTransition(0) && timer > DelayFrames * Time.deltaTime){
				//Debug.Log("Now, stop emitting!");
				ParSys.Stop();
				}
			}

		}


}

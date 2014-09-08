using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {
	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	Animator anim;
	//float lastUpdateTime;
	// Use this for initialization
	void Start () {
		anim=GetComponent<Animator>();
	}

	void CacheComponents () {
		if (anim == null) {
			anim=GetComponent<Animator>();	
			if (anim == null) {
				Debug.LogError("This character has no Animator component");
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
				
		} else {
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.05f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.05f);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		CacheComponents();
		if (stream.isWriting) {
			//This is our player
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(anim.GetFloat("Speed"));
			stream.SendNext(anim.GetBool("Jump"));
			stream.SendNext(anim.GetBool("Grounded"));
			stream.SendNext(anim.GetBool("Falling"));
			stream.SendNext(anim.GetBool("IsSlope"));
			stream.SendNext(anim.GetBool("DoubleJump"));

			stream.SendNext(anim.GetBool("IsAttacking"));
			stream.SendNext(anim.GetInteger("AttackChainID"));
			stream.SendNext(anim.GetInteger("AttackID"));
			stream.SendNext(anim.GetBool("IsHit"));
			stream.SendNext(anim.GetBool("Flinch"));




				
		}else{
			//This is someone elses player. We need to receive their position and update our version of that player
			realPosition = (Vector3) stream.ReceiveNext();
			realRotation = (Quaternion) stream.ReceiveNext();

			anim.SetFloat("Speed" , (float) stream.ReceiveNext());
			anim.SetBool("Jump" , (bool) stream.ReceiveNext());
			anim.SetBool("Grounded" , (bool) stream.ReceiveNext());
			anim.SetBool("Falling" , (bool) stream.ReceiveNext());
			anim.SetBool("IsSlope", (bool) stream.ReceiveNext());
			anim.SetBool("DoubleJump", (bool) stream.ReceiveNext());

			anim.SetBool("IsAttacking", (bool) stream.ReceiveNext());
			anim.SetInteger("AttackChainID", (int) stream.ReceiveNext());
			anim.SetInteger("AttackID", (int) stream.ReceiveNext());
			anim.SetBool("IsHit", (bool) stream.ReceiveNext());
			anim.SetBool("Flinch", (bool) stream.ReceiveNext());


			
		}
	}

}

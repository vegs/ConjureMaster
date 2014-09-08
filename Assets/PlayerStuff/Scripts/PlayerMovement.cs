using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float moveSpeed = 10.0f;

	Vector3 direction = Vector3.zero;  //forward/back left/right
	
	private Vector3 _velocity = Vector3.zero;
	public Vector3 velocity{
		get { return _velocity; }
	}
	
	Animator anim;

	// Use this for initialization
	void Start () {
		anim=GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		direction =  ( transform.rotation * new Vector3 ( Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") ));

		if( direction.magnitude > 1f){
			direction = direction.normalized; 
		}

		_velocity = direction * moveSpeed;

		anim.SetFloat ("Speed", direction.magnitude);

	}
}

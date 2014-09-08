using UnityEngine;
using System.Collections;

public class GamePhysics  {

	float drag = 1f;
	float friction = 1f;
	float controlFriction = 0.95f;
	
	// Use this for initialization
	void Start () {

	}


	public Vector3 AddGravity(Vector3 velocity){
		velocity.y += (Physics.gravity.y * Time.deltaTime);

		return velocity;
	}

	public Vector3 AddDrag(Vector3 velocity){
		Vector3 horizontalVelocity = new Vector3 (velocity.x, 0, velocity.z);
		if (horizontalVelocity.magnitude > 4f) {
			horizontalVelocity = horizontalVelocity - (horizontalVelocity * (drag) * Time.deltaTime );
		}else {
			horizontalVelocity = Vector3.zero;
		}
		horizontalVelocity.y = velocity.y;

		return horizontalVelocity;
	}

	public Vector3 AddFriction(Vector3 velocity){
		Vector3 horizontalVelocity = new Vector3 (velocity.x, 0, velocity.z);
		if (horizontalVelocity.magnitude > 4f) {
			horizontalVelocity = horizontalVelocity - (horizontalVelocity * (friction) * Time.deltaTime );
		}else {
			horizontalVelocity = Vector3.zero;
		}
		horizontalVelocity.y = velocity.y;

		return horizontalVelocity;
	}


	public Vector3 AddControlFriction(Vector3 velocity){
		Vector3 horizontalVelocity = new Vector3 (velocity.x, 0, velocity.z);
		if (horizontalVelocity.magnitude > 4f) {
			horizontalVelocity = horizontalVelocity - (horizontalVelocity * (controlFriction) * Time.deltaTime );
		}else {
			horizontalVelocity = Vector3.zero;
		}
		horizontalVelocity.y = velocity.y;

		return horizontalVelocity;
	}	
}

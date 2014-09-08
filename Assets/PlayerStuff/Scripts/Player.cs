//using UnityEngine;
//using System.Collections;
//
//public class Player : MonoBehaviour {
//	public GameObject fireHitBox;
//	public float speed=1;
//	public float sensitivityX=0.1F;
//	public float sensitivityY=0.1F;
//	float rotationX = 0F;
////	float rotationY = 0F;
//	public float groundJumpSpeed = 100.0f;
//	public float airJumpSpeed = 100.0f;
//	public float jumpDelay = 0.5f;
//	bool canJumpAir = true;
//	bool falling = true;
//	float jumpTime = 0.0f;
//	float airTime = 0.0f;
//	float hitAnimTime;
//	Quaternion originalRotation;
//	
//
//	void Start(){
//		originalRotation = transform.localRotation;
//	}
//
//	void Update(){
//		if (!Physics.Raycast (transform.position, -transform.up, 1)) {
//			airTime += Time.deltaTime;
//		} else {
//			airTime=0;
//			canJumpAir=true;
//			falling=true;
//		}
//	
//
//		rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, 0);
//
//		rotationX += Input.GetAxis("Mouse X") * sensitivityX;
//		//rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//
//		Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
//		//Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
//		
//		transform.localRotation = originalRotation * xQuaternion; // * yQuaternion;
//
////		rigidbody.rotation = new Quaternion (rigidbody.rotation.x + rotationX,
////		                                              rigidbody.rotation.y + rotationY,
////		                                              rigidbody.rotation.z);
//
//		//float amountToMove = movementSpeed * Time.deltaTime;
//		Vector3 movement = (Input.GetAxis("Horizontal") * -Vector3.left ) + (Input.GetAxis("Vertical") * Vector3.forward);
//		rigidbody.AddForce(movement, ForceMode.Force);
//		
//		if (Input.GetButtonDown ("Jump"))				
//		{
//			//print ("time: " +Time.time);
//			float stop=0.0f;
//			//print ("airTime: " + airTime);
//			//print ("canJumpAir: " + canJumpAir);
//			//print ("jumpDelay: " + jumpDelay);
//			//print ("vel1: " + rigidbody.velocity);
//			if(Physics.Raycast (transform.position, -transform.up, 1)){
//				//rigidbody.velocity.y=stop;
//				//rigidbody.velocity.Set(stop, stop, stop);
//				Debug.Log ("vel2: " + rigidbody.velocity);
//				//rigidbody.AddForce(-Vector3.up * jumpSpeed);
//				rigidbody.AddForce(Vector3.up * groundJumpSpeed);
//				canJumpAir=true;
//				falling=false;
//				//jumpTime=Time.time;
//			}else if (canJumpAir){
//				if(falling){
//					canJumpAir=false;
//					rigidbody.velocity= new Vector3(stop, stop, stop);
//					
//					Debug.Log ("vel1: " + rigidbody.velocity);
//					rigidbody.AddForce(Vector3.up * airJumpSpeed);
//				} 
//				else if(airTime > jumpDelay){
//					canJumpAir=false;
//					rigidbody.velocity= new Vector3(stop, stop, stop);
//
//					Debug.Log ("vel1: " + rigidbody.velocity);
//					rigidbody.AddForce(Vector3.up * airJumpSpeed);
//				}
//			}
//
//			//rigidbody.AddForce(Vector3.up * jumpSpeed);
//		}
//
//		
//		Debug.DrawRay (transform.position, -transform.up*1, Color.magenta);
//		
//		
//		if (Input.GetAxis("Vertical")>0){
//			rigidbody.velocity = rigidbody.velocity + transform.forward*2;
//		}
//		if (Input.GetAxis("Vertical")<0){
//			rigidbody.velocity = rigidbody.velocity -transform.forward;
//		}
//		if (Input.GetAxis("Horizontal")>0){
//			rigidbody.velocity = rigidbody.velocity + transform.right;
//		}
//		if (Input.GetAxis("Horizontal")<0){
//			rigidbody.velocity = rigidbody.velocity - transform.right;
//		}
//
//		if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.1 && Mathf.Abs (Input.GetAxis("Horizontal")) < 0.1){
//			//print("Stop!");
//			rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, 0);
//		}
//		rigidbody.velocity = new Vector3 (rigidbody.velocity.x * speed, rigidbody.velocity.y, rigidbody.velocity.z * speed);
//		//print (Input.GetAxis ("Vertical"));
//
//		//Vector3 vel = rigidbody.velocity;
//		//vel = Quaternion.Inverse(transform.rotation) * vel;
//		//vel.y = 0;
//		
//		//if (vel.sqrMagnitude>0.1f){
//		//	ani.SetInteger("walking",1);
//		//	ani.SetFloat("xvel", vel.x);
//		//	ani.SetFloat("zvel", vel.z);
//		//}else{
//		//	ani.SetInteger("walking",0);
//		//}
//	}
//}
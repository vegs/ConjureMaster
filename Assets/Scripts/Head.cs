using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour {
	public float speed=1;
	public float sensitivityX=0.1F;
	public float sensitivityY=0.1F;
//	float rotationX = 0F;
	float rotationY = 0F;
	Quaternion originalRotation;

	// Use this for initialization
	void Start () {
		originalRotation = transform.localRotation;
	
	}
	
	// Update is called once per frame
	void Update () {
		//rotationX += Input.GetAxis("Mouse X") * sensitivityX;
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY= Mathf.Clamp (rotationY, -60, 18);

//		rotationY= Mathf.Clamp (rotationY, -50, 18);
		//Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
		
		transform.localRotation = originalRotation * yQuaternion; // * yQuaternion;
		//print ("rotationY: "+rotationY);
	}
}

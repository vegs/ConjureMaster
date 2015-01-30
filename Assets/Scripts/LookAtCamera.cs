using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	public GameObject body;
	public GameObject head;


	//Smoothing and Damping
	private Vector3 velocityCamSmooth = Vector3.zero;
	[SerializeField]
	private float camSmoothDampTime = 0.1f;

	float positonModifier = 10f;

	private Vector3 _currLookDir = Vector3.zero;
	public Vector3 currLookDir{
		get { return _currLookDir; }
	}

	// Bool to determine whether the camera should move/follow or not
	public bool _autoMove = true;
	public bool autoMover{
		get { return _autoMove; }
		set { _autoMove = value; }
	}


	void LateUpdate() {
		if (body == null || head == null) {
			Debug.LogError ("No player for camera to follow") ;		
		} else {
			Vector3 targetPosition = body.transform.position;
			//Vector3 temp = new Vector3(targetPosition.x, targetPosition.y +5, targetPosition.z -7);

			float xcomp = Vector3.Dot (body.transform.forward, new Vector3 (1, 0, 0));
			float zcomp = Vector3.Dot (body.transform.forward, new Vector3 (0, 0, 1));

			float ycomp = 2 * Vector3.Dot (head.transform.forward, new Vector3 (0, 1, 0));

			Vector3 MovCor = new Vector3 (xcomp, ycomp, zcomp);
			MovCor.Normalize ();

			Vector3 temp = new Vector3(targetPosition.x - positonModifier * MovCor.x, 
			                           targetPosition.y - positonModifier * MovCor.y, 
			                           targetPosition.z - positonModifier * MovCor.z);

			//Vector3 temp = new Vector3(targetPosition.x - 7 * xcomp, 
			 //                          targetPosition.y - 7 * ycomp, 
			//                           targetPosition.z - 7 * zcomp);
			
			//Vector3 temp = new Vector3(targetPosition.x - 7 * target.transform.forward.z, 
			//                           targetPosition.y + 5 *target.transform.up.y, 
			//                          targetPosition.z - 7 * target.transform.forward.z);

			Debug.DrawRay (body.transform.position, body.transform.up*10, Color.blue);
			Debug.DrawRay (body.transform.position, body.transform.forward*10, Color.green);
			Debug.DrawRay (targetPosition, 
			               new Vector3 (targetPosition.x - positonModifier * xcomp, 
			             targetPosition.y - positonModifier * ycomp, 
			             targetPosition.z - positonModifier * zcomp), 
			               Color.cyan);


			//Vector3 derp = target.transform.forward


			// check if the camera passes through a wall, and compensate if that is the case
			CompensateForWalls(head.transform.position , ref temp);
			
			
			//transform.position = temp;
			//Vector3 targetPos = target.transform;
			//print ("camera: " + temp);

			if (_autoMove){
			smoothPosition(this.transform.position, temp);
			}
			transform.LookAt(head.transform);

			//transform.Translate(test.x, test.y +5, test.z -7);
			//transform.position.Equals(test);//.Set(test.x, test.y + 5, test.z - 7);

			_currLookDir = body.transform.position - this.transform.position;

		}

	}

	private void smoothPosition(Vector3 fromPos, Vector3 toPos)
	{
		//making a smooth transition, of this object, between one position (fromPos) and another (toPos)
		this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}

	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
	{
		Debug.DrawLine(fromObject,toTarget,Color.red );
		
		//Compensate for walls between camera
		RaycastHit wallHit = new RaycastHit();
		if(Physics.Linecast(fromObject,toTarget, out wallHit))
		{
			Debug.DrawRay(wallHit.point,Vector3.left,Color.red);
//			toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
			toTarget = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);
		}
	}

	
}

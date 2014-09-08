using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	
		[SerializeField]
		private float distanceAway;
		[SerializeField]
		private float distanceUp;
		[SerializeField]
		private float smooth;
		[SerializeField]
		private Transform followXform;
	
		//Private global only
		private Vector3 lookDir;
		private Vector3 targetPosition;
	
	private CamStates camstate = CamStates.Behind;
	
		//Smoothing and Damping
		private Vector3 velocityCamSmooth = Vector3.zero;
		[SerializeField]
		private float camSmoothDampTime = 0.1f;
	
	
	
	public enum CamStates
	{
		Behind,
		ThirdPerson,
		Target,
		Free	
	}
					
	
	// Use this for initialization
	void Start () {
		followXform = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	// Update that is called after all "void Update" functions - ensures everything is positioned correctly for camera
	void LateUpdate()
	{
		
		Vector3 characterOffset = followXform.position + new Vector3(0f, distanceUp, 0f);
		
		
		//Determine camera state
		if (Input.GetAxis("Target") > 0.01f)
		{
			camstate = CamStates.Target;
		}
		else
		{
			camstate = CamStates.Behind;
		}
		
		
		//Execute camera state
		switch (camstate)
		{
			case CamStates.Behind:
				//calculate direction from camera to player, kill y-component, and normalise to give valid direction with unit magnitude
				lookDir = characterOffset - this.transform.position;
				lookDir.y = 0.0f;
				lookDir.Normalize();
				Debug.DrawRay(this.transform.position, 5*lookDir, Color.green);
				
				// setting up target position
				targetPosition = characterOffset + followXform.up * distanceUp - lookDir * distanceAway;
			break;
				
			case CamStates.Target:
				lookDir	= followXform.forward;
				// setting up target position
				targetPosition = characterOffset + followXform.up * distanceUp - followXform.forward * distanceAway;
			break;

		}			

		// check if the camera passes through a wall, and compensate if that is the case
		CompensateForWalls(characterOffset, ref targetPosition);
			
		//making a smooth transition between its current position and the position it wants to be in
		smoothPosition(this.transform.position, targetPosition);
			
		// Making sure to face the right way!
		transform.LookAt(followXform);
	}
	
	private void smoothPosition(Vector3 fromPos, Vector3 toPos)
	{
		//making a smooth transition, of this object, between one position (fromPos) and another (toPos)
		this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}
	
	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
	{
		Debug.DrawLine(fromObject,toTarget,Color.cyan);
		
		//Compensate for walls between camera
		RaycastHit wallHit = new RaycastHit();
		if(Physics.Linecast(fromObject,toTarget, out wallHit))
		{
			Debug.DrawRay(wallHit.point,Vector3.left,Color.red);
			toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
		}
	}
	
}

using UnityEngine;
using System.Collections;

public class IdleCamMove : MonoBehaviour {

	public float startX;
	public float endX;

	public Vector3 MovingSpeed = new Vector3 (0,0,0);
	public bool MovFwd = true;
	public float Maxspeed = 1.0f;
	public float smalldelta = 0.1f;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		if (this.transform.position.x < startX)
		{
			MovFwd = true;
		}

		if (this.transform.position.x > endX)
		{
			MovFwd = false;
		}


		if ((MovFwd) && (MovingSpeed.x < Maxspeed))
		{
			MovingSpeed.x += smalldelta*Time.deltaTime;
		}
		else if ((!MovFwd) && (MovingSpeed.x > -Maxspeed))
		{
			MovingSpeed.x -= smalldelta*Time.deltaTime;
		}
		
		this.transform.Translate(MovingSpeed * Time.deltaTime);


	}
}

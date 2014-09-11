using UnityEngine;
using System.Collections;

public class ContinuousSpin : MonoBehaviour {

	public float RevPerSec = 2.0f;

	// Update is called once per frame
	void Update () {

		transform.Rotate ( new Vector3 (0, -RevPerSec * 1000 * Time.deltaTime ,0));
	
	}
}

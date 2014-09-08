using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

	//public GameObject AOEsmokeFXPrefab;


	[RPC]
	public void PlayFX (GameObject FX, Vector3 origin){
		GameObject SmokeFX = (GameObject)Instantiate (FX, origin, Quaternion.identity);

	}

}

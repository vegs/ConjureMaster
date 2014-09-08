using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

	//public GameObject AOEsmokeFXPrefab;
	public GameObject AOESlamSmokeFX_Prefab;


	[RPC]
	public void PlayFX ( Vector3 origin){ //GameObject FX,
		GameObject SmokeFX = (GameObject)Instantiate (AOESlamSmokeFX_Prefab, origin, Quaternion.identity);
		Debug.Log ("PlayFX");
	}

}

using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

	//public GameObject AOEsmokeFXPrefab;
	public GameObject AOESlamSmokeFX_Prefab;


	[RPC]
	public void PlayFX (string fxName, Vector3 origin){ //GameObject FX,
		switch (fxName) 
		{
			case "AOESmokeFX":
				AOESmokeFX(origin);
				break;
			default:
				break;
				
		}

		//GameObject SmokeFX = (GameObject)Instantiate (AOESlamSmokeFX_Prefab, origin, Quaternion.identity);
		Debug.Log ("PlayFX");
	}

	public void AOESmokeFX (Vector3 origin){
		GameObject SmokeFX = (GameObject)Instantiate (AOESlamSmokeFX_Prefab, origin, Quaternion.identity);
	}
}

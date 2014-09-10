using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

	//public GameObject AOEsmokeFXPrefab;
	public GameObject AOESlamSmokeFX_Prefab;
	public GameObject FlinchStarFX_Prefab;
	public GameObject NormalStarFX_Prefab;
	

	[RPC]
	public void PlayFX (string fxName, Vector3 origin, Vector3 attackerPos){ //GameObject FX,
		switch (fxName) 
		{
			case "AOESmokeFX":
				AOESmokeFX(origin);
				break;
			case "FlinchHit":
				FlinchHit((origin + attackerPos)/2);
				break;
			case "NormalHit":
				NormalHit((origin + attackerPos)/2);
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

	public void FlinchHit (Vector3 position){
		GameObject FlinchStar = (GameObject)Instantiate (FlinchStarFX_Prefab, position, Quaternion.identity);
	}

	public void NormalHit (Vector3 position){
		GameObject NormalStar = (GameObject)Instantiate (NormalStarFX_Prefab, position, Quaternion.identity);
	}
}

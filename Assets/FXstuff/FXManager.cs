using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

	//public GameObject AOEsmokeFXPrefab;
	public GameObject AOESlamSmokeFX_Prefab;
	public GameObject FlinchStarFX_Prefab;
	public GameObject NormalStarFX_Prefab;
	public GameObject TrailingSmokeFX_Prefab;
	

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
			case "TrailingSmoke":
				Debug.Log ("TrailingSmoke activated!");
				TrailingSmoke(origin);					//duration has to be set in later versions!
			break;
		default:
				Debug.Log ("No FX name match");
			break;
				
		}

		//GameObject SmokeFX = (GameObject)Instantiate (AOESlamSmokeFX_Prefab, origin, Quaternion.identity);
		Debug.Log ("PlayFX!");
	}




	public void TrailingSmoke (Vector3 pos){

		float radius = 10f;
		Debug.Log("Instantiating Trailing smoke!");
		GameObject TrailingSmokeOb = (GameObject)Instantiate (TrailingSmokeFX_Prefab, pos, Quaternion.identity);

	//////Find Gameobject based on position
		// get all colliders that intersect pos:
		Collider[] cols = Physics.OverlapSphere(pos, radius);
		
		// find the nearest one:
		float dist = Mathf.Infinity;
		
		foreach (Collider col in cols){
			// find the distance to pos:
			float d = Vector3.Distance(pos, col.transform.position);
			
			if (d < dist && col.tag == "Player"){ // if closer...
				dist = d; // save its distance... 
				
				TrailingSmokeOb.transform.parent = col.gameObject.transform; // and its gameObject
				Debug.Log("We found something!");
				
			}
		}
		TrailingSmokeOb.transform.localPosition = new Vector3 (0,2,0);
	/////////
	}	


	// smoke effects
	public void AOESmokeFX (Vector3 origin){
		GameObject SmokeFX = (GameObject)Instantiate (AOESlamSmokeFX_Prefab, origin, Quaternion.identity);
	}

	public void FlinchHit (Vector3 position){
		GameObject FlinchStar = (GameObject)Instantiate (FlinchStarFX_Prefab, position, Quaternion.identity);
	}

	public void NormalHit (Vector3 position){
		GameObject NormalStar = (GameObject)Instantiate (NormalStarFX_Prefab, position, Quaternion.identity);
	}







	
	// Find Gameobject (and parents an empty GameObject to it) given transform position
	public GameObject FindGamObj(Vector3 pos){
		
		float radius = 10f;
		
		// get all colliders that intersect pos:
		Collider[] cols = Physics.OverlapSphere(pos, radius);
		
		// find the nearest one:
		float dist = Mathf.Infinity;
		
		
		GameObject nearest = new GameObject();
		//nearest.transform.position = pos;
		
		foreach (Collider col in cols){
			// find the distance to pos:
			float d = Vector3.Distance(pos, col.transform.position);
			
			if (d < dist && col.tag == "Player"){ // if closer...
				dist = d; // save its distance... 
				
				nearest.transform.parent = col.gameObject.transform; // and its gameObject
				Debug.Log("We found something!");
				
			}
		}
		nearest.transform.localPosition = new Vector3 ();
		return nearest;
	}



	
}

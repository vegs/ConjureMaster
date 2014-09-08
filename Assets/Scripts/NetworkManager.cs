using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	public GameObject standbyCamera;
	SpawnSpot[] spawnSpots;
	public GameObject mainCamera;
	// Use this for initialization
	public bool offlineMode = false;

	public float respawnTimer = 0;

	void Start () {
		spawnSpots= GameObject.FindObjectsOfType<SpawnSpot>();
		Connect ();
	}

	void Connect(){
		if ( offlineMode ){
			PhotonNetwork.offlineMode = true;
			OnJoinedLobby();
		}else{
			PhotonNetwork.ConnectUsingSettings ( "ConjureMaster v001" );
		}
	}

	void OnGUI(){
		GUILayout.Label ( PhotonNetwork.connectionStateDetailed.ToString() );
	}

	void OnJoinedLobby(){
		Debug.Log ( "OnJoinedLobby" );
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log ( "OnPhotonRandomLobbyFailed" );
		PhotonNetwork.CreateRoom ( null );
	}

	void OnJoinedRoom(){
		SpawnMyPlayer ();
	}

	void SpawnMyPlayer(){
		SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0,spawnSpots.Length)];
		GameObject myPlayerGO = (GameObject) PhotonNetwork.Instantiate ("Player", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);

		standbyCamera.SetActive(false);

		//myPlayerGO.GetComponent<OLD_PlayerMovement> ().enabled = true;
		myPlayerGO.GetComponent<PlayerMovement> ().enabled = true;
		myPlayerGO.GetComponent<Jump> ().enabled = true;
		myPlayerGO.GetComponent<MouseLook> ().enabled = true;
		myPlayerGO.GetComponent<PlayerCombat> ().enabled = true;
		//myPlayerGO.GetComponent<PlayerState> ().enabled = true;


		mainCamera.GetComponent<LookAtCamera> ().body = (GameObject)myPlayerGO;
		mainCamera.GetComponent<LookAtCamera> ().head = myPlayerGO.transform.FindChild ("Head").gameObject;
		mainCamera.SetActive(true);

	}

	void Update() {
		if(respawnTimer > 0) {
			respawnTimer -= Time.deltaTime;
			
			if(respawnTimer <= 0) {
				// Time to respawn the player!
				SpawnMyPlayer();
			}
		}
	}


}

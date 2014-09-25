using UnityEngine;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	public GameObject standbyCamera;
	SpawnSpot[] spawnSpots;
	public GameObject mainCamera;
	// Use this for initialization
	public bool offlineMode = false;
	bool connecting = false;
	public float respawnTimer = 0;
	List<string> chatMessages;
	string currentMsg="";
	bool toggleChat=false;
	float chatDelay=0f;


	void Start () {
		spawnSpots= GameObject.FindObjectsOfType<SpawnSpot>();
		PhotonNetwork.player.name = PlayerPrefs.GetString ("Username", "King Joffrey");
		chatMessages = new List<string> ();
	}
	void OnDestroy(){
		PlayerPrefs.SetString ("Username", PhotonNetwork.player.name);
	}

	public void AddChatMessage(string m){
		GetComponent<PhotonView> ().RPC ("AddChatMessage_RPC", PhotonTargets.All, m);
	}

	[RPC]
	void AddChatMessage_RPC (string m){
		chatMessages.Add (m);
	}

	void Connect(){
		PhotonNetwork.ConnectUsingSettings ( "ConjureMaster v001" );
	}

	void OnGUI(){
		GUILayout.Label ( PhotonNetwork.connectionStateDetailed.ToString() );

		if (PhotonNetwork.connected == false && connecting == false) {
			GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Username: ");
			PhotonNetwork.player.name = GUILayout.TextField (PhotonNetwork.player.name,GUILayout.MaxWidth(300), GUILayout.MinWidth(300) );
			GUILayout.EndHorizontal();

			if (GUILayout.Button("Single Player") ) {
				connecting=true;
				PhotonNetwork.offlineMode = true;
				OnJoinedLobby();
			}
			if (GUILayout.Button("Multi Player") ) {
				connecting=true;
				Connect ();
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		if (PhotonNetwork.connected == true && connecting == false) {
			GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
			GUILayout.BeginVertical();
			GUILayout.FlexibleSpace();	

			foreach(string msg in chatMessages){
				GUILayout.Label (msg);
			}

			GUI.SetNextControlName("chat");



			if (toggleChat){
				Event e = Event.current;
				GUI.FocusControl("chat");
				chatDelay+=Time.deltaTime;

				currentMsg = GUILayout.TextField (currentMsg);
				if (e.keyCode == KeyCode.Return && chatDelay>=0.5f){
					if(currentMsg.Length>0)	AddChatMessage(PhotonNetwork.player.name + ": " +currentMsg);
					chatDelay=0;
					currentMsg="";
					toggleChat=false;
				}
			}
			if (Input.GetButtonDown("Chat")){
				if(toggleChat){
					toggleChat=false;
				}else{
					toggleChat=true;
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
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
		AddChatMessage ("Spawning player: " + PhotonNetwork.player.name);
		connecting = false;
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

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManager_GAME : MonoBehaviour {
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
	int pvID=0;

	public int lives;

	public bool justDied = false;
	public Vector3 DeathPos = Vector3.zero;
	public Quaternion DeathRot = Quaternion.identity;
	public Vector3 DeathVel = Vector3.zero;
	public float deathWatch = 0;
	GameObject deadPlayer = null;
	float startTimer = 0;
	float startTime = 5f;
	bool started = false;

	string selectedChar = null;
	bool inGameMenu=false;
	bool inGameMenu_main=false;
	bool inGameMenu_charSelect = false;
	bool justJoined=false;
	GameObject myPlayerGO = null;


	void OnLevelWasLoaded () {
		Debug.Log ("OnLevelWasLoaded");
	}
	void Awake(){
		Debug.Log ("Awake");

	}

	void Start(){
		Debug.Log ("Start");
		spawnSpots= GameObject.FindObjectsOfType<SpawnSpot>();
		chatMessages = new List<string> ();
		if (PhotonNetwork.inRoom) {
			inGameMenu = true;
			inGameMenu_main = false;
			inGameMenu_charSelect = false;
			connecting = false;
			justJoined = true;	
			lives = (int)PhotonNetwork.room.customProperties["Lives"];
			Debug.Log ("lives: "+lives);
			selectedChar="Player_"+PhotonNetwork.player.customProperties["Character"]+"01";
			if(selectedChar=="Player_Random01"){
				int c=Random.Range(0,3);
				Debug.Log("Random: " +c);
				if(c==0){
					selectedChar="Player_Zombie01";
				}else if (c==1){
					selectedChar="Player_Samurai01";
				}else{
					selectedChar="Player_Elf01";
				}

				
			}


		}else{
			PhotonNetwork.JoinRoom (PhotonNetwork.player.customProperties["RoomName"].ToString());
		}
	}

	public void AddChatMessage(string m){
		GetComponent<PhotonView> ().RPC ("AddChatMessage_RPC", PhotonTargets.All, m);
	}

	[RPC]
	void AddChatMessage_RPC (string m){
		chatMessages.Add (m);
	}

	void Connect(){
		PhotonNetwork.ConnectUsingSettings ( "ConjureMaster v002" );
	}

	void OnGUI(){
		GUILayout.Label ( PhotonNetwork.connectionStateDetailed.ToString() );

		//IN GAME
		if (PhotonNetwork.connected == true && connecting == false) {

			if(inGameMenu){
				GUILayout.BeginArea( new Rect(0, 0, Screen.width, Screen.height) );
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				GUILayout.BeginVertical();
				GUILayout.FlexibleSpace();

				//Menu Title
				GUILayout.BeginHorizontal();
				if(inGameMenu_main) GUILayout.Label("Menu");
				GUILayout.EndHorizontal();
				
				//Menu Buttons
				if(inGameMenu_main){
					if (GUILayout.Button("Disconnect") ) {
						standbyCamera.SetActive(true);
						mainCamera.SetActive(false);
						PhotonNetwork.Disconnect ();
						Application.LoadLevel("Startup_Graveyard");
					}
					if (GUILayout.Button("Close") ) {
						inGameMenu=false;
					}

				}
				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}
			if (myPlayerGO != null)
			{
				GUIStyle nameTagStyle = new GUIStyle();
				nameTagStyle.fontSize = 18;
				nameTagStyle.normal.textColor = Color.red;
				nameTagStyle.alignment = TextAnchor.UpperCenter;
				
				GUIStyle nameTagStyle_outline = new GUIStyle();
				nameTagStyle_outline.fontSize = 18;
				nameTagStyle_outline.alignment = TextAnchor.UpperCenter;
				nameTagStyle_outline.normal.textColor = Color.black;
				nameTagStyle.normal.textColor = Color.red;

//				foreach (PhotonPlayer p in PhotonNetwork.playerList){
//					p.Find(
//
//				}


				GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
				GameObject Hud = GameObject.FindGameObjectWithTag("HUD");
				int c=0;
				foreach (GameObject pO in players){

					Vector3 pos = Camera.main.WorldToScreenPoint(pO.transform.position + Vector3.up*4);
					Vector3 pos1 = pO.transform.position + Vector3.up*4 - Camera.main.transform.position;

					string name=PhotonPlayer.Find(pO.GetComponent<PhotonView>().ownerId).name;
												
					if (Vector3.Dot(Camera.main.transform.forward, pos1) > 0){
						
						GUI.Label (new Rect(pos.x-74, (Screen.height - pos.y) - pos.z/Camera.main.fieldOfView, 150, 150), name, nameTagStyle_outline);
						GUI.Label (new Rect(pos.x-76, (Screen.height - pos.y) - pos.z/Camera.main.fieldOfView, 150, 150), name, nameTagStyle_outline);
						GUI.Label (new Rect(pos.x-75, (Screen.height - pos.y - 1) - pos.z/Camera.main.fieldOfView, 150, 150), name, nameTagStyle_outline);
						GUI.Label (new Rect(pos.x-75, (Screen.height - pos.y + 1) - pos.z/Camera.main.fieldOfView, 150, 150), name, nameTagStyle_outline);
						GUI.Label (new Rect(pos.x-75, (Screen.height - pos.y) - pos.z/Camera.main.fieldOfView, 150, 150), name, nameTagStyle);
					}


					c++;
					foreach (Transform child in Hud.transform)
					{
						if (child.name == "p"+c.ToString()){
							child.gameObject.SetActive(true);

							child.GetComponentInChildren<Text>().text=PhotonPlayer.Find(pO.GetComponent<PhotonView>().ownerId).name + ": \n"+ pO.GetComponent<Health>().damagePercent + "\nLives: "+pO.GetComponent<Health>().lives;
							//GameObject panel = child.FindChild("Panel").gameObject;
							Color32 fr = new Color32(76,76,173,96);
							Color32 en = new Color32(179,0,0,125);
							if(pO.GetPhotonView().isMine){
								child.GetComponent<Image>().color=fr;
								//panel.GetComponent<Image>().color.Equals(new Color(76,76,173,96));
							}else{
								child.GetComponent<Image>().color=en;
							}

							//child.gameObject.SetActive(true);
						}
						if(child.name.CompareTo("p"+c.ToString())>0){
							child.gameObject.SetActive(false);
							//child.GetComponent<Text>().text= "";
							//child.FindChild("Panel").gameObject.SetActive(false);
						}
					}
				}
			}






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
					if(currentMsg.Length>0){
						if(currentMsg.StartsWith("/")){
							Command(currentMsg);
						}else{
							AddChatMessage(PhotonNetwork.player.name + ": " +currentMsg);
						}
					}
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
		inGameMenu = true;
		inGameMenu_main = false;
		inGameMenu_charSelect = true;
		connecting = false;
		justJoined = true;
		lives = (int)PhotonNetwork.room.customProperties["Lives"];
		Debug.Log ("lives: "+lives);
	}

	void SpawnMyPlayer(){
		justJoined=false;
		AddChatMessage ("Spawning player: " + PhotonNetwork.player.name);
		connecting = false;

		SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0,spawnSpots.Length)];
		myPlayerGO = (GameObject) PhotonNetwork.Instantiate (selectedChar, mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);

		standbyCamera.SetActive(false);
		
		myPlayerGO.GetComponent<PlayerMovement> ().enabled = true;
		myPlayerGO.GetComponent<Jump> ().enabled = true;
		myPlayerGO.GetComponent<MouseLook> ().enabled = true;
		myPlayerGO.GetComponent<PlayerCombat> ().enabled = true;
		//myPlayerGO.GetComponent<PlayerState> ().enabled = true;


		mainCamera.GetComponent<LookAtCamera> ().body = (GameObject)myPlayerGO;
		mainCamera.GetComponent<LookAtCamera> ().head = myPlayerGO.transform.FindChild ("Head").gameObject;
		mainCamera.SetActive(true);
		mainCamera.GetComponent<LookAtCamera> ()._autoMove = true;

		myPlayerGO.GetComponent<PlayerState> ().head = (GameObject)myPlayerGO;

	}


	void Update() {
		if (!started) {
			startTimer += Time.deltaTime;
			if(startTimer>=startTime){
				SpawnMyPlayer();
				started = true;
			}
		
		}


		if(respawnTimer > 0) {

			respawnTimer -= Time.deltaTime;
			
			if(respawnTimer <= 0) {
				// Time to respawn the player!
				SpawnMyPlayer();
			}

		}

		if(justDied)
		{
			justDied = false;
			deadPlayer = (GameObject) PhotonNetwork.Instantiate("Dying_" + selectedChar, DeathPos, DeathRot, 0);

			deadPlayer.GetComponent<SelfDestruct> ().selfDestructTime = deathWatch;
			deadPlayer.GetComponent<DyingPlayerState> ().velocity = DeathVel;

			mainCamera.GetComponent<LookAtCamera> ()._autoMove = false;
			mainCamera.GetComponent<LookAtCamera> ().body = (GameObject)deadPlayer;
			mainCamera.GetComponent<LookAtCamera> ().head = deadPlayer.transform.FindChild ("Head").gameObject;

		}
		
		if(deathWatch > 0) {
			deathWatch -= Time.deltaTime;
			
			if(deathWatch <= 0){
				mainCamera.SetActive(false);
				standbyCamera.SetActive(true);
			}


		}

		//MENU
		if (PhotonNetwork.connected == false && inGameMenu == false) {
			Screen.showCursor = true;
			Screen.lockCursor = false;
		}
		//GAME
		if (PhotonNetwork.connected == true && connecting == false) {

			if (Input.GetButtonDown ("Menu")) {
				inGameMenu=true;
				inGameMenu_main=true;
				inGameMenu_charSelect=false;
			}
		}
	}



	void Command(string cmd){
		switch (cmd) 
		{
		case "/spawnbot":
			SpawnBot ();
			break;
		default:
			AddChatMessage ("Unknown oommand!");
			break;	
		}
	}
	void SpawnBot(){
		AddChatMessage ("Spawning bot");
		SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0,spawnSpots.Length)];
		PhotonNetwork.Instantiate ("PlayerBot", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
	}


}

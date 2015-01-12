using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using EventSystem = UnityEngine.EventSystems.EventSystem;

public class NetworkManager_MENU : MonoBehaviour {
	
	public GameObject Canvas_Main;
	public GameObject Canvas_Rooms;
	public GameObject Canvas_NewRoom;
	public GameObject RoomEntry;
	RoomInfo[] roomList;
	bool loaded=false;
	string selectedMap="";
	
	
	
	public bool offlineMode = false;
	bool connecting = false;
	//GameObject menu = GameObject.Find("Name");
	void Awake (){
		Debug.Log ("Awake");
		GameObject.Find("NameField").GetComponent<InputField>().text=PlayerPrefs.GetString ("Username", "");;
		Canvas_Main.SetActive (false);
		Canvas_Rooms.SetActive (false);
		Canvas_NewRoom.SetActive (false);
	}

	void OnLevelWasLoaded () {
		Debug.Log ("OnLevelWasLoaded");
		loaded = true;
		//PhotonNetwork.LeaveRoom ();
		Canvas_Rooms.SetActive (true);
		Connect ();

	}

	void Start () {
		if (!loaded) 
			MainScreen ();
		
		PhotonNetwork.player.name = PlayerPrefs.GetString ("Username", "King Joffrey");
		//GameObject.Find("NameField").GetComponent<InputField>().text=PhotonNetwork.player.name;
	}
	void OnDestroy(){
		PlayerPrefs.SetString ("Username", PhotonNetwork.player.name);
	}
	
	public void OnNameChange(){
		PhotonNetwork.player.name=GameObject.Find("NameField").GetComponent<InputField>().text;
		//PlayerPrefs.SetString ("Username", PhotonNetwork.player.name);
	}
	public void Connect(){
		PhotonNetwork.ConnectUsingSettings ( "ConjureMaster v002" );
		Debug.Log ("CONNECTED");
	}
	
	void OnJoinedLobby(){
		RoomsScreen ();
		Debug.Log ("OnJoinedLobby");
		roomList = PhotonNetwork.GetRoomList ();
		
		foreach (RoomInfo room in PhotonNetwork.GetRoomList ()) {
			Debug.Log(room.name);
			GameObject.Find ("roomtext").GetComponent<Text>().text+="\nNew Room: "+room.name+"   ;   "+room.playerCount+"/"+room.maxPlayers;
		}
	}
	//
	//	void OnJoinedLobby(){
	//		Debug.Log ( "OnJoinedLobby" );
	//		PhotonNetwork.JoinRandomRoom ();
	//	}
	
	//	void OnPhotonRandomJoinFailed(){
	//		Canvas_Main.SetActive (false);
	//		Canvas_Rooms.SetActive (true);
	//		Debug.Log ( "OnPhotonRandomLobbyFailed" );
	//		PhotonNetwork.CreateRoom ( "TESTROOM" );
	//				Debug.Log ("RoomlistLength   "+PhotonNetwork.countOfRooms);
	//				Debug.Log ("RoomlistLength22   "+PhotonNetwork.GetRoomList().Length);
	//		foreach (RoomInfo room in PhotonNetwork.GetRoomList ()) {
	//			Debug.Log(room.name);
	//			GameObject.Find ("roomtext").GetComponent<Text>().text="\nNew Room: "+room.name+"   ;   "+room.playerCount+"/"+room.maxPlayers;
	//		}
	//	}


	public void CreateNewRoom(){
		
		Debug.Log (GameObject.Find ("RoomNameField").GetComponent<InputField>().text);
		if (PhotonNetwork.connectedAndReady){
			if (GameObject.Find ("RoomNameField").GetComponent<InputField>().text!="" && selectedMap!=""){

				Hashtable ht=new Hashtable(){{"Map", selectedMap},{"Lives", 5}, {"Owner", PlayerPrefs.GetString("Username")}};

				string[] roomPropsInLobby = { "Map", "Lives" , "Owner"};
				
				PhotonNetwork.CreateRoom(GameObject.Find ("RoomNameField").GetComponent<InputField>().text, true, true, 4, ht, roomPropsInLobby);
			}
		}
	}


	


	public void LeaveRoom(){
		PhotonNetwork.LeaveRoom ();
	}
	
	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
	
	void OnReceivedRoomListUpdate(){
		Debug.Log (PhotonNetwork.insideLobby);
		if(PhotonNetwork.insideLobby && roomList != PhotonNetwork.GetRoomList ()){
			roomList=PhotonNetwork.GetRoomList ();
			List<GameObject> children = new List<GameObject>();
			foreach (Transform child in GameObject.Find("RoomListAreaPanel").transform) children.Add(child.gameObject);
			children.ForEach(child => Destroy(child));
			Vector2 sd=GameObject.Find("RoomListAreaPanel").GetComponent<RectTransform>().sizeDelta;
			sd.Set(sd.x, 0f);
			foreach (RoomInfo room in roomList) {
				sd.Set (sd.x, sd.y+30f);

				GameObject NewRoomEntry=(GameObject)Canvas.Instantiate(RoomEntry);
				NewRoomEntry.transform.SetParent(GameObject.Find("RoomListAreaPanel").transform);
				RectTransform rect = NewRoomEntry.GetComponent<RectTransform>();
				rect.localScale=new Vector3(1f,1f,1f);
				rect.FindChild("Button").GetComponent<Button>().onClick.AddListener (delegate{JoinARoom (room);});
				rect.FindChild("Text").GetComponent<Text>().text="  "+room.name+"   |   "+room.playerCount+"/"+room.maxPlayers+"  |  "+ room.customProperties["Map"]+ "  |  " + room.customProperties["Lives"] + "  |  " + room.customProperties["Owner"];
				GameObject.Find("RoomListAreaPanel").GetComponent<RectTransform>().sizeDelta.Set(GameObject.Find("RoomListAreaPanel").GetComponent<RectTransform>().sizeDelta.x, GameObject.Find("RoomListAreaPanel").GetComponent<RectTransform>().sizeDelta.y+30f);;
				Debug.Log(room.name);

			}
		}
	}

	void JoinARoom(RoomInfo room){
		//PhotonNetwork.JoinRoom(room.name);

		Canvas_Rooms.SetActive (false);
		if (room.customProperties ["Map"].Equals("Skyscraper")) {
			Application.LoadLevel("Skyscraper_v01");		
		}else if(room.customProperties ["Map"].Equals("Graveyard")) {
			Application.LoadLevel("Graveyard_v02");		
		}
		Hashtable ht=new Hashtable(){{"RoomName", room.name}};
		PhotonNetwork.player.SetCustomProperties(ht);
		//PhotonNetwork.JoinRoom(room.name);
	}

	void OnJoinedRoom(){
		Canvas_Rooms.SetActive (false);
		Debug.Log (PhotonNetwork.room.customProperties ["Map"]);
		if (PhotonNetwork.room.customProperties ["Map"].Equals("Skyscraper")) {
			Debug.Log (PhotonNetwork.room.customProperties ["Map"]);
			Application.LoadLevel("Skyscraper_v01");		
		}else if(PhotonNetwork.room.customProperties ["Map"].Equals("Graveyard")) {
			Debug.Log (PhotonNetwork.room.customProperties ["Map"]);
			Application.LoadLevel("Graveyard_v02");		
		}
	}

	public void SelectMap(string map){
		selectedMap = map;
	}

	public void CreateRoomScreen(){	
		Canvas_Main.SetActive (false);
		Canvas_Rooms.SetActive (false);
		Canvas_NewRoom.SetActive (true);
	}

	public void MainScreen(){	
		PhotonNetwork.Disconnect ();
		Canvas_Main.SetActive (true);
		Canvas_Rooms.SetActive (false);
		Canvas_NewRoom.SetActive (false);
	}

	public void RoomsScreen(){	
		Canvas_Main.SetActive (false);
		Canvas_Rooms.SetActive (true);
		Canvas_NewRoom.SetActive (false);
	}
}

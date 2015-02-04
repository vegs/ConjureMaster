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
	public GameObject Canvas_RoomLobby;
	public GameObject RoomEntry;
	public GameObject RoomLobbyPlayer;
	GameObject stockSlider;
	RoomInfo[] roomList;
	PhotonPlayer[] playerList;
	bool loaded=false;
	string selectedMap = "";
	string selectedCharacter = "Random";
	bool inRoomLobby = false;

	public Texture Icon_Zombie;
	public Texture Icon_Samurai;
	public Texture Icon_Elf;
	public Texture Icon_Random;

	Hashtable player_ht=new Hashtable(){{"RoomName", ""},{"Character", "Random"},{"Lives", 0},{"Damage", 0}};
		
	public bool offlineMode = false;
	bool connecting = false;

	void Awake (){
		Debug.Log ("Awake");
		GameObject.Find("NameField").GetComponent<InputField>().text=PlayerPrefs.GetString ("Username", "");
		stockSlider = GameObject.Find ("StockSlider");
		stockSlider.transform.GetComponentInChildren<Text>().text = stockSlider.GetComponent<Slider>().value.ToString();

		Canvas_Main.SetActive (false);
		Canvas_Rooms.SetActive (false);
		Canvas_NewRoom.SetActive (false);
		Canvas_RoomLobby.SetActive (false);
	}

	void OnLevelWasLoaded () {
		Debug.Log ("OnLevelWasLoaded");
		loaded = true;
		Connect ();

	}

	void Start () {
		if (!loaded) 
			MainScreen ();
		
		PhotonNetwork.player.name = PlayerPrefs.GetString ("Username", "King Joffrey");
	}
	void OnDestroy(){
		PlayerPrefs.SetString ("Username", PhotonNetwork.player.name);
	}
	
	public void OnNameChange(){
		PhotonNetwork.player.name=GameObject.Find("NameField").GetComponent<InputField>().text;
	}
	public void OnStockSliderValueChange(){
		stockSlider.transform.GetComponentInChildren<Text>().text = stockSlider.GetComponent<Slider>().value.ToString();
		
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


	public void CreateNewRoom(){
		
		Debug.Log (GameObject.Find ("RoomNameField").GetComponent<InputField>().text);
		if (PhotonNetwork.connectedAndReady){
			if (GameObject.Find ("RoomNameField").GetComponent<InputField>().text!="" && selectedMap!=""){

				Hashtable ht=new Hashtable(){{"Map", selectedMap},{"Lives", (int)stockSlider.GetComponent<Slider>().value}, {"Owner", PhotonNetwork.player.name}};

				string[] roomPropsInLobby = { "Map", "Lives" , "Owner"};
				
				PhotonNetwork.CreateRoom(GameObject.Find ("RoomNameField").GetComponent<InputField>().text, true, true, 4, ht, roomPropsInLobby);
			}
		}
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
		PhotonNetwork.JoinRoom (room.name);
	}

	void OnJoinedRoom()
	{
		player_ht ["RoomName"] = PhotonNetwork.room.name;
		player_ht ["Lives"] = PhotonNetwork.room.customProperties["Lives"];
		UpdateCustomPlayerProps ();

		RoomLobbyScreen ();	
	}

	void UpdateCustomPlayerProps()
	{
		PhotonNetwork.player.SetCustomProperties(player_ht);
	}

	public void SelectMap(string map){
		selectedMap = map;
	}
	public void SelectCharacter(string character){
		selectedCharacter = character;
		player_ht ["Character"] = character;
		UpdateCustomPlayerProps();

		Debug.Log (character);
	}

	Texture GetIcon(string character){
		switch (character)
		{
		case "Zombie":
			return Icon_Zombie;
			break;
		case "Samurai":
			return Icon_Samurai;
			break;
		case "Elf":
			return Icon_Elf;
			break;
		default:
			return Icon_Random;
			break;
		}
	}

	void Update(){
		if (inRoomLobby) {
			if (PhotonNetwork.inRoom){
				UpdatePlayerList();
				if(PhotonNetwork.isMasterClient){
					Canvas_RoomLobby.transform.FindChild ("StartButton").gameObject.SetActive(true);
				}else{
					Canvas_RoomLobby.transform.FindChild ("StartButton").gameObject.SetActive(false);
				}
			}
			else{
				inRoomLobby = false;
				RoomsScreen();
			}
		}
	}

	void UpdatePlayerList(){
	
		playerList=PhotonNetwork.playerList;
		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in GameObject.Find("PlayerListAreaPanel").transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
		Vector2 sd=GameObject.Find("PlayerListAreaPanel").GetComponent<RectTransform>().sizeDelta;
		sd.Set(sd.x, 0f);
		foreach (PhotonPlayer p in PhotonNetwork.playerList) {
			sd.Set (sd.x, sd.y+30f);
			
			GameObject NewPlayerEntry=(GameObject)Canvas.Instantiate(RoomLobbyPlayer);
			NewPlayerEntry.transform.SetParent(GameObject.Find("PlayerListAreaPanel").transform);
			RectTransform rect = NewPlayerEntry.GetComponent<RectTransform>();
			rect.localScale=new Vector3(1f,1f,1f);
			if(p.isMasterClient) rect.FindChild("Name").FindChild("host").gameObject.SetActive(true);
			rect.FindChild("Icon").GetComponentInChildren<RawImage>().texture = GetIcon(p.customProperties["Character"].ToString());
			rect.FindChild("Name").GetComponentInChildren<Text>().text=p.name;
		}
		
	}

	public void LeaveRoom(){
		PhotonNetwork.LeaveRoom ();
		//RoomsScreen ();
	}

	public void StartGame(){
		PhotonNetwork.room.open = false;
		PhotonNetwork.room.visible = false;

		GetComponent<PhotonView> ().RPC ("LoadLevel", PhotonTargets.All);
	}
	[RPC]
	public void LoadLevel(){
		if (PhotonNetwork.room.customProperties ["Map"].Equals("Skyscraper")) {
			Debug.Log (PhotonNetwork.room.customProperties ["Map"]);
			Application.LoadLevel("Skyscraper_v01");		
		}else if(PhotonNetwork.room.customProperties ["Map"].Equals("Graveyard")) {
			Debug.Log (PhotonNetwork.room.customProperties ["Map"]);
			Application.LoadLevel("Graveyard_v02");		
		}
	}

	public void CreateRoomScreen(){	
		Canvas_NewRoom.SetActive (true);
		Canvas_Main.SetActive (false);
		Canvas_Rooms.SetActive (false);
		Canvas_RoomLobby.SetActive (false);
		GameObject.Find ("RoomNameField").GetComponent<InputField> ().text = PhotonNetwork.player.name + "'s Room";
		SelectMap ("Graveyard");
	}

	public void MainScreen(){	
		PhotonNetwork.Disconnect ();
		Canvas_Main.SetActive (true);
		Canvas_Rooms.SetActive (false);
		Canvas_NewRoom.SetActive (false);
		Canvas_RoomLobby.SetActive (false);
	}

	public void RoomsScreen(){	
		Canvas_Rooms.SetActive (true);
		Canvas_Main.SetActive (false);
		Canvas_NewRoom.SetActive (false);
		Canvas_RoomLobby.SetActive (false);
	}

	public void RoomLobbyScreen(){
		Canvas_RoomLobby.SetActive (true);
		Canvas_Main.SetActive (false);
		Canvas_Rooms.SetActive (false);
		Canvas_NewRoom.SetActive (false);

		inRoomLobby = true;

		Canvas_RoomLobby.transform.FindChild ("Title").GetComponent<Text> ().text = PhotonNetwork.room.name;

//		if (!PhotonNetwork.isMasterClient) {
//			Canvas_RoomLobby.transform.FindChild ("StartButton").gameObject.SetActive(false);
//		}

		UpdatePlayerList ();

	}
}

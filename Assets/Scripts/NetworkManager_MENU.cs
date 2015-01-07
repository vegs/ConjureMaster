using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager_MENU : MonoBehaviour {
	
	public GameObject Canvas_Main;
	public GameObject Canvas_Rooms;
	public GameObject Canvas_CharSelect;
	public GameObject Canvas_NewRoom;
	public GameObject RoomEntry;
	RoomInfo[] roomList;
	
	
	
	public bool offlineMode = false;
	bool connecting = false;
	//GameObject menu = GameObject.Find("Name");
	
	
	void Start () {
		Canvas_Main.SetActive (true);
		Canvas_Rooms.SetActive (false);
		Canvas_CharSelect.SetActive (false);
		Canvas_NewRoom.SetActive (false);
		
		PhotonNetwork.player.name = PlayerPrefs.GetString ("Username", "King Joffrey");
		GameObject.Find("NameField").GetComponent<InputField>().text=PhotonNetwork.player.name;
	}
	void OnDestroy(){
		PlayerPrefs.SetString ("Username", PhotonNetwork.player.name);
	}
	
	public void OnNameChange(){
		PhotonNetwork.player.name=GameObject.Find("NameField").GetComponent<InputField>().text;
	}
	public void Connect(){
		PhotonNetwork.ConnectUsingSettings ( "ConjureMaster v002" );
		Debug.Log ("CONNECTED");
	}
	
	void OnJoinedLobby(){
		Canvas_Main.SetActive (false);
		Canvas_Rooms.SetActive (true);
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
		RoomOptions opt=new RoomOptions();
		
		Debug.Log (GameObject.Find ("RoomNameField").GetComponent<InputField>().text);
		if (PhotonNetwork.connectedAndReady){
			//PhotonNetwork.CreateRoom (GameObject.Find ("RoomNameField").GetComponent<InputField>().text, true, true, 4);
			//string[] prop=new string["Map", "stock"];

			Hashtable ht=new Hashtable(){{"Map","Skyscraper"},{"Lives", 5}, {"Owner",PhotonNetwork.player.name}};
//			ht.Add("Map", "skyscraper");
//			ht.Add("Lives", 5);
			string[] roomPropsInLobby = { "Map", "Lives" };
			
			PhotonNetwork.CreateRoom(GameObject.Find ("RoomNameField").GetComponent<InputField>().text, true, true, 4, ht, roomPropsInLobby);
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
			//GameObject.Find ("roomtext").GetComponent<Text>().text="";

			float ypos=0;
			foreach (RoomInfo room in roomList) {


				//RoomEntry.GetComponent<RectTransform>().SetParent(GameObject.Find("RoomListArea").GetComponent<RectTransform>());
				GameObject NewRoomEntry=(GameObject)Canvas.Instantiate(RoomEntry);
				NewRoomEntry.transform.SetParent(GameObject.Find("RoomListArea").transform);
				//NewRoomEntry.GetComponent<RectTransform>().position.Set(NewRoomEntry.transform.parent.GetComponent<RectTransform>().position.x,NewRoomEntry.transform.parent.GetComponent<RectTransform>().position.y,NewRoomEntry.transform.parent.GetComponent<RectTransform>().position.z);
				RectTransform rect = NewRoomEntry.GetComponent<RectTransform>();
				//rect.anchoredPosition.Set(0,0);
				//rect.localPosition.Set (0,0,0);
				//Event e=new Event({LeaveRoom ()});
				//EventSystem e = new EventSystem();

				rect.FindChild("Button").GetComponent<Button>().onClick.AddListener (delegate{JoinARoom (room);});
				rect.FindChild("Text").GetComponent<Text>().text=room.name+"   |   "+room.playerCount+"/"+room.maxPlayers+"  |  "+ room.customProperties["Map"]+ "  |  " + room.customProperties["Owner"];
				//rect.sizeDelta=new Vector2(200f,30f);
				//Debug.Log ("apos: "+ rect.anchoredPosition+"   pos: "+rect.position+"   lpos: "+rect.localPosition+"   pivot: "+rect.pivot);
				ypos+=30f;
				Debug.Log(room.name);
//				GameObject.Find ("roomtext").GetComponent<Text>().text+="\nNew Room: "+room.name+"   ;   "+room.playerCount+"/"+room.maxPlayers+"   ;  "+ room.customProperties["Map"];
			}
		}
	}

	void JoinARoom(RoomInfo room){
		PhotonNetwork.JoinRoom(room.name);
	}

	void OnJoinedRoom(){
		Canvas_Rooms.SetActive (false);
		Debug.Log (PhotonNetwork.room.customProperties ["Map"]);
		if (PhotonNetwork.room.customProperties ["Map"].Equals("Skyscraper")) {
			Debug.Log (PhotonNetwork.room.customProperties ["Map"]);
			Application.LoadLevel("Skyscraper_v01");		
		}
	}
}

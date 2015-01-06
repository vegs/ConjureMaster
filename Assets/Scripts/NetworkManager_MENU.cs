using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NetworkManager_MENU : MonoBehaviour {

	public GameObject Canvas_Main;
	public GameObject Canvas_Rooms;
	public GameObject Canvas_CharSelect;
	public GameObject Canvas_NewRoom;
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
		//GameObject.Find("NameField").GetComponent<InputField>().onValueChange.AddListener(OnNameChange());
		PhotonNetwork.ConnectUsingSettings ( "ConjureMaster v002" );
		Debug.Log ("CONNECTED");


	}

	void OnJoinedLobby(){
				Canvas_Main.SetActive (false);
				Canvas_Rooms.SetActive (true);
				RoomOptions opt = new RoomOptions ();
				//Room test = new Room ("MyRoom", opt);
				//PhotonNetwork.CreateRoom ("MyRoom1113121312312", true, true, 4);
				//PhotonNetwork.room
				Debug.Log ("OnJoinedLobby");
				roomList = PhotonNetwork.GetRoomList ();
				Debug.Log ("RoomlistLength   " + PhotonNetwork.countOfRooms);
				Debug.Log ("RoomlistLength22   " + PhotonNetwork.GetRoomList ().Length);
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
		//RoomOptions opt=new RoomOptions() {customRoomPropertiesForLobby = new string[name="test"]};
		Debug.Log (GameObject.Find ("RoomNameField").GetComponent<InputField>().text);
		if (PhotonNetwork.connectedAndReady){
			PhotonNetwork.CreateRoom (GameObject.Find ("RoomNameField").GetComponent<InputField>().text, true, true, 4);
			foreach (RoomInfo room in PhotonNetwork.GetRoomList ()) {
				Debug.Log(room.name);
				GameObject.Find ("roomtext").GetComponent<Text>().text="\nNew Room: "+room.ToString()+"   ;   "+room.playerCount+"/"+room.maxPlayers;
			}
		}
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnReceivedRoomListUpdate(){
		Debug.Log (PhotonNetwork.insideLobby);
		if(PhotonNetwork.insideLobby){
			GameObject.Find ("roomtext").GetComponent<Text>().text="";
			foreach (RoomInfo room in PhotonNetwork.GetRoomList ()) {
				Debug.Log(room.name);
				GameObject.Find ("roomtext").GetComponent<Text>().text+="\nNew Room: "+room.name+"   ;   "+room.playerCount+"/"+room.maxPlayers;
			}
		}
	}	
}

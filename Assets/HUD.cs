//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;
//using System.Collections.Generic;
//using Hashtable = ExitGames.Client.Photon.Hashtable;
//using EventSystem = UnityEngine.EventSystems.EventSystem;
//
//public class HUD : MonoBehaviour {
//	public GameObject UI_PlayerInfo;
//	// Use this for initialization
//	void Start () {
//		PlayerUIUpdate ();
//	}
//	void OnPhotonPlayerDisconnected()
//	{
//		PlayerUIUpdate ();
//	}
//
//	void OnPhotonPlayerConnected()
//	{
//		PlayerUIUpdate ();
//	}
//	// Update is called once per frame
//	void Update(){
//		//PlayerUIUpdate ();
//	}
//	void PlayerUIUpdate () {
//
//		List<GameObject> children = new List<GameObject>();
//		foreach (Transform child in GameObject.Find("PlayerScoreAreaPanel").transform) children.Add(child.gameObject);
//		children.ForEach(child => Destroy(child));
//		Vector2 sd=GameObject.Find("PlayerScoreAreaPanel").GetComponent<RectTransform>().sizeDelta;
//		sd.Set(sd.x, 0f);
//		foreach (PhotonPlayer p in PhotonNetwork.playerList)  {
//			sd.Set (sd.x, sd.y+30f);
//			
//			GameObject NewPlayerElement=(GameObject)Canvas.Instantiate(UI_PlayerInfo);
//			NewPlayerElement.transform.SetParent(GameObject.Find("PlayerScoreAreaPanel").transform);
//			RectTransform rect = NewPlayerElement.GetComponent<RectTransform>();
//			rect.localScale=new Vector3(1f,1f,1f);
//			rect.FindChild("Text").GetComponent<Text>().text=p.name + ": \n"+ p.customProperties["Damage"] + "\nLives: "+p.customProperties["Lives"];
//			Color32 fr = new Color32(76,76,173,96);
//			Color32 en = new Color32(179,0,0,125);
//			if(p.isLocal){
//				rect.GetComponent<Image>().color=fr;
//				//panel.GetComponent<Image>().color.Equals(new Color(76,76,173,96));
//			}else{
//				rect.GetComponent<Image>().color=en;
//			}
//			GameObject.Find("PlayerScoreAreaPanel").GetComponent<RectTransform>().sizeDelta.Set(GameObject.Find("PlayerScoreAreaPanel").GetComponent<RectTransform>().sizeDelta.x, GameObject.Find("PlayerScoreAreaPanel").GetComponent<RectTransform>().sizeDelta.y+30f);
//			
//		}
//
//
//
//		foreach (PhotonPlayer p in PhotonNetwork.playerList) {
//			
//
//		}
//
//
//	}
//}

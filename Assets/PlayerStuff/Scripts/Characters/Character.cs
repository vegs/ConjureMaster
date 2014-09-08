using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	//NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();
	public PlayableCharacters _characters = null;

	public CharacterData myCharacter = null;

	// Use this for initialization
	void Start () {
		_characters = GameObject.FindObjectOfType<PlayableCharacters> ();
		myCharacter = new DefaultCharacter ();
//		if (characterInfo) {
//			Debug.Log ("Selected character TestCharacter01");
//		}else if (characterInfo.TestCharacter02){
//			Debug.Log ("Selected character TestCharacter02");
//		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

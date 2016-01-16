using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public static MusicPlayer Instance { get; private set; }

	void Awake() {
		//Debug.Log("MusicPlayer:Awake() ::" + GetInstanceID());
		if (Instance != null) {
			// Don't create new music players if one already exists
			Debug.Log("MusicPlayer:Awake() Destroying extra instance of MusicPlayer ::" + GetInstanceID());
			Destroy(gameObject);
		}
		else {
			// Don't destroy the music player when changing scenes
			Instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		//Debug.Log("MusicPlayer:Start() ::" + GetInstanceID());
	}
	
	// Update is called once per frame
	void Update () {

	}
}

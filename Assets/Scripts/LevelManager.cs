using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public const int _lvlGame = 1;

	public void LoadLevel (string name) {
		Debug.Log("LoadLevel() Name: " + name);
		Application.LoadLevel(name);
	}

	public void LoadNextLevel() {
		Debug.Log("LoadNextLevel() Loaded Level: " + Application.loadedLevelName);
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	public void Quit(){
		Debug.Log("Quit()");
		Application.Quit();
	}

	void OnLevelWasLoaded(int level) {
		Debug.Log("OnLevelWasLoaded() Level: " + level);

		if (level == _lvlGame) {
			ScoreKeeper.Reset();
		}
	}
}

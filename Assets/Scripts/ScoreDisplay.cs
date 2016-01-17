using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplay : MonoBehaviour {

	void Start() {
		UpdateText();

		ScoreKeeper.OnScoreChanged += OnScoreChanged;
	}
	
	//void Update() {
	//	UpdateText();
	//}

	void OnScoreChanged(int score) {
		UpdateText();
	}

	void UpdateText() {
		var txt = GetComponent<Text>();
		if (txt != null) {
			txt.text = ScoreKeeper.score.ToString();
		}
	}

	void OnDestroy() {
		ScoreKeeper.OnScoreChanged -= OnScoreChanged;
	}
}

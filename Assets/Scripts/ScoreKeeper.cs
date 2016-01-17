using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class ScoreKeeper {
	
	public delegate void ScoreChanged(int score);

	public static int score;
	public static event ScoreChanged OnScoreChanged;

	public static void Add(int points) {
		score += points;
		if (OnScoreChanged != null) {
			OnScoreChanged(score);
		}
	}

	public static void Reset() {
		score = 0;
		if (OnScoreChanged != null) {
			//OnScoreChanged(score);
		}
	}
}

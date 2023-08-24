using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerService : MonoBehaviour {
	[SerializeField] private int _extraLifes = 2;

	private int _score = 0;

	public int ExtraLifes => _extraLifes;
	public int Score => _score;


	public event Action <PlayerService> OnEndLifes;

	private void OnDestroy () {
		OnEndLifes = null;
	}

	public void AddScore (int score) {
		_score += score;
	}

	public void AddScore (Vector2 position, int score) {
		AddScore (score);

		Glabal.PlayerInfoPanel.CreateScoreMark (position, score.ToString ());
	}

	public void OnPlayerKilled () {
		_extraLifes--;

		if (ExtraLifes > 0) {
			Glabal.SpawnController.SpawnPlayer ();
		} else {
			OnEndLifes?.Invoke (this);
		}
	}

	public void OnGetExtraLife () {
		_extraLifes++;
	}
}

